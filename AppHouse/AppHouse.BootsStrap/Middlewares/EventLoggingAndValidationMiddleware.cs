using FluentValidation;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppHouse.BootsStrap.Middlewares
{
    internal record struct EventLogger(string Id, string EventName, BsonDocument[]? Validators, bool PassValidations, BsonDocument EventData, DateTime TimeStamp);

    public class EventLoggingAndValidationMiddleware<TRequest, TResponse>
        (
            IMongoClient client,
            IEnumerable<IValidator<TRequest>> validators
        ) 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
        private readonly IMongoCollection<EventLogger> _collection = client.GetDatabase(typeof(EventLogger).Name).GetCollection<EventLogger>("log");

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var log = new EventLogger(
                Id: Guid.NewGuid().ToString(),
                EventName: typeof(TRequest).Name ?? $"unknown event from {typeof(TRequest).Namespace}",
                EventData: request.ToBsonDocument(),
                TimeStamp: DateTime.Now,
                Validators: null,
                PassValidations: true
                
            );

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                log.Validators = [];
                foreach(var validation in validationResults)
                {
                    log.Validators = [.. log.Validators, validation.ToBsonDocument()];
                }

                if (failures.Count > 0)
                {
                    log.PassValidations = false;
#if !DEBUG
                    await _collection.InsertOneAsync(log, null, cancellationToken);
                    throw new ValidationException(failures);
#endif
                }
            }



            _ = _collection.InsertOneAsync(log, null, cancellationToken);
            return await next();
        }
    }
}

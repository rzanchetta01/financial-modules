using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppHouse.BootsStrap.Middlewares
{
    internal sealed record EventLogger(string Id, string EventName, dynamic EventData, DateTime TimeStamp);

    public class EventLoggingMiddleware<TRequest, TResponse>(IMongoDatabase database) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IMongoCollection<EventLogger> _collection = database.GetCollection<EventLogger>(typeof(EventLogger).Name);

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var log = new EventLogger(
                Id: Guid.NewGuid().ToString(),
                EventName: typeof(TRequest).Name ?? $"unkown event from {typeof(TRequest).Namespace}",
                EventData: request.ToBson(),
                TimeStamp: DateTime.Now
                );

            _ = _collection.InsertOneAsync(log, null, cancellationToken);
            var response = await next();
            return response;
        }
    }
}

using AppHouse.Loans.Core.Interfaces;
using AppHouse.SharedKernel.BasicEvents;
using AppHouse.SharedKernel.DTOs;
using MediatR;

namespace AppHouse.Loans.Core
{
    public class LoanService(
        ILoanRepository loanRepository,
        IMediator mediator
        ) : ILoanService
    {
        private readonly ILoanRepository _loanRepository = loanRepository;
        private readonly IMediator _mediator = mediator;

        public async Task Create(LoanDto dto, CancellationToken token)
        {
            var entity = LoanMapping.Map(dto);
            await _loanRepository.CreateAsync(entity, token);
            await _mediator.Publish(new TEventCreated<LoanDto>(LoanMapping.Map(entity)), token);
        }

        public async Task<LoanDto?> FindById(Guid Id, CancellationToken token)
        {
            var entity = await _loanRepository.FindByIdAsync(Id, token);
            if (entity is not null)
                return LoanMapping.Map(entity);

            return null;
        }

        public async Task<IEnumerable<LoanDto>> GetFeasibleLoans(AccountDto accountDto, DateOnly maxFeasibleLoanApplyDate, CancellationToken token)
        {
            if(accountDto.Income > 0 && accountDto.CreditScore > 0)
            {
                var queryResult = await _loanRepository.GetAllAvailableLoans(maxFeasibleLoanApplyDate, token);
                if (queryResult.Any())
                    return LoanMapping.Map(queryResult.Where(e => e.LoanQualityRating >= accountDto.CreditScore));
            }

            return [];
        }

        public async Task Purge(Guid Id, CancellationToken token)
        {
            await _loanRepository.PurgeAsync(Id, token);
            await _mediator.Publish(new TEventCreated<Guid>(Id), token);
        }

        public async Task Update(LoanDto dto, CancellationToken token)
        {
            await _loanRepository.CreateAsync(LoanMapping.Map(dto), token);
        }
    }
}

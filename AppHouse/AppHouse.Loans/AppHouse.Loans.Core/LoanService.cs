using AppHouse.Loans.Core.Interfaces;
using AppHouse.Loans.Domain.Dtos;
using AppHouse.Loans.Domain.Mapping;

namespace AppHouse.Loans.Core
{
    public class LoanService(ILoanRepository loanRepository) : ILoanService
    {
        private readonly ILoanRepository _loanRepository = loanRepository;
        public async Task Create(LoanDto dto, CancellationToken token)
        {
            await _loanRepository.CreateAsync(LoanMapping.Map(dto), token);
        }

        public async Task<LoanDto?> FindById(Guid Id, CancellationToken token)
        {
            var entity = await _loanRepository.FindByIdAsync(Id, token);
            if (entity is not null)
                return LoanMapping.Map(entity);

            return null;
        }

        public async Task Purge(Guid Id, CancellationToken token)
        {
            await _loanRepository.PurgeAsync(Id, token);
        }

        public async Task Update(LoanDto dto, CancellationToken token)
        {
            await _loanRepository.CreateAsync(LoanMapping.Map(dto), token);
        }
    }
}

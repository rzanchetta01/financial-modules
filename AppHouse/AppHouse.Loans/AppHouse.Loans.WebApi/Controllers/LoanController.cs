using AppHouse.Loans.Application.Requests.Commands;
using AppHouse.Loans.Application.Requests.Queries;
using AppHouse.Loans.Core;
using AppHouse.Loans.WebApi.Dto.Request;
using AppHouse.Loans.WebApi.Dto.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppHouse.Loans.WebApi.Controllers
{
    [Route("api/loan")]
    public class LoanController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost("availableloans")]
        public async Task<IResult> GetAvailableLoans([FromBody]GetAvailableLoansRequestDto data, CancellationToken token)
        {
            var req = new GetAvailableLoanRequest
                (
                    AccountId: data.AccountId,
                    CreditScore: data.CreditScore,
                    Income: data.Income,
                    MaxFeasibleDate: data.MaxDate
                );

            var result = await _mediator.Send(req, token);

            List<GetAvailableLoansResponseDto> response = [];
            foreach (var item in result)
            {
                response.Add(new GetAvailableLoansResponseDto
                (
                    CreatorAccountId: item.CreatorAccountId,
                    MaxAmount: item.MaxAmount,
                    MinAmount: item.MinAmount,
                    MaxDateFeasible: item.MaxDateFeasible,
                    MinDateFeasible: item.MinDateFeasible,
                    LoanStyleType: item.LoanStyleType,
                    LoanQualityRating: item.LoanQualityRating,
                    LoanDescription: item.LoanDescription
                ));
            }
            
            return Results.Ok(response);
        }

        [HttpPost("createloan")]
        public async Task<IResult> CreateLoan([FromBody] CreateLoanRequestDto data, CancellationToken token)
        {
            var loanDto = new LoanDto
                (
                    CreatorAccountId: data.CreatorAccountId,
                    MaxAmount: data.MaxAmount,
                    MinAmount: data.MinAmount,
                    MaxDateFeasible: data.MaxDateFeasible,
                    MinDateFeasible: data.MinDateFeasible,
                    LoanStyleType: data.LoanStyleType,
                    LoanQualityRating: data.LoanQualityRating,
                    LoanDescription: data.LoanDescription,
                    Id: null,
                    DateCreated: null,
                    IsActive: null
                );

            var req = new CreateLoanRequest(loanDto);            
            var result = await _mediator.Send(req, token);

            return result ? Results.Created() : Results.BadRequest();
        }
    }
}

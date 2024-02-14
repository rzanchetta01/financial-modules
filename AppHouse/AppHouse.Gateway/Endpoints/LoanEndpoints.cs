using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using AppHouse.SharedKernel.SharedRequests.SharedQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppHouse.Gateway.Endpoints
{
    internal static class LoanEndpoints
    {
        internal static void Setup(WebApplication app)
        {
            var loansEndpoints = app.MapGroup("api/loans").WithOpenApi();

            loansEndpoints.MapGet("available/{MaxDate}/{Id}", async (string MaxDate, Guid Id, [FromServices] IMediator mediator, CancellationToken token) =>
            {
                var req = new GetAvailableLoanRequest(Id, MaxDate);
                var result = await mediator.Send(req, token);

                return Results.Ok(result);
            });

            loansEndpoints.MapPost("", async ([FromBody] LoanDto loan, [FromServices] IMediator mediator, CancellationToken token) =>
            {
                var req = new CreateLoanRequest(loan);
                var result = await mediator.Send(req, token);

                return result ? Results.Created() : Results.BadRequest();
            });
        }
    }
}

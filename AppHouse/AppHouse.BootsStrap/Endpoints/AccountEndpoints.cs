using AppHouse.SharedKernel.DTOs;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppHouse.BootsStrap.Endpoints
{
    internal static class AccountEndpoints
    {
        internal static void Setup(WebApplication app)
        {
            var accountEndpoints = app.MapGroup("api/account").WithOpenApi();

            accountEndpoints.MapPost("", async ([FromBody] AccountDto account, [FromServices] IMediator mediator, CancellationToken token) =>
            {
                var req = new CreateAccountRequest(account);
                var result = await mediator.Send(req, token);

                return result ? Results.Created() : Results.BadRequest();
            });

            accountEndpoints.MapDelete("{Id}", async (Guid id, [FromServices] IMediator mediator, CancellationToken token) =>
            {
                var req = new DeleteAccountRequest(id);
                var result = await mediator.Send(req, token);

                return result ? Results.Accepted() : Results.BadRequest();
            });

            accountEndpoints.MapPut("", async ([FromBody] AccountDto account, [FromServices] IMediator mediator, CancellationToken token) =>
            {
                var req = new UpdateAccountRequest(account);
                var result = await mediator.Send(req, token);

                return result ? Results.Accepted() : Results.BadRequest();
            });
        }
    }
}

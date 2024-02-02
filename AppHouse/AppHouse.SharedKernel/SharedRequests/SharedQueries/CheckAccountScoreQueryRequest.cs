using MediatR;

namespace AppHouse.SharedKernel.SharedRequests.SharedQueries
{
    public record CheckAccountScoreQueryRequest(Guid AccountId) : IRequest<int>;
}

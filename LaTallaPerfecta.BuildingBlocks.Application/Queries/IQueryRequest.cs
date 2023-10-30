using MediatR;

namespace LaTallaPerfecta.BuildingBlocks.Application.Queries;

public interface IQueryRequest<TResponse> : IRequest<TResponse>
{
}

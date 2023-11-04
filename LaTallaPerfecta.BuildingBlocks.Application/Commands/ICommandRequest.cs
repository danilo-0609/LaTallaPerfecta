using MediatR;

namespace LaTallaPerfecta.BuildingBlocks.Application.Commands;

public interface ICommandRequest<TResponse> : IRequest<TResponse>
{
}

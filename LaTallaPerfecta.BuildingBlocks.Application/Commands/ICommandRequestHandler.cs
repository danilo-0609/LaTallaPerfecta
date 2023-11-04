using MediatR;

namespace LaTallaPerfecta.BuildingBlocks.Application.Commands;

public interface ICommandRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : ICommandRequest<TResponse>
    where TResponse : notnull
{
}

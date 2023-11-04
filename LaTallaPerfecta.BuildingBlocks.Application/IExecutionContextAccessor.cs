namespace LaTallaPerfecta.BuildingBlocks.Application;

public interface IExecutionContextAccessor
{
    Ulid UserId { get; }
}

namespace Core.Shared
{
    using MediatR;

    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}


namespace Core.Shared
{
    using MediatR;

    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

}


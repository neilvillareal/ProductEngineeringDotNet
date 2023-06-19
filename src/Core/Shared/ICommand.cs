using System;
using MediatR;

namespace Core.Shared
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}


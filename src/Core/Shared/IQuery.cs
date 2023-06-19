using System;
using MediatR;

namespace Core.Shared
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

}


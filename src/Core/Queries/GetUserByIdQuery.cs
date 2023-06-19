namespace Core.Queries
{
    using System;
    using Core.Shared;
    using Domain.Entities;
    using MediatR;

    public record GetUserByIdQuery(int Id) : IQuery<User>;
}


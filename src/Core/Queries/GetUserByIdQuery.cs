namespace Core.Queries
{
    using Core.Shared;
    using Domain.Entities;

    public record GetUserByIdQuery(int Id) : IQuery<User>;
}


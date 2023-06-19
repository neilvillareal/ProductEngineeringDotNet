namespace Core.Command
{
    using Core.Shared;
    using Domain.Entities;

    public record CreateUserCommand(User User) : ICommand<User>;
}


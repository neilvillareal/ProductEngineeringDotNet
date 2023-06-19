namespace Core.Command
{
    using Core.Shared;
    using Domain.Entities;

    public record UpdateUserCommand(int Id, User User) : ICommand<User>;
}

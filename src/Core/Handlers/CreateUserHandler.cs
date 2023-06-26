namespace Core.Handlers
{
    using Core.Command;
    using Core.Services;
    using Core.Shared;
    using Domain.Entities;

    public class CreateUserHandler : ICommandHandler<CreateUserCommand, User>
	{
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
		{
            _userService = userService;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateUser(request.User, cancellationToken);
        }
    }
}


namespace Core.Handlers
{
    using Core.Command;
    using Core.Services;
    using Core.Shared;
    using Domain.Entities;

    public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, User>
	{
        private readonly IUserService _userService;

        public UpdateUserHandler(IUserService userService)
		{
            _userService = userService;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUser(request.Id, request.User, cancellationToken);
        }
    }
}


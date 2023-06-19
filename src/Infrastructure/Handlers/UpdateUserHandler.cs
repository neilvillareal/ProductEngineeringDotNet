namespace Infrastructure.Handlers
{
    using System;
    using Core.Command;
    using Core.Services;
    using Core.Shared;
    using Domain.Entities;
    using Domain.Exceptions;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

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


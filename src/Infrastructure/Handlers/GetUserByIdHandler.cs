namespace Infrastructure.Handlers
{
    using System;
    using Core.Queries;
    using Core.Services;
    using Core.Shared;
    using Domain.Entities;
    using Domain.Exceptions;
    using Infrastructure.Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, User>
    {
        private readonly IUserService _userService;

        public GetUserByIdHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetUserById(request.Id, cancellationToken);
        }
    }
}


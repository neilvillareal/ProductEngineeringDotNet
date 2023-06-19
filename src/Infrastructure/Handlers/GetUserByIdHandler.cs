namespace Infrastructure.Handlers
{
    using System;
    using Core.Queries;
    using Core.Shared;
    using Domain.Entities;
    using Domain.Exceptions;
    using Infrastructure.Data;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserByIdHandler : IQueryHandler<GetUserByIdQuery, User>
    {
        private readonly AppDbContext _appDbContext;

        public GetUserByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.Users
                            .Include(a=>a.Employments)
                            .Include(a=>a.Address)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Id.Equals(request.Id), cancellationToken: cancellationToken);

            if (result is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            return result!;
        }
    }
}


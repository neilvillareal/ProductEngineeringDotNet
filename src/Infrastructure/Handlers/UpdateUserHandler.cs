namespace Infrastructure.Handlers
{
    using System;
    using Core.Command;
    using Core.Shared;
    using Domain.Entities;
    using Domain.Exceptions;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class UpdateUserHandler : ICommandHandler<UpdateUserCommand, User>
	{
        private readonly AppDbContext _appDbContext;

        public UpdateUserHandler(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
        }

        public async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users
                                .Include(a => a.Employments)
                                .Include(a => a.Address)
                                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (user is null)
            {
                throw new UserNotFoundException(request.Id);
            }

            _appDbContext.Entry(user).State = EntityState.Modified;
            _appDbContext.Entry(user).Property(x => x.Id).IsModified = false;
            _appDbContext.Entry(user!).CurrentValues.SetValues(request.User);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return user!;
        }
    }
}


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

            //var a = _appDbContext.Entry(user!).CurrentValues.Properties;
            //_appDbContext.Entry(user).State = EntityState.Modified;
            //_appDbContext.Entry(user).Property(x => x.Id).IsModified = false;
            //_appDbContext.Attach(user);
            //_appDbContext.Entry(user!).CurrentValues.SetValues(request.User);

            user.FirstName = request.User.FirstName;
            user.LastName = request.User.LastName;
            user.Email = request.User.Email;

            if (request.User.Address is not null)
            {
                _appDbContext.Remove(user.Address!);
            }

            user.Address = request.User.Address;

            if (request.User.Employments is not null &&
                user.Employments.Count > 0)
            {
                _appDbContext.RemoveRange(user.Employments!);
            }

            user.Employments = request.User.Employments!;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return user!;
        }
    }
}


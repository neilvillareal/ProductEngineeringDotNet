namespace Infrastructure.Services
{
    using System;
    using System.Threading;
    using Core.Services;
    using Domain.Entities;
    using Domain.Exceptions;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
	{
        private readonly AppDbContext _appDbContext;

        public UserService(AppDbContext appDbContext)
		{
            this._appDbContext = appDbContext;
        }

        public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
        {
            _appDbContext.Users.Add(user);

            await _appDbContext.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User?> GetUserByEmailAddress(string emailAddress)
        {
            return await _appDbContext.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Email.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<User> GetUserById(int id, CancellationToken cancellationToken)
        {
            var result = await _appDbContext.Users
                            .Include(a => a.Employments)
                            .Include(a => a.Address)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Id.Equals(id), cancellationToken: cancellationToken);

            if (result is null)
            {
                throw new UserNotFoundException(id);
            }

            return result;
        }

        public async Task<User> UpdateUser(int id, User user, CancellationToken cancellationToken)
        {
            var existingUser = await _appDbContext.Users
                                .Include(a => a.Employments)
                                .Include(a => a.Address)
                                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

            if (existingUser is null)
            {
                throw new UserNotFoundException(id);
            }

            //var a = _appDbContext.Entry(user!).CurrentValues.Properties;
            //_appDbContext.Entry(user).State = EntityState.Modified;
            //_appDbContext.Entry(user).Property(x => x.Id).IsModified = false;
            //_appDbContext.Attach(user);
            //_appDbContext.Entry(user!).CurrentValues.SetValues(request.User);

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            if (existingUser.Address is not null)
            {
                _appDbContext.Remove(existingUser.Address!);
            }

            existingUser.Address = user.Address;

            if (user.Employments is not null &&
                existingUser.Employments.Count > 0)
            {
                _appDbContext.RemoveRange(existingUser.Employments!);
            }

            existingUser.Employments = user.Employments!;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return user!;
        }
    }
}


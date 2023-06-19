namespace Core.Services
{
    using System;
    using Domain.Entities;

    public interface IUserService
	{
		Task<User> GetUserByEmailAddressAsync(string emailAddress);

        Task<User> CreateUser(User user, CancellationToken cancellationToken);

        Task<User> GetUserById(int id, CancellationToken cancellationToken);

        Task<User> UpdateUser(int id, User user, CancellationToken cancellationToken);
    }
}


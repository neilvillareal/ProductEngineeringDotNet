namespace Infrastructure.Handlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Command;
    using Core.Shared;
    using Domain.Entities;
    using Infrastructure.Data;

    public class CreateUserHandler : ICommandHandler<CreateUserCommand, User>
	{
        private readonly AppDbContext _appDbContext;

        public CreateUserHandler(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _appDbContext.Users.Add(request.User);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return request.User;
        }
    }
}


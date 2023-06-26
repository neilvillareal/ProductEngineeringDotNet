namespace Core.Handlers
{
    using Core.Queries;
    using Core.Services;
    using Core.Shared;
    using Domain.Entities;

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


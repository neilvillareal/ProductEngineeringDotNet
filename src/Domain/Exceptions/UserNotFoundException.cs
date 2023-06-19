namespace Domain.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(int userId)
            : base($"Unable to find a user with UserId: {userId}")
        {
        }
    }
}
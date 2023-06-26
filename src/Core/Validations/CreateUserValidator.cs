namespace Core.Validations
{
    using System;
    using Core.Command;
    using Core.Services;
    using FluentValidation;

    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
	{
        private readonly IUserService _userService;

        public CreateUserValidator(IUserService userService)
		{

            _userService = userService;

            RuleFor(u => u.User.FirstName)
				.NotEmpty()
				.NotNull()
				.MaximumLength(200);

			RuleFor(u => u.User.LastName)
				.NotEmpty()
				.NotNull()
				.MaximumLength(200);

			RuleFor(u => u.User.Email)
				.NotNull()
				.NotEmpty()
				.EmailAddress()
				.MaximumLength(100);

            RuleFor(u => u.User.Email)
                .MustAsync(async (email, cancellationToken) =>
				{
					var user = await _userService.GetUserByEmailAddress(email!);

					return user is null;
				}).WithMessage("'Email' already exists");

            RuleFor(u => u.User.Address)
				.SetValidator(new AddressValidator());

            RuleForEach(u => u.User.Employments)
				.SetValidator(new EmploymentValidator());
        }
	}
}


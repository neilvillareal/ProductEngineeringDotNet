namespace Core.Validations
{
    using System;
    using Core.Command;
    using Core.Services;
    using FluentValidation;

    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
	{
        private readonly IUserService _userService;

        public UpdateUserValidator(IUserService userService)
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

            //RuleFor(u => u.User.Email)
            //   .MustAsync(async (email, cancellationToken) =>
            //   {
            //       var user = await _userService.GetUserByEmailAddressAsync(email!);
            //       if (user is null)
            //           return false;

            //       return user.Email != this.va;
            //   }).WithMessage("'Email' already exists");

            RuleFor(u => u.User.Address)
                .SetValidator(new AddressValidator());

            RuleForEach(u => u.User.Employments)
                .SetValidator(new EmploymentValidator());
        }
	}
}


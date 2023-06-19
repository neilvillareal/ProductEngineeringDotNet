namespace Core.Validations
{
    using System;
    using Core.Command;
    using FluentValidation;

    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserValidator()
		{
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

            RuleFor(u => u.User.Address)
				.SetValidator(new AddressValidator());

			RuleForEach(u => u.User.Employments)
				.SetValidator(new EmploymentValidator());
        }
	}
}


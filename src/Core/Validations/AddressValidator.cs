namespace Core.Validations
{
    using System;
    using Domain.Entities;
    using FluentValidation;

    public class AddressValidator : AbstractValidator<Address>
	{
		public AddressValidator()
		{
			RuleFor(a => a.Street)
                .NotEmpty()
                .NotNull()
                .MaximumLength(200);

            RuleFor(a => a.City)
                .NotEmpty()
                .NotNull()
                .MaximumLength(200);
        }
	}
}


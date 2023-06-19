namespace Core.Validations
{
    using System;
    using Domain.Entities;
    using FluentValidation;

    public class EmploymentValidator : AbstractValidator<Employment>
	{
		public EmploymentValidator()
		{
			RuleFor(e => e.Company)
				.NotEmpty()
				.NotNull()
				.MaximumLength(200);

            RuleFor(e => e.MonthsOfExperience)
                .NotEmpty()
                .NotNull();

            RuleFor(e => e.Salary)
                .NotEmpty()
                .NotNull();

            RuleFor(e => e.StartDate)
                .NotEmpty()
                .NotNull();

            RuleFor(e => e.EndDate)
                .GreaterThan(e => e.StartDate)
                .WithMessage("'End Date' should be greater that 'Start Date'")
                .NotEmpty()
                .NotNull();

        }
	}
}


namespace UnitTests.CoreTest.ValidatorsTests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Core.Command;
    using Core.Validations;
    using Domain.Entities;
    using FluentValidation.TestHelper;

    public class CreateUserValidationTest
	{
		private CreateUserValidator validator;

        private User user;

        [SetUp]
        public void Setup()
        {
            validator = new CreateUserValidator();

            user = new User
            {
                FirstName = "Neil",
                LastName = "Villareal",
                Email = "neilvillareal@gmail.com",
                Address = new Address
                {
                    City = "Mandaluyong City",
                    PostCode = 1550,
                    Street = "Kalinisan St"
                },
                Employments = new List<Employment>
                {
                    new Employment
                    {
                        Company = "Doofenshmirtz Evil Inc.",
                        MonthsOfExperience = 12,
                        Salary = 100000,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddYears(1),
                    },
                    new Employment
                    {
                        Company = "Willy Wonka Chocolate Factory",
                        MonthsOfExperience = 23,
                        Salary = 180000,
                        StartDate = DateTime.Now.AddYears(-3),
                        EndDate = DateTime.Now.AddYears(-2),
                    }
                }
            };
        }

        [Test]
		public void Should_ReturnValidationErrorOnAddress_When_CityStreet_IsNullOrEmpty()
		{
            user.Address.City = null;
            user.Address.Street = string.Empty;
            var command = new CreateUserCommand(user);

            var result = validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(a => a.User.Address.Street)
                .WithErrorMessage("'Street' must not be empty.");

            result.ShouldHaveValidationErrorFor(a => a.User.Address.City)
               .WithErrorMessage("'City' must not be empty.");

        }

        [Test]
        public void Should_ReturnValidationErrorOnEmployments_When_EndDate_IsLessThanOrEqualToStartDate()
        {
            user.Employments[0].StartDate = DateTime.Now;
            user.Employments[0].EndDate = DateTime.Now.AddDays(-2);

            var command = new CreateUserCommand(user);

            var result = validator.TestValidate(command);

            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo("'End Date' should be greater that 'Start Date'"));
        }

    }
}


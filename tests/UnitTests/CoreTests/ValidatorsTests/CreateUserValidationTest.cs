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
                FirstName = "",
                LastName = "",
                Email = "neilvillareal@gmail.com",
                Address = new Address
                {
                    City = "Mandaluyong City",
                    PostCode = 1550,
                    Street = "Kalinisan St"
                },
                Employments = new List<Employment>
                {

                }
            };
        }

        [Test]
		public void Should_ReturnErrorOnAddress_When_CityStreetIsNullOrEmpty()
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

    }
}


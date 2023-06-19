using System;
using Domain.Entities;

namespace IntegrationTests.ServicesTests.UserServiceTests
{
	public class GetUserByIdTest : BaseUserServiceTest
	{
        protected User User;

        public GetUserByIdTest():base()
		{

		}

		[SetUp]
		public void Setup()
		{
            User = new User
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
		public async Task Should_ReturnNewlyCreatedUser_WithId()
		{
            _ = await Service.CreateUser(User!, new CancellationToken());

            int id = User.Id;

			var result = await Service.GetUserById(id, new CancellationToken());

            Assert.That(result.Email, Is.EqualTo(User.Email));
		}
	}
}


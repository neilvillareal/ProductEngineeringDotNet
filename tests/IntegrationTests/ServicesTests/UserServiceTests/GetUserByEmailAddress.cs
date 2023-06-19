using System;
using Domain.Entities;

namespace IntegrationTests.ServicesTests.UserServiceTests
{
	public class GetUserByEmailAddress : BaseUserServiceTest
	{
        protected User User;

        public GetUserByEmailAddress():base()
		{
		}

        [SetUp]
        public async Task Setup()
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

            _ = await Service.CreateUser(User, new CancellationToken());
        }

        [Test]
        [TestCase("neilvillareal@gmail.com", true)]
        [TestCase("neil.villareal@gmail.com", false)]
        [TestCase("nEilvIllaReaL@gmail.com", true)]
        [TestCase("nEil02@gmail.com", false)]
        [TestCase("nelvilareal@gmail.com", false)]
        public async Task Should_GetUserByEmailAddressAsync_Return_ExistingUser(string email, bool shouldExists)
        {
            var actualResult = await Service.GetUserByEmailAddressAsync(email);

            Assert.AreEqual(shouldExists, actualResult is not null);
        }
    }
}


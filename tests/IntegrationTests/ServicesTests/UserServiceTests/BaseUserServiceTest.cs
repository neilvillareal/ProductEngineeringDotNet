namespace IntegrationTests.ServicesTests.UserServiceTests
{
    using Domain.Entities;

    public class BaseUserServiceTest
	{
		protected AppDbContext Context;

		protected UserService Service;

		public BaseUserServiceTest()
		{
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                                .UseInMemoryDatabase(databaseName: "AlightTestDb")
                                .Options;

            Context = new AppDbContext(dbOptions);

            Service = new UserService(Context);
        }
	}
}


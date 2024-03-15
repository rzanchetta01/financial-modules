namespace AppHouse.Tests.Accounts
{
    public class AccountRepositoryTests
    {
        [Fact]
        public async Task PassDeleteAccountRepositoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var data = AccountMapping.Map(DummyData.DummyNewAccountDto);
            var token = CancellationToken.None;

            await uat.CreateAsync(data, token);

            //Act
            await uat.PurgeAsync(data.Id, token);

            //Assert
            Assert.Equal(0, await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task PassCreateAccountRepositoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var data = AccountMapping.Map(DummyData.DummyNewAccountDto);
            var token = CancellationToken.None;

            //Act
            await uat.CreateAsync(data, token);

            //Assert
            Assert.Equal(1, await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task FailCreateAccountRepositoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var data = AccountMapping.Map(DummyData.DummyNewAccountDto);
            var uat = new AccountRepository(context);
            var token = CancellationToken.None;

            //Act and Assert
            await uat.CreateAsync(data, token);

            //duplicated inserts should be blocked by the context/table rules
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.CreateAsync(data, token));
            Assert.Equal(1, await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task PassUpdateAccountRepositoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var dataInput = AccountMapping.Map(DummyData.DummyNewAccountDto);
            var token = CancellationToken.None;

            await uat.CreateAsync(dataInput, token);

            //Act
            var dataOutputResult = await uat.FindByIdAsync(dataInput.Id, token) ?? throw new Exception("did not found input data");
            dataOutputResult.Income = 999M;

            await uat.UpdateAsync(dataOutputResult, token);

            //Assert
            Assert.Equal(dataInput, dataOutputResult);
            Assert.Equal(1, await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task PassFindByIdRepositoryTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var data = AccountMapping.Map(DummyData.DummyNewAccountDto);
            var token = CancellationToken.None;
            var dtoData = AccountMapping.Map(data);

            await uat.CreateAsync(data, token);

            //Act
            var result = await uat.FindByIdAsync(data.Id, token) ?? throw new Exception("did not found");

            //Assert
            Assert.Equal(dtoData, AccountMapping.Map(result));
            Assert.Equal(1, await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task PassCreateRangeAsyncTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var data = AccountMapping.Map(DummyData.DummyListOfExistingAccountDto);
            var token = CancellationToken.None;

            //Act
            await uat.CreateRangeAsync(data, token);

            //Assert
            Assert.Equal(data.Count(), await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task FailCreateRangeAsyncTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var data = AccountMapping.Map(DummyData.DummyListOfExistingAccountDto);
            var uat = new AccountRepository(context);
            var token = CancellationToken.None;

            //Act and Assert
            await uat.CreateRangeAsync(data, token);

            //duplicated inserts should be blocked by the context/table rules
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.CreateRangeAsync(data, token));
            Assert.Equal(data.Count(), await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task PassUpdateRangeAsyncTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var dataInput = AccountMapping.Map(DummyData.DummyListOfExistingAccountDto).ToList();
            var token = CancellationToken.None;

            await uat.CreateRangeAsync(dataInput, token);

            //Act
            var dataOutputResult = await uat.Table().ToListAsync(token) ?? throw new Exception("did not found input data");
            dataOutputResult.ForEach(e => e.Income = 99M);

            await uat.UpdateRangeAsync(dataOutputResult, token);

            //Assert
            Assert.Equal(dataInput, dataOutputResult); 
            Assert.Equal(dataInput.Count, await context.Accounts.CountAsync());
        }

        [Fact]
        public async Task PassDeleteRangeAsyncTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);
            var data = AccountMapping.Map(DummyData.DummyListOfExistingAccountDto);
            var token = CancellationToken.None;

            await uat.CreateRangeAsync(data, token);

            //Act
            var ids = data.Select(data => data.Id).ToList();
            await uat.PurgeRangeAsync(ids, token);

            //Assert
            Assert.Equal(0, await context.Accounts.CountAsync());
        }

        [Fact]
        public void PassTableQuery()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<AccountsContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new AccountsContext(options);

            var uat = new AccountRepository(context);

            //Act
            var result = uat.Table();

            //Assert
            Assert.NotNull(result);
        }
    }
}

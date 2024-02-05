namespace AppHouse.Accounts.Tests
{
    public class CreateAccountTests
    {
        private readonly Mock<IAccountService> _mockAccountService = new();
        private readonly Mock<IAccountRepository> _mockAccountRepository = new();
        private readonly Mock<IMediator> _mockMediator = new();

        [Fact]
        public async Task PassCreateAccountCommandTest() 
        {
            //Arrange
            var data = DummyData.DummyNewAccountDto;
            var request = new CreateAccountRequest(data);
            var token = CancellationToken.None;
            var uat = new CreateAccountCommand(_mockAccountService.Object, _mockMediator.Object);
            
            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.True(result);
            Assert.Null(data.Id);
            Assert.Null(data.DateCreated);
            Assert.Null(data.IsActive);
            _mockAccountService.Verify(v => v.Create(It.IsAny<AccountDto>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEntityCreated<AccountDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailCreateAccountCommandTest()
        {
            //Arrange
            var data = DummyData.DummyExistingActiveAccountDto;//Existing account should fail account creation
            var request = new CreateAccountRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.Create(data, token)).Throws(new Exception("fake exception"));

            var uat = new CreateAccountCommand(_mockAccountService.Object, _mockMediator.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));

            Assert.NotNull(data.Id);
            Assert.NotNull(data.DateCreated);
            Assert.NotNull(data.IsActive);
            _mockAccountService.Verify(v => v.Create(It.IsAny<AccountDto>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEntityCreated<AccountDto>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task PassCreateAccountValidatorTest()
        {
            //Arrange
            var requestData = DummyData.DummyNewAccountDto;
            var request = new CreateAccountRequest(requestData);
            var token = CancellationToken.None;
            var uat = new CreateAccountValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task FailCreateAccountValidatorTest()
        {
            //Arrange
            var requestData = DummyData.DummyExistingDisabledAccountDto;
            var request = new CreateAccountRequest(requestData);
            var token = CancellationToken.None;
            var uat = new CreateAccountValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task PassCreateAccountServiceTest()
        {
            //Arrange
            var data = DummyData.DummyNewAccountDto;
            var token = CancellationToken.None;

            var uat = new AccountService(_mockAccountRepository.Object);
            
            //Act
            await uat.Create(data, token);

            //Assert
            _mockAccountRepository.Verify(m => m.CreateAsync(It.IsAny<Account>(), token), Times.Once);
        }

        [Fact]
        public async Task FailCreateAccountServiceTest()
        {
            //Arrange
            var data = DummyData.DummyExistingDisabledAccountDto; //Should fail with existing accounts
            var token = CancellationToken.None;

            _mockAccountRepository.Setup(m => m.CreateAsync(It.IsAny<Account>(), token)).Throws(new Exception("fake exception"));

            var uat = new AccountService(_mockAccountRepository.Object);
        
            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () =>  await uat.Create(data, token));
            _mockAccountRepository.Verify(m => m.CreateAsync(It.IsAny<Account>(), token), Times.Once);
        }
    }
}

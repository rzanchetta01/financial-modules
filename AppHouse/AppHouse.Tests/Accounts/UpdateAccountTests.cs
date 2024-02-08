namespace AppHouse.Tests.Accounts
{
    public class UpdateAccountTests
    {
        private readonly Mock<IAccountService> _mockAccountService = new();
        private readonly Mock<IAccountRepository> _mockAccountRepository = new();
        private readonly Mock<IMediator> _mockMediator = new();

        [Fact]
        public async Task PassUpdateAccountCommandTest()
        {
            //Arrange
            var data = DummyData.DummyExistingActiveAccountDto;
            var request = new UpdateAccountRequest(data);
            var token = CancellationToken.None;
            var uat = new UpdateAccountCommand(_mockAccountService.Object, _mockMediator.Object);

            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.True(result);
            _mockAccountService.Verify(v => v.Update(It.IsAny<AccountDto>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEntityUpdated<AccountDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailUpdateAccountCommandTest()
        {
            //Arrange
            var data = DummyData.DummyExistingDisabledAccountDto;//Existing disabled account should fail account update
            var request = new UpdateAccountRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.Update(data, token)).Throws(new Exception("fake exception"));

            var uat = new UpdateAccountCommand(_mockAccountService.Object, _mockMediator.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));

            Assert.NotNull(data.Id);
            Assert.NotNull(data.DateCreated);
            Assert.NotNull(data.IsActive);
            _mockAccountService.Verify(v => v.Update(It.IsAny<AccountDto>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEntityUpdated<AccountDto>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task PassUpdateAccountValidatorTest()
        {
            //Arrange
            var requestData = DummyData.DummyExistingActiveAccountDto;
            var request = new UpdateAccountRequest(requestData);
            var token = CancellationToken.None;
            var uat = new UpdateAccountValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task FailUpdateAccountValidatorTest()
        {
            //Arrange
            var requestData = DummyData.DummyExistingDisabledAccountDto;
            var request = new UpdateAccountRequest(requestData);
            var token = CancellationToken.None;
            var uat = new UpdateAccountValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task PassUpdateAccountServiceTest()
        {
            //Arrange
            var data = DummyData.DummyExistingActiveAccountDto;
            var token = CancellationToken.None;

            var uat = new AccountService(_mockAccountRepository.Object);

            //Act
            await uat.Update(data, token);

            //Assert
            _mockAccountRepository.Verify(m => m.UpdateAsync(It.IsAny<Account>(), token), Times.Once);
        }

        [Fact]
        public async Task FailUpdateAccountServiceTest()
        {
            //Arrange
            var data = DummyData.DummyExistingDisabledAccountDto; //Should fail with disable accounts
            var token = CancellationToken.None;

            _mockAccountRepository.Setup(m => m.UpdateAsync(It.IsAny<Account>(), token)).Throws(new Exception("fake exception"));

            var uat = new AccountService(_mockAccountRepository.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Update(data, token));
            _mockAccountRepository.Verify(m => m.UpdateAsync(It.IsAny<Account>(), token), Times.Once);
        }
    }
}

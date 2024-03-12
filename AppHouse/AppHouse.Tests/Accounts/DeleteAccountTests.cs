namespace AppHouse.Tests.Accounts
{
    public class DeleteAccountTests
    {
        private readonly Mock<IAccountService> _mockAccountService = new();
        private readonly Mock<IAccountRepository> _mockAccountRepository = new();
        private readonly Mock<IMediator> _mockMediator = new();

        [Fact]
        public async Task PassDeleteAccountCommandTest()
        {
            //Arrange
            var data = Guid.NewGuid();
            var request = new DeleteAccountRequest(data);
            var token = CancellationToken.None;
            var uat = new DeleteAccountCommandHandler(_mockAccountService.Object, _mockMediator.Object);

            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.True(result);
            _mockAccountService.Verify(v => v.Purge(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEventPurged<Guid>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailDeleteAccountCommandTest()
        {
            //Arrange
            var data = Guid.Empty;
            var request = new DeleteAccountRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.Purge(data, token)).Throws(new Exception("fake exception"));

            var uat = new DeleteAccountCommandHandler(_mockAccountService.Object, _mockMediator.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));

            _mockAccountService.Verify(v => v.Purge(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEventPurged<Guid>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task PassDeleteAccountValidatorTest()
        {
            //Arrange
            var requestData = Guid.NewGuid();
            var request = new DeleteAccountRequest(requestData);
            var token = CancellationToken.None;
            var uat = new DeleteAccountValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task FailDeleteAccountValidatorTest()
        {
            //Arrange
            var requestData = Guid.Empty;
            var request = new DeleteAccountRequest(requestData);
            var token = CancellationToken.None;
            var uat = new DeleteAccountValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task PassDeleteAccountServiceTest()
        {
            //Arrange
            var data = Guid.NewGuid();
            var token = CancellationToken.None;

            var uat = new AccountService(_mockAccountRepository.Object, _mockMediator.Object);

            //Act
            await uat.Purge(data, token);
    
            //Assert
            _mockAccountRepository.Verify(m => m.PurgeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailDeleteAccountServiceTest()
        {
            //Arrange
            var data = Guid.Empty;
            var token = CancellationToken.None;

            _mockAccountRepository.Setup(m => m.PurgeAsync(It.IsAny<Guid>(), token)).Throws(new Exception("fake exceptions"));

            var uat = new AccountService(_mockAccountRepository.Object, _mockMediator.Object);

            //Act and Assert
            await Assert.ThrowsAsync<Exception>(() => uat.Purge(data, token));

            _mockAccountRepository.Verify(m => m.PurgeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

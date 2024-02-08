namespace AppHouse.Tests.Accounts
{
    public class GetAccountDetailsTests
    {
        private readonly Mock<IAccountService> _mockAccountService = new();

        [Fact]
        public async Task PassGetAccountDetailsHandler()
        {
            //Arrange
            var data = Guid.NewGuid();
            var request = new GetAccountDetailsQueryRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.FindById(It.IsAny<Guid>(), token)).ReturnsAsync(DummyData.DummyExistingActiveAccountDto);
            var uat = new GetAccountDetailsQueryHandler(_mockAccountService.Object);

            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(DummyData.DummyExistingActiveAccountDto, result);
            _mockAccountService.Verify(m => m.FindById(It.IsAny<Guid>(), token), Times.Once);
        }

        [Fact]
        public async Task FailGetAccountDetailsHandler()
        {
            //Arrange
            var data = Guid.Empty;
            var request = new GetAccountDetailsQueryRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.FindById(It.IsAny<Guid>(), token)).ReturnsAsync((AccountDto?)null);
            var uat = new GetAccountDetailsQueryHandler(_mockAccountService.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));
            _mockAccountService.Verify(m => m.FindById(It.IsAny<Guid>(), token), Times.Once);
        }

        [Fact]
        public async Task PassGetAccountDetailsValidator()
        {
            //Arrange
            var requestData = Guid.NewGuid();
            var request = new GetAccountDetailsQueryRequest(requestData);
            var token = CancellationToken.None;
            var uat = new GetAccountDetailsValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task FailGetAccountDetailsValidator()
        {
            //Arrange
            var requestData = Guid.Empty;
            var request = new GetAccountDetailsQueryRequest(requestData);
            var token = CancellationToken.None;
            var uat = new GetAccountDetailsValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}

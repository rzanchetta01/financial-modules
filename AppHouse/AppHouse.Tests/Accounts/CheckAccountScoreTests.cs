namespace AppHouse.Tests.Accounts
{
    public class CheckAccountScoreTests
    {
        private readonly Mock<IAccountService> _mockAccountService = new();

        [Fact]
        public async Task PassCheckAccountScoreHandler()
        {
            //Arrange
            var data = Guid.NewGuid();
            var request = new CheckAccountScoreQueryRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.FindById(It.IsAny<Guid>(), token)).ReturnsAsync(DummyData.DummyExistingActiveAccountDto);
            var uat = new CheckAccountScoreQueryHandler(_mockAccountService.Object);

            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(DummyData.DummyExistingActiveAccountDto.CreditScore, result);
            _mockAccountService.Verify(m => m.FindById(It.IsAny<Guid>(), token), Times.Once);
        }

        [Fact]
        public async Task FailCheckAccountScoreHandler()
        {
            //Arrange
            var data = Guid.Empty;
            var request = new CheckAccountScoreQueryRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.FindById(data, token)).ReturnsAsync((AccountDto?)null);
            var uat = new CheckAccountScoreQueryHandler(_mockAccountService.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));
            _mockAccountService.Verify(m => m.FindById(It.IsAny<Guid>(), token), Times.Once);
        }

        [Fact]
        public async Task PassCheckAccountScoreValidator()
        {
            //Arrange
            var requestData = Guid.NewGuid();
            var request = new CheckAccountScoreQueryRequest(requestData);
            var token = CancellationToken.None;
            var uat = new CheckAccountScoreValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task FailCheckAccountScoreValidator()
        {
            //Arrange
            var requestData = Guid.Empty;
            var request = new CheckAccountScoreQueryRequest(requestData);
            var token = CancellationToken.None;
            var uat = new CheckAccountScoreValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}

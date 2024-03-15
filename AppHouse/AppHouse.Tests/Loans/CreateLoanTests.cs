using AppHouse.Loans.Application.Handlers.Commands;
using AppHouse.Loans.Application.Validators.Commands;
using AppHouse.Loans.Core;
using AppHouse.Loans.Core.Interfaces;

namespace AppHouse.Tests.Loans
{
    public class CreateLoanTests
    {
        private readonly Mock<ILoanService> _mockLoanService = new();
        private readonly Mock<ILoanRepository> _mockLoanRepository = new();
        private readonly Mock<IMediator> _mockMediator = new();

        [Fact]
        public async Task PassCreateLoanCommandTest()
        {
            //Arrange
            var data = DummyData.DummyNewLoanDto;
            var request = new CreateLoanRequest(data);
            var token = CancellationToken.None;
            var uat = new CreateLoanCommandHandler(_mockLoanService.Object);

            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.True(result);
            Assert.Null(data.Id);
            Assert.Null(data.DateCreated);
            Assert.Null(data.IsActive);
            _mockLoanService.Verify(v => v.Create(It.IsAny<LoanDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailCreateLoanCommandTest()
        {
            //Arrange
            var data = DummyData.DummyExistingActiveLoanDto;
            var request = new CreateLoanRequest(data);
            var token = CancellationToken.None;

            _mockLoanService.Setup(m => m.Create(data, token)).Throws(new Exception("fake exception"));

            var uat = new CreateLoanCommandHandler(_mockLoanService.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));

            Assert.NotNull(data.Id);
            Assert.NotNull(data.DateCreated);
            Assert.NotNull(data.IsActive);
            _mockLoanService.Verify(v => v.Create(It.IsAny<LoanDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task PassCreateLoanValidatorTest()
        {
            //Arrange
            var requestData = DummyData.DummyNewLoanDto;
            var request = new CreateLoanRequest(requestData);
            var token = CancellationToken.None;
            var uat = new CreateLoanValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact]
        public async Task FailCreateLoanValidatorTest()
        {
            //Arrange
            var requestData = DummyData.DummyNewLoanDto;
            var newLoanDto = requestData with { MaxAmount = 0 };

            var request = new CreateLoanRequest(newLoanDto);
            var token = CancellationToken.None;
            var uat = new CreateLoanValidator();

            //Act
            var result = await uat.ValidateAsync(request, token);

            //Assert
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public async Task PassCreateLoanServiceTest()
        {
            //Arrange
            var data = DummyData.DummyNewLoanDto;
            var token = CancellationToken.None;
            var uat = new LoanService(_mockLoanRepository.Object, _mockMediator.Object);

            //Act
            await uat.Create(data, token);

            //Assert
            _mockLoanRepository.Verify(v => v.CreateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEventCreated<LoanDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailCreateLoanServiceTest()
        {
            //Arrange
            var data = DummyData.DummyNewLoanDto;
            var token = CancellationToken.None;
            var uat = new LoanService(_mockLoanRepository.Object, _mockMediator.Object);

            _mockLoanRepository.Setup(m => m.CreateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>())).Throws(new Exception("fake exception"));

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Create(data, token));
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEventCreated<LoanDto>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

    }
}

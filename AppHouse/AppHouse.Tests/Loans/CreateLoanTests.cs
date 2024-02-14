using AppHouse.Loans.Application.Handlers.Commands;
using AppHouse.Loans.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var uat = new CreateLoanCommand(_mockLoanService.Object, _mockMediator.Object);

            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.True(result);
            Assert.Null(data.Id);
            Assert.Null(data.DateCreated);
            Assert.Null(data.IsActive);
            _mockLoanService.Verify(v => v.Create(It.IsAny<LoanDto>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEntityCreated<LoanDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
        
    }
}

﻿

namespace AppHouse.Tests.Accounts
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
            var uat = new CreateAccountCommandHandler(_mockAccountService.Object);
            
            //Act
            var result = await uat.Handle(request, token);

            //Assert
            Assert.True(result);
            Assert.Null(data.Id);
            Assert.Null(data.DateCreated);
            Assert.Null(data.IsActive);
            _mockAccountService.Verify(v => v.Create(It.IsAny<AccountDto>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailCreateAccountCommandTest()
        {
            //Arrange
            var data = DummyData.DummyExistingActiveAccountDto;
            var request = new CreateAccountRequest(data);
            var token = CancellationToken.None;

            _mockAccountService.Setup(m => m.Create(data, token)).Throws(new Exception("fake exception"));

            var uat = new CreateAccountCommandHandler(_mockAccountService.Object);

            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.Handle(request, token));

            Assert.NotNull(data.Id);
            Assert.NotNull(data.DateCreated);
            Assert.NotNull(data.IsActive);
            _mockAccountService.Verify(v => v.Create(It.IsAny<AccountDto>(), It.IsAny<CancellationToken>()), Times.Once);
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

            var uat = new AccountService(_mockAccountRepository.Object, _mockMediator.Object);
            
            //Act
            await uat.Create(data, token);

            //Assert
            _mockAccountRepository.Verify(m => m.CreateAsync(It.IsAny<Account>(), token), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEventCreated<AccountDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task FailCreateAccountServiceTest()
        {
            //Arrange
            var data = DummyData.DummyExistingDisabledAccountDto;
            var token = CancellationToken.None;

            _mockAccountRepository.Setup(m => m.CreateAsync(It.IsAny<Account>(), token)).Throws(new Exception("fake exception"));

            var uat = new AccountService(_mockAccountRepository.Object, _mockMediator.Object);
        
            //Act and Assert
            await Assert.ThrowsAnyAsync<Exception>(async () =>  await uat.Create(data, token));
            _mockAccountRepository.Verify(m => m.CreateAsync(It.IsAny<Account>(), token), Times.Once);
            _mockMediator.Verify(v => v.Publish(It.IsAny<TEventCreated<AccountDto>>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Theory]
        [InlineData(1, "testName", "1987-12-12", null, 1000)]
        [InlineData(2, "test Name", "1987-12-12", null, 1000)]
        [InlineData(5, "test Name", "1987-12-12", "assfdafdf", 19000)]
        public void PassDefineStartAccountRating( int expected, string name, string birthdate, string? addressComplement, decimal income)
        {
            //Arrange
            var dto = new AccountDto(
                Name: name,
                Email: "test@email.com",
                Password: "testPassword",
                Cellphone: "1197264321",
                BirthDate: birthdate,
                Country: "testCountry",
                State: "testState",
                City: "testCity",
                PostalCode: "02582123",
                Address: "testAddressWithFakeCountryStateAndCity",
                AddressComplement: addressComplement,
                Income: income,
                CreditScore: 0D,
                Id: null,
                DateCreated: null,
                IsActive: null
                );

            var uat = new AccountService(_mockAccountRepository.Object, _mockMediator.Object);

            //Act
            var result = uat.DefineStartAccountRating(dto);

            //Assert
            Assert.Equal(expected, result);

        }
    }
}
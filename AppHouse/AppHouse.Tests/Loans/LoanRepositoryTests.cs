using AppHouse.Loans.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppHouse.Tests.Loans
{
    public class LoanRepositoryTests
    {
        [Fact]
        public async Task PassCreateRangeAsyncTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LoanContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new LoanContext(options);

            var uat = new LoanRepository(context);
            var data = LoanMapping.Map(DummyData.DummyNewLoanDto);
            var token = CancellationToken.None;

            //Act
            await uat.CreateRangeAsync(new List<Loan> { data }, token);

            //Assert
            Assert.Equal(1, await context.Loans.CountAsync());

        }

        [Fact]
        public async Task FailCreateRangeAsyncTest()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LoanContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new LoanContext(options);

            var uat = new LoanRepository(context);
            var data = LoanMapping.Map(DummyData.DummyNewLoanDto);
            var token = CancellationToken.None;

            //Act
            await uat.CreateRangeAsync(new List<Loan> { data }, token);

            //Assert
            await Assert.ThrowsAnyAsync<Exception>(async () => await uat.CreateRangeAsync(new List<Loan> { data }, token));
            Assert.Equal(1, await context.Loans.CountAsync());
        }
        
    }
}

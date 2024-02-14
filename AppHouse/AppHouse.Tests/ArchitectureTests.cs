using NetArchTest.Rules;

namespace AppHouse.Tests
{
    public class ArchitectureTests
    {
        private readonly Assembly _accountCore = typeof(AppHouse.Accounts.Core.AccountService).Assembly;
        private readonly Assembly _paymentCore = typeof(AppHouse.Payments.Core.Class1).Assembly;//TEMP
        private readonly Assembly _loanCore = typeof(AppHouse.Loans.Core.LoanService).Assembly;

        private readonly Assembly _accountApplication = typeof(AppHouse.Accounts.Application.Init).Assembly;
        private readonly Assembly _paymentApplication = typeof(AppHouse.Payments.Application.Class1).Assembly;//TEMP
        private readonly Assembly _loanApplication = typeof(AppHouse.Loans.Application.Init).Assembly;

        private readonly Assembly _sharedKernel = typeof(AppHouse.SharedKernel.Init).Assembly;

        private readonly Assembly _gateway = typeof(AppHouse.Gateway.Init).Assembly;


        [Fact]
        public void AccountApplicationArcTests()
        {

            var handlers = Types.InAssembly(_accountApplication)
                .That()
                .HaveNameEndingWith("Handler")
                .Should()
                .BePublic()
                .And()
                .ImplementInterface(typeof(IRequestHandler<,>))
                .GetResult();

            var validators = Types.InAssembly(_accountApplication)
                .That()
                .HaveNameEndingWith("Validator")
                .Should()
                .BePublic()
                .And()
                .Inherit(typeof(AbstractValidator<>))
                .GetResult();

            var references = Types.InAssembly(_accountApplication)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                _paymentApplication.FullName,
                _paymentCore.FullName,
                _loanApplication.FullName,
                _loanCore.FullName,
                _gateway.FullName,
                _loanApplication.FullName
                )
                .GetResult();

            Assert.True(handlers.IsSuccessful);
            Assert.True(validators.IsSuccessful);
            Assert.True(references.IsSuccessful);
        }

        [Fact]
        public void AccountCoreArcTests()
        {
            var classes = Types.InAssembly(_accountCore)
                .That()
                .HaveNameStartingWith("Account")
                .Should()
                .BePublic()
                .And()
                .NotBeInterfaces()
                .And()
                .BeClasses()
                .And()
                .HaveNameStartingWith("Account")
                .GetResult();

            var interfaces = Types.InAssembly(_accountCore)
                .That()
                .ResideInNamespace(_accountCore.FullName + "interfaces")
                .Should()
                .BePublic()
                .And()
                .BeInterfaces()
                .And()
                .HaveNameStartingWith("I")
                .GetResult();

            var references = Types.InAssembly(_accountCore)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                _paymentCore.FullName,
                _loanCore.FullName,
                _accountApplication.FullName,
                _paymentApplication.FullName,
                _loanApplication.FullName,
                _gateway.FullName
                )
                .GetResult();

            Assert.True(classes.IsSuccessful);
            Assert.True (interfaces.IsSuccessful);
            Assert.True(references.IsSuccessful);
        }

        [Fact]
        public void SharedKernelArcTests()
        {
            /*var baseClasses;
            var dtos;
            var entities;
            var interfaces;
            var sharedrequests;
            var references;*/
        }
    }
}

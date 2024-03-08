using AppHouse.SharedKernel.BaseClasses;
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
        public void ApplicationArcGenericTests()
        {
            List<Assembly> assemblyList = [_accountApplication, _paymentApplication, _loanApplication];

            var events = Types
                .InAssemblies(assemblyList)
                .That()
                .ResideInNamespaceEndingWith("Events")
                .Should()
                .BePublic()
                .And()
                .HaveNameEndingWith("Event")
                .And()
                .ImplementInterface(typeof(INotificationHandler<>))
                .And()
                .BeClasses()
                .GetResult();

            var handlers = Types
                .InAssemblies(assemblyList)
                .That()
                .ResideInNamespaceEndingWith("Handlers")
                .Should()
                .BePublic()
                .And()
                .HaveNameEndingWith("Handler")
                .And()
                .ImplementInterface(typeof(IRequestHandler<,>))
                .And()
                .BeClasses()
                .GetResult();
            
            var validators = Types
                .InAssemblies(assemblyList)
                .That()
                .ResideInNamespaceEndingWith("Validators")
                .Should()
                .BePublic()
                .And()
                .HaveNameEndingWith("Validator")
                .And()
                .Inherit(typeof(AbstractValidator<>))
                .And()
                .BeClasses()
                .GetResult();


            Assert.True(events.IsSuccessful, string.Join("{fail}", FormatErrorMessage(events)));            
            Assert.True(handlers.IsSuccessful, string.Join("{fail}", FormatErrorMessage(handlers)));
            Assert.True(validators.IsSuccessful, string.Join("{fail}", FormatErrorMessage(validators)));
        }
        [Fact]
        public void CoreArcGenericTests()
        {
            List<Assembly> assemblies = [_accountCore, _loanCore, _paymentCore];
                                         
            var classes = Types.InAssemblies(assemblies)
                .That()
                .DoNotResideInNamespaceEndingWith("Interfaces")
                .Should()
                .BePublic()
                .And()
                .BeClasses()
                .GetResult();

            var interfaces = Types.InAssemblies(assemblies)
                .That()
                .ResideInNamespaceEndingWith("Interfaces")
                .Should()
                .BePublic()
                .And()
                .BeInterfaces()
                .And()
                .HaveNameStartingWith("I")
                .GetResult();

            Assert.True(classes.IsSuccessful, FormatErrorMessage(classes));
            Assert.True(interfaces.IsSuccessful, FormatErrorMessage(interfaces));
        }

        [Fact]
        public void AccountApplicationArcTests()
        {
           
            var references = Types.InAssembly(_accountApplication)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                    _paymentCore.FullName,
                    _loanCore.FullName,
                    _paymentApplication.FullName,
                    _loanApplication.FullName,
                    _gateway.FullName
                )
                .GetResult();

            Assert.True(references.IsSuccessful, FormatErrorMessage(references));
        }
        [Fact]
        public void AccountCoreArcTests()
        {
            
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

            Assert.True(references.IsSuccessful);
        }


        [Fact]
        public void LoanApplicationArcTests()
        {
            var references = Types.InAssembly(_loanApplication)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                    _accountCore.FullName,
                    _paymentCore.FullName,
                    _accountApplication.FullName,
                    _paymentApplication.FullName,
                    _gateway.FullName
                )
                .GetResult();

            Assert.True(references.IsSuccessful, FormatErrorMessage(references));
        }
        [Fact]
        public void LoanCoreArcTests()
        {
            var references = Types.InAssembly(_loanCore)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                    _accountCore.FullName,
                    _paymentCore.FullName,
                    _accountApplication.FullName,
                    _paymentApplication.FullName,
                    _loanApplication.FullName,
                    _gateway.FullName
                )
                .GetResult();

            Assert.True(references.IsSuccessful);
        }


        [Fact]
        public void PaymentsApplicationArcTests()
        {
            var references = Types.InAssembly(_paymentApplication)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                    _accountCore.FullName,
                    _loanCore.FullName,
                    _accountApplication.FullName,
                    _loanApplication.FullName,
                    _gateway.FullName
                )
                .GetResult();

            Assert.True(references.IsSuccessful);
        }
        [Fact]
        public void PaymentsCoreArcTests()
        {
            var references = Types.InAssembly(_paymentCore)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                    _accountCore.FullName,
                    _loanCore.FullName,
                    _accountApplication.FullName,
                    _paymentApplication.FullName,
                    _loanApplication.FullName,
                    _gateway.FullName
                )
                .GetResult();

            Assert.True(references.IsSuccessful);
        }
        
        
        [Fact]
        public void SharedKernelArcTests()
        {
            var baseClasses = Types
                .InAssembly(_sharedKernel)
                .That()
                .ResideInNamespaceEndingWith("BaseClasses")
                .Should()
                .BeAbstract()
                .And()
                .BeClasses()
                .And()
                .BePublic()
                .And()
                .HaveNameStartingWith("Base")
                .GetResult();
            
            var dtos = Types
                .InAssembly(_sharedKernel)
                .That()
                .ResideInNamespaceEndingWith("DTOs")
                .Should()
                .Inherit(typeof(BaseDto))
                .And()
                .BeClasses()
                .And()
                .BePublic()
                .And()
                .NotBeAbstract()
                .And()
                .HaveNameEndingWith("Dto")
                .GetResult();


            var entities = Types
                .InAssembly(_sharedKernel)
                .That()
                .ResideInNamespaceEndingWith("Entities")
                .Should()
                .Inherit(typeof(BaseEntity))
                .And()
                .BeClasses()
                .And()
                .NotBeAbstract()
                .And()
                .BePublic()
                .Or()
                .BeEnums()
                .GetResult();
                
            var interfaces = Types
                .InAssembly(_sharedKernel)
                .That()
                .ResideInNamespaceEndingWith("Interfaces")
                .Should()
                .HaveNameStartingWith("I")
                .And()
                .BeInterfaces()
                .And()
                .BePublic()
                .GetResult();
                
            var sharedRequests = Types
                .InAssembly(_sharedKernel)
                .That()
                .ResideInNamespaceContaining("SharedRequests")
                .Should()
                .BePublic()
                .And()
                .NotBeAbstract()
                .And()
                .BeClasses()
                .And()
                .ImplementInterface(typeof(IRequest<>))
                .And()
                .HaveNameEndingWith("Request")
                .GetResult();

            var references = Types
                .InAssembly(_sharedKernel)
                .ShouldNot()
                .HaveDependencyOnAny
                (
                    _accountCore.FullName,
                    _paymentCore.FullName,
                    _loanCore.FullName,
                    _accountApplication.FullName,
                    _paymentApplication.FullName,
                    _loanApplication.FullName,
                    _gateway.FullName
                )
                .GetResult();

            Assert.True(baseClasses.IsSuccessful, FormatErrorMessage(baseClasses));
            Assert.True(dtos.IsSuccessful, FormatErrorMessage(dtos));
            Assert.True(entities.IsSuccessful, FormatErrorMessage(entities));
            Assert.True(interfaces.IsSuccessful, FormatErrorMessage(interfaces));
            Assert.True(sharedRequests.IsSuccessful, FormatErrorMessage(sharedRequests));
            Assert.True(references.IsSuccessful, FormatErrorMessage(references));
        }

        private static string FormatErrorMessage(TestResult test)
        {
            StringBuilder sb = new();

            foreach (var item in test.FailingTypes)
            {
                sb.AppendLine($"CLASS {item.FullName} | ERROR {item.Explanation}");
            }

            return sb.ToString();
        }
    }
}

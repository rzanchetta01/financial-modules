using AppHouse.Loans.Application.Validators.Commands;
using AppHouse.Loans.Core;
using AppHouse.Loans.Core.Interfaces;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace AppHouse.Loans.Application
{
    public static class StartupServices
    {
        public static IServiceCollection AddLoansStartup(this IServiceCollection services)
        {
            services.AddRepositories();
            services.AddServices();
            services.AddValidators();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILoanRepository, LoanRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILoanService, LoanService>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateLoanRequest>, CreateLoanValidator>();
            return services;
        }
    }
}

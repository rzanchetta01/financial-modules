using AppHouse.Loans.Core;
using AppHouse.Loans.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return services;
        }
    }
}

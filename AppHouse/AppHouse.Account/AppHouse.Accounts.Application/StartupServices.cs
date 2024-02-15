using AppHouse.Accounts.Core;
using AppHouse.Accounts.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using AppHouse.Accounts.Application.Validators;
using AppHouse.SharedKernel.SharedRequests.SharedCommands;
using AppHouse.AccountActivityHistories.Core;

namespace AppHouse.Accounts.Application
{
    public static class StartupServices
    {
        public static IServiceCollection AddAccountStartup(this IServiceCollection services)
        {
            services.AddValidators();
            services.AddServices();
            services.AddRepositories();
            
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountActivityHistoryRepository, AccountActivityHistoryRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountActivityHistoryService, AccountActivityHistoryService>();
            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateAccountRequest>, CreateAccountValidator>();
            services.AddScoped<IValidator<UpdateAccountRequest>, UpdateAccountValidator>();
            return services;
        }
    }
}

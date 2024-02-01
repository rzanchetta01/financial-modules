using AppHouse.Accounts.Core;
using AppHouse.Accounts.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using AppHouse.Accounts.Domain.Dto;
using AppHouse.Accounts.Domain.Mapping;

namespace AppHouse.Accounts.Application
{
    public static class StartupServices
    {
        public static IServiceCollection AddAccountStartup(this IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
            
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IValidator<AccountDto>, AccountDtoValidator>();
            return services;
        }
    }
}

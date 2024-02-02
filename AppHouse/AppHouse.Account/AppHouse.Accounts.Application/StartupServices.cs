using AppHouse.Accounts.Core;
using AppHouse.Accounts.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using AppHouse.Accounts.Application.Requests.Commands;
using MediatR;
using AppHouse.Accounts.Application.Validators;

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
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
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

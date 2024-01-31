using AppHouse.Accounts.Domain.Dto;
using AppHouse.Accounts.Domain.Entity;
using Mapster;

namespace AppHouse.Accounts.Domain.Mapping
{
    internal static class AccountMapping
    {
        public static void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AccountDto, Account>()
                .Map(dest => dest.BirthDate, src => DateOnly.FromDateTime(src.BirthDate));
        }
    }
}

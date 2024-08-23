using divas_dynasty.game;
using divas_dynasty.wallet;
using Microsoft.Extensions.DependencyInjection;

namespace divas_dynasty.shared;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<Wallet>();
        services.AddSingleton<Game>();
        services.AddSingleton<IValidator<decimal>, DepositValidator>();
        services.AddSingleton<IValidator<WithdrawData>, WithdrawValidator>();
        services.AddSingleton<IValidator<BettingData>, BettingValidator>();
        services.AddSingleton<DepositHandler>();
        services.AddSingleton<WithdrawHandler>();
        services.AddSingleton<BetHandler>();
        services.AddSingleton<GameHandler>();
    }

}

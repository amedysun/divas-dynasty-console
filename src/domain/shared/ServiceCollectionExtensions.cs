using domain.game;
using domain.wallet;
using Microsoft.Extensions.DependencyInjection;

namespace domain.shared
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<Random>();
            services.AddSingleton<Wallet>();
            services.AddSingleton<Game>();
            services.AddSingleton<IValidator<decimal>, DepositValidator>();
            services.AddSingleton<IValidator<WithdrawData>, WithdrawValidator>();
            services.AddSingleton<IValidator<BettingData>, BettingValidator>();
            services.AddSingleton<IHandler<DepositCommand>, DepositHandler>();
            services.AddSingleton<IHandler<WithdrawCommand>, WithdrawHandler>();
            services.AddSingleton<IHandler<BetCommand>, BetHandler>();
            services.AddSingleton<GameHandler>();
        }
    }
}

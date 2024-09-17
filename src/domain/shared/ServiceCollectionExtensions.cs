using domain.bet;
using domain.game;
using domain.game.console;
using domain.wallet;
using domain.wallet.deposit;
using domain.wallet.withdraw;
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
            services.AddSingleton<IHandler<DepositCommand, DepositResponse>, DepositHandler>();
            services.AddSingleton<IHandler<WithdrawCommand, WithdrawResponse>, WithdrawHandler>();
            services.AddSingleton<IHandler<BetCommand, BetResponse>, BetHandler>();
            services.AddSingleton<GameHandler>();
        }
    }
}

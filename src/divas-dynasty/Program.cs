using Microsoft.Extensions.DependencyInjection;
using Serilog;
using divas_dynasty.game;
using divas_dynasty.wallet;
using divas_dynasty.shared;

namespace divas_dynasty;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var depositHandler = serviceProvider.GetRequiredService<DepositHandler>();
        var withdrawHandler = serviceProvider.GetRequiredService<WithdrawHandler>();
        var betHandler = serviceProvider.GetRequiredService<BetHandler>();

        bool running = true;

        while (running)
        {
            Console.WriteLine("Please, submit action:");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Log.Error("No input received. Please try again.");
                continue;
            }

            string[] parts = input.Split(' ');

            try
            {
                switch (parts[0].ToLower())
                {
                    case "deposit":
                        if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal depositAmount))
                        {
                            depositHandler.Handle(depositAmount);
                        }
                        else
                        {
                            Log.Error("Invalid deposit amount.");
                        }
                        break;
                    case "withdraw":
                        if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal withdrawAmount))
                        {
                            withdrawHandler.Handle(withdrawAmount);
                        }
                        else
                        {
                            Log.Error("Invalid withdrawal amount.");
                        }
                        break;
                    case "bet":
                        if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal betAmount))
                        {
                            betHandler.Handle(betAmount);
                        }
                        else
                        {
                            Log.Error("Invalid bet amount.");
                        }
                        break;
                    case "exit":
                        running = false;
                        Console.WriteLine("Thank you for playing Wonder Woman! Hope to see you again soon.");
                        Environment.Exit(0);
                        break;
                    default:
                        Log.Error("Invalid action. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
            }
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<Wallet>();
        services.AddSingleton<Game>();
        services.AddSingleton<IValidator<decimal>, DepositValidator>();
        services.AddSingleton<IValidator<WithdrawData>, WithdrawValidator>();
        services.AddSingleton<IValidator<BettingData>, BettingValidator>();
        services.AddSingleton<DepositHandler>();
        services.AddSingleton<WithdrawHandler>();
        services.AddSingleton<BetHandler>();
    }
}

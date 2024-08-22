using Serilog;

using divas_dynasty.game;
using divas_dynasty.wallet;

namespace divas_dynasty;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        Wallet wallet = new();
        Game game = new();

        DepositValidator depositValidator = new();
        WithdrawValidator withdrawValidator = new();
        BettingValidator bettingValidator = new();

        DepositHandler depositHandler = new(wallet, depositValidator);
        WithdrawHandler withdrawHandler = new(wallet, withdrawValidator);
        BetHandler betHandler = new(wallet, game, bettingValidator);

        bool running = true;

        while (running)
        {
            Console.WriteLine("Please, submit action:");
            string? input = Console.ReadLine();
            if (input == null)
            {
                Console.WriteLine("No input received. Please try again.");
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
                            Console.WriteLine("Invalid deposit amount.");
                        }
                        break;
                    case "withdraw":
                        if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal withdrawAmount))
                        {
                            withdrawHandler.Handle(withdrawAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid withdrawal amount.");
                        }
                        break;
                    case "bet":
                        if (parts.Length == 2 && decimal.TryParse(parts[1], out decimal betAmount))
                        {
                            betHandler.Handle(betAmount);
                        }
                        else
                        {
                            Console.WriteLine("Invalid bet amount.");
                        }
                        break;
                    case "exit":
                        running = false;
                        Console.WriteLine("Thank you for playing Wonder Woman! Hope to see you again soon.");
                        break;
                    default:
                        Console.WriteLine("Invalid action. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}

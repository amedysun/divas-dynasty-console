using Serilog;
using divas_dynasty.wallet;

namespace divas_dynasty.game;

public class GameHandler(DepositHandler depositHandler, WithdrawHandler withdrawHandler, BetHandler betHandler)
{
    private readonly DepositHandler _depositHandler = depositHandler;
    private readonly WithdrawHandler _withdrawHandler = withdrawHandler;
    private readonly BetHandler _betHandler = betHandler;

    public void Handle()
    {
        while (true)
        {
            Console.WriteLine("Please, submit action:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Log.Error("No input received. Please try again.");
                continue;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Log.Error("Invalid command format. Please try again.");
                continue;
            }

            try
            {
                ProcessCommand(parts);
            }
            catch (Exception ex)
            {
                Log.Error($"{ex.Message}");
            }
        }
    }

    private void ProcessCommand(string[] parts)
    {
        string action = parts[0].ToLower();
        if (!decimal.TryParse(parts[1], out decimal amount))
        {
            Log.Error("Invalid amount.");
            return;
        }

        switch (action)
        {
            case "deposit":
                _depositHandler.Handle(amount);
                break;
            case "withdraw":
                _withdrawHandler.Handle(amount);
                break;
            case "bet":
                _betHandler.Handle(amount);
                break;
            case "exit":
                Console.WriteLine("Thank you for playing Wonder Woman! Hope to see you again soon.");
                Environment.Exit(0);
                break;
            default:
                Log.Error("Invalid action. Please try again.");
                break;
        }
    }
}

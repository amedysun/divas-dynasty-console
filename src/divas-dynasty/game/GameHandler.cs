using divas_dynasty.wallet;
using divas_dynasty.shared;

namespace divas_dynasty.game;

public class GameHandler(
    IHandler<DepositCommand> depositHandler,
    IHandler<WithdrawCommand> withdrawHandler,
    IHandler<BetCommand> betHandler
    ) : IHandler<string>
{

    public void Handle(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new InvalidOperationException("No input received. Please try again.");
        }

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.First() is "exit")
        {
            Console.WriteLine("Thank you for playing Wonder Woman! Hope to see you again soon.");
            Environment.Exit(0);
        }

        if (parts.Length < 2)
        {
            throw new InvalidOperationException("Invalid command format. Please try again.");
        }

        string action = parts[0].ToLower();
        if (!decimal.TryParse(parts[1], out decimal amount))
        {
            throw new InvalidOperationException("Invalid amount.");
        }

        switch (action)
        {
            case "deposit":
                depositHandler.Handle(new DepositCommand(amount));
                break;
            case "withdraw":
                withdrawHandler.Handle(new WithdrawCommand(amount));
                break;
            case "bet":
                betHandler.Handle(new BetCommand(amount));
                break;
            default:
                throw new InvalidOperationException("Invalid action. Please try again.");
        }
    }
}

using domain.wallet;
using domain.shared;

namespace domain.game
{
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

            if (!Enum.TryParse(parts[0], true, out GameAction action))
            {
                throw new InvalidOperationException("Invalid action. Please try again.");
            }

            if (action is GameAction.Exit)
            {
                Console.WriteLine("Thank you for playing Wonder Woman! Hope to see you again soon.");
                Environment.Exit(0);
            }

            if (parts.Length < 2)
            {
                throw new InvalidOperationException("Invalid command format. Please try again.");
            }

            if (!decimal.TryParse(parts[1], out decimal amount))
            {
                throw new InvalidOperationException("Invalid amount.");
            }

            switch (action)
            {
                case GameAction.Deposit:
                    depositHandler.Handle(new DepositCommand(amount));
                    break;
                case GameAction.Withdraw:
                    withdrawHandler.Handle(new WithdrawCommand(amount));
                    break;
                case GameAction.Bet:
                    betHandler.Handle(new BetCommand(amount));
                    break;
                default:
                    throw new InvalidOperationException("Invalid action. Please try again.");
            }
        }
    }
}

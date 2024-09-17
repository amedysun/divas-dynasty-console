using domain.bet;
using domain.shared;
using domain.wallet.deposit;
using domain.wallet.withdraw;

namespace domain.game.console
{
    public class GameHandler(
        IHandler<DepositCommand, DepositResponse> depositHandler,
        IHandler<WithdrawCommand, WithdrawResponse> withdrawHandler,
        IHandler<BetCommand, BetResponse> betHandler
        ) : IHandler<GameCommand, GameResponse>
    {
        public GameResponse Handle(GameCommand command)
        {
            string input = command.Input;
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

            string message;

            switch (action)
            {
                case GameAction.Deposit:
                    message = depositHandler.Handle(new DepositCommand(amount)).Message;
                    break;
                case GameAction.Withdraw:
                    message = withdrawHandler.Handle(new WithdrawCommand(amount)).Message;
                    break;
                case GameAction.Bet:
                    message = betHandler.Handle(new BetCommand(amount)).Message;
                    break;
                default:
                    throw new InvalidOperationException("Invalid action. Please try again.");
            }

            return new GameResponse(message);
        }
    }
}

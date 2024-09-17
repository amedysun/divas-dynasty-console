using domain.game;
using domain.shared;
using domain.wallet;

namespace domain.bet;

public class BetHandler(Wallet wallet, Game game, IValidator<BettingData> validator) : IHandler<BetCommand, BetResponse>
{
    public BetResponse Handle(BetCommand command)
    {
        var betAmount = command.Amount;
        validator.Validate(new BettingData(betAmount, wallet.Balance));
        BetResult betResult = game.PlaceBet(betAmount);
        wallet.UpdateBalance(betAmount, betResult.WinAmount);

        return new BetResponse(betResult.IsWin
            ? $"You won: ${betResult.WinAmount:F2}. Current balance: ${wallet.Balance:F2}"
            : $"You lost the bet. Current balance: ${wallet.Balance:F2}");
    }
}

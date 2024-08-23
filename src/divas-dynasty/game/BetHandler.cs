using divas_dynasty.shared;
using divas_dynasty.wallet;
using Serilog;

namespace divas_dynasty.game;

public class BetHandler(Wallet wallet, Game game, IValidator<BettingData> validator) : IHandler<BetCommand>
{
    public void Handle(BetCommand command)
    {
        var betAmount = command.Amount;
        var walletBalance = wallet.Balance;
        validator.Validate(new BettingData(betAmount, walletBalance));
        BetResult betResult = game.PlaceBet(betAmount);
        wallet.UpdateBalance(betAmount, betResult.WinAmount);

        Log.Information(betResult.IsWin
            ? $"You won: ${betResult.WinAmount:F2}. Current balance: ${walletBalance:F2}"
            : $"You lost the bet. Current balance: ${walletBalance:F2}");
    }
}

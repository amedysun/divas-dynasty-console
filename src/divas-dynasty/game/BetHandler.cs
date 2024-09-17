using divas_dynasty.shared;
using divas_dynasty.wallet;
using Serilog;

namespace divas_dynasty.game;

public class BetHandler(Wallet wallet, Game game, IValidator<BettingData> validator) : IHandler<BetCommand>
{
    public void Handle(BetCommand command)
    {
        var betAmount = command.Amount;
        validator.Validate(new BettingData(betAmount, wallet.Balance));
        BetResult betResult = game.PlaceBet(betAmount);
        wallet.UpdateBalance(betAmount, betResult.WinAmount);

        Log.Information(betResult.IsWin
            ? $"You won: ${betResult.WinAmount:F2}. Current balance: ${wallet.Balance:F2}"
            : $"You lost the bet. Current balance: ${wallet.Balance:F2}");
    }
}

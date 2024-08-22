using divas_dynasty.shared;
using divas_dynasty.wallet;
using Serilog;

namespace divas_dynasty.game;

public class BetHandler(Wallet wallet, Game game, IValidator<BettingData> validator) : IHandler<decimal>
{
    private readonly Wallet _wallet = wallet;
    private readonly Game _game = game;
    private readonly IValidator<BettingData> _validator = validator;

    public void Handle(decimal betAmount)
    {
        _validator.Validate(new BettingData(betAmount, _wallet.Balance));
        BetResult betResult = _game.PlaceBet(betAmount);
        _wallet.UpdateBalance(betAmount, betResult.WinAmount);

        Log.Information(betResult.IsWin
            ? $"You won: ${betResult.WinAmount:F2}. Current balance: ${_wallet.Balance:F2}"
            : $"You lost the bet. Current balance: ${_wallet.Balance:F2}");
    }
}

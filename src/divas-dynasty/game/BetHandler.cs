using divas_dynasty.shared;
using divas_dynasty.wallet;
using Serilog;

namespace divas_dynasty.game;

public class BetHandler(Wallet wallet, Game game, IValidator<decimal> validator) : IHandler<decimal>
{
    private readonly Wallet _wallet = wallet;
    private readonly Game _game = game;
    private readonly IValidator<decimal> _validator = validator;

    public void Handle(decimal betAmount)
    {
        _validator.Validate(betAmount);
        decimal winAmount = _game.PlaceBet(betAmount);
        _wallet.UpdateBalance(betAmount, winAmount);

        switch (winAmount)
        {
            case > 0:
                Log.Information($"Congrats you won ${winAmount:F2}! Your current balance is: ${_wallet.Balance:F2}");
                break;
            default:
                Log.Information($"No luck this time! Your current balance is: ${_wallet.Balance:F2}");
                break;
        }
    }
}

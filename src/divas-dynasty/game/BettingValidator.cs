using divas_dynasty.shared;

namespace divas_dynasty.game;

public class BettingValidator : IValidator<BettingData>
{
    public void Validate(BettingData data)
    {
        if (data.BetAmount is < 1 or > 10)
        {
            throw new ArgumentException("Bet amount must be between $1 and $10.");
        }

        if(data.Balance < data.BetAmount)
        {
            throw new ArgumentException("Insufficient balance to place the bet.");
        }
    }
}

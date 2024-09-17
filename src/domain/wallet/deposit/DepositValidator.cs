using domain.shared;

namespace domain.wallet.deposit;

public class DepositValidator : IValidator<decimal>
{
    public void Validate(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be positive.");
        }
    }
}

using divas_dynasty.shared;

namespace divas_dynasty.wallet;

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

using domain.shared;

namespace domain.wallet;

public class WithdrawValidator : IValidator<WithdrawData>
{
    public void Validate(WithdrawData value)
    {
        if (value.WithdrawAmount <= 0)
        {
            throw new ArgumentException("Withdraw amount must be greater than zero.");
        }

        if (value.WithdrawAmount > value.Balance)
        {
            throw new ArgumentException("Insufficient balance to withdraw the requested amount.");
        }
    }
}

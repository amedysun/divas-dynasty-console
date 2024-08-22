using divas_dynasty.shared;

namespace divas_dynasty.wallet;
public class WithdrawHandler(Wallet wallet, IValidator<decimal> validator) : IHandler<decimal>
{
    private readonly Wallet _wallet = wallet;
    private readonly IValidator<decimal> _validator = validator;

    public void Handle(decimal amount)
    {
        _validator.Validate(amount);
        _wallet.Withdraw(amount);
    }
}

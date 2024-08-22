using divas_dynasty.shared;
using Serilog;

namespace divas_dynasty.wallet;

public class WithdrawHandler(Wallet wallet, IValidator<WithdrawData> validator) : IHandler<decimal>
{
    private readonly Wallet _wallet = wallet;
    private readonly IValidator<WithdrawData> _validator = validator;

    public void Handle(decimal amount)
    {
        _validator.Validate(new WithdrawData(amount, _wallet.Balance));
        _wallet.Withdraw(amount);
        Log.Information($"Your withdrawal of ${amount:F2} was successful. Your current balance is: ${_wallet.Balance:F2}");
    }
}

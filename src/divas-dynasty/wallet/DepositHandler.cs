using divas_dynasty.shared;
using Serilog;

namespace divas_dynasty.wallet;

public class DepositHandler(Wallet wallet, IValidator<decimal> validator) : IHandler<decimal>
{
    private readonly Wallet _wallet = wallet;
    private readonly IValidator<decimal> _validator = validator;

    public void Handle(decimal amount)
    {
        _validator.Validate(amount);
        _wallet.Deposit(amount);
        Log.Information($"Your deposit of ${amount} was successful. Your current balance is: ${_wallet.Balance}");
    }
}

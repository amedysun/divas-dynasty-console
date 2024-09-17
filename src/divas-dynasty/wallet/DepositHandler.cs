using divas_dynasty.shared;
using Serilog;

namespace divas_dynasty.wallet;

public class DepositHandler(Wallet wallet, IValidator<decimal> validator) : IHandler<DepositCommand>
{
    public void Handle(DepositCommand command)
    {
        var depositAmount = command.Amount;
        validator.Validate(depositAmount);
        wallet.Deposit(depositAmount);
        Log.Information($"Your deposit of ${depositAmount:F2} was successful. Your current balance is: ${wallet.Balance:F2}");
    }
}

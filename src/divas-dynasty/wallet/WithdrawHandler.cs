using divas_dynasty.shared;
using Serilog;

namespace divas_dynasty.wallet;

public class WithdrawHandler(Wallet wallet, IValidator<WithdrawData> validator) : IHandler<WithdrawCommand>
{
    public void Handle(WithdrawCommand command)
    {
        var withdrawAmount = command.Amount;
        var walletBalance = wallet.Balance;
        validator.Validate(new WithdrawData(withdrawAmount, walletBalance));
        wallet.Withdraw(withdrawAmount);
        Log.Information($"Your withdrawal of ${withdrawAmount:F2} was successful. Your current balance is: ${walletBalance:F2}");
    }
}

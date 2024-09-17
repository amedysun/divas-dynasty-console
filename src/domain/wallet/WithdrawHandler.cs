using domain.shared;
using Serilog;

namespace domain.wallet;

public class WithdrawHandler(Wallet wallet, IValidator<WithdrawData> validator) : IHandler<WithdrawCommand>
{
    public void Handle(WithdrawCommand command)
    {
        var withdrawAmount = command.Amount;
        validator.Validate(new WithdrawData(withdrawAmount, wallet.Balance));
        wallet.Withdraw(withdrawAmount);
        Log.Information($"Your withdrawal of ${withdrawAmount:F2} was successful. Your current balance is: ${wallet.Balance:F2}");
    }
}

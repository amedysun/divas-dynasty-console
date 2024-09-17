using domain.shared;

namespace domain.wallet.withdraw;

public class WithdrawHandler(Wallet wallet, IValidator<WithdrawData> validator) : IHandler<WithdrawCommand, WithdrawResponse>
{
    public WithdrawResponse Handle(WithdrawCommand command)
    {
        var withdrawAmount = command.Amount;
        validator.Validate(new WithdrawData(withdrawAmount, wallet.Balance));
        wallet.Withdraw(withdrawAmount);
        return new WithdrawResponse($"Your withdrawal of ${withdrawAmount:F2} was successful. Your current balance is: ${wallet.Balance:F2}");
    }
}

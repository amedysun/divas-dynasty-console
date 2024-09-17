using domain.shared;

namespace domain.wallet.deposit;

public class DepositHandler(Wallet wallet, IValidator<decimal> validator) : IHandler<DepositCommand, DepositResponse>
{
    public DepositResponse Handle(DepositCommand command)
    {
        var depositAmount = command.Amount;
        validator.Validate(depositAmount);
        wallet.Deposit(depositAmount);
        return new DepositResponse($"Your deposit of ${depositAmount:F2} was successful. Your current balance is: ${wallet.Balance:F2}");
    }
}

using Serilog;

namespace divas_dynasty.wallet;

public class Wallet
{
    public decimal Balance { get; private set; }

    public Wallet() =>
        Balance = 0;

    public void Deposit(decimal amount)
    {
        Balance += amount;
        Log.Information($"Your deposit of ${amount:F2} was successful. Your current balance is: ${Balance:F2}");
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
        Log.Information($"Your withdrawal of ${amount:F2} was successful. Your current balance is: ${Balance:F2}");
    }

    public void UpdateBalance(decimal betAmount, decimal winAmount)
    {
        Balance = Balance - betAmount + winAmount;
    }
}
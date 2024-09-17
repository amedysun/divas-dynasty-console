namespace domain.wallet;

public class Wallet
{
    public decimal Balance { get; private set; }

    public Wallet() =>
        Balance = 0;

    public void Deposit(decimal amount) =>
        Balance += amount;

    public void Withdraw(decimal amount) =>
        Balance -= amount;

    public void UpdateBalance(decimal betAmount, decimal winAmount) =>
        Balance = Balance - betAmount + winAmount;
}
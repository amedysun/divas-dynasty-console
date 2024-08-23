using Xunit;
using divas_dynasty.wallet;

namespace wallet;

public class WalletTests
{
    private readonly Wallet _wallet;

    public WalletTests() =>
        _wallet = new Wallet();

    [Fact]
    public void Wallet_ShouldInitializeWithZeroBalance()
    {
        Assert.Equal(0, _wallet.Balance);
    }

    [Fact]
    public void Deposit_ShouldIncreaseBalance()
    {
        _wallet.Deposit(100);
        Assert.Equal(100, _wallet.Balance);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance()
    {
        _wallet.Deposit(100);
        _wallet.Withdraw(50);
        Assert.Equal(50, _wallet.Balance);
    }

    [Fact]
    public void UpdateBalance_ShouldAdjustBalanceCorrectly()
    {
        _wallet.Deposit(100);
        _wallet.UpdateBalance(50, 200);
        Assert.Equal(250, _wallet.Balance);
    }
}

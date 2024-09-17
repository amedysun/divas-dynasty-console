using System;
using Xunit;
using domain.wallet.withdraw;

namespace wallet;

public class WithdrawValidatorTests
{
    private readonly WithdrawValidator _withdrawValidator;

    public WithdrawValidatorTests() =>
        _withdrawValidator = new WithdrawValidator();

    [Fact]
    public void Validate_ShouldThrowArgumentException_WhenAmountIsLessThanOrEqualToZero()
    {
        var data = new WithdrawData(0, 100);
        var exception = Assert.Throws<ArgumentException>(() => _withdrawValidator.Validate(data));
        Assert.Equal("Withdraw amount must be greater than zero.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentException_WhenBalanceIsLessThanAmount()
    {
        var data = new WithdrawData(50, 30);
        var exception = Assert.Throws<ArgumentException>(() => _withdrawValidator.Validate(data));
        Assert.Equal("Insufficient balance to withdraw the requested amount.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldNotThrowException_WhenAmountAndBalanceAreValid()
    {
        var data = new WithdrawData(50, 100);
        var exception = Record.Exception(() => _withdrawValidator.Validate(data));
        Assert.Null(exception);
    }
}

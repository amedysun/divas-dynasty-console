using System;
using Xunit;
using domain.wallet;

namespace wallet;

public class DepositValidatorTests
{
    private readonly DepositValidator _depositValidator;

    public DepositValidatorTests() =>
        _depositValidator = new DepositValidator();

    [Fact]
    public void Validate_ShouldThrowArgumentException_WhenAmountIsLessThanOrEqualToZero()
    {
        var exception = Assert.Throws<ArgumentException>(() => _depositValidator.Validate(0));
        Assert.Equal("Amount must be positive.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldNotThrowException_WhenAmountIsPositive()
    {
        var exception = Record.Exception(() => _depositValidator.Validate(100));
        Assert.Null(exception);
    }
}

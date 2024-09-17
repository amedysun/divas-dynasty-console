using System;
using Xunit;
using domain.game;

namespace game;

public class BettingValidatorTests
{
    private readonly BettingValidator _bettingValidator;

    public BettingValidatorTests() =>
        _bettingValidator = new BettingValidator();

    [Fact]
    public void Validate_ShouldThrowArgumentException_WhenBetAmountIsLessThanOne()
    {
        var data = new BettingData(0, 10);
        var exception = Assert.Throws<ArgumentException>(() => _bettingValidator.Validate(data));
        Assert.Equal("Bet amount must be between $1 and $10.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentException_WhenBetAmountIsGreaterThanTen()
    {
        var data = new BettingData(11, 20);
        var exception = Assert.Throws<ArgumentException>(() => _bettingValidator.Validate(data));
        Assert.Equal("Bet amount must be between $1 and $10.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentException_WhenBalanceIsLessThanBetAmount()
    {
        var data = new BettingData(5, 4);
        var exception = Assert.Throws<ArgumentException>(() => _bettingValidator.Validate(data));
        Assert.Equal("Insufficient balance to place the bet.", exception.Message);
    }

    [Fact]
    public void Validate_ShouldNotThrowException_WhenBetAmountAndBalanceAreValid()
    {
        var data = new BettingData(5, 10);
        var exception = Record.Exception(() => _bettingValidator.Validate(data));
        Assert.Null(exception);
    }
}

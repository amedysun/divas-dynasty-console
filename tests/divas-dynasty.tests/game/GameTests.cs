using System;
using divas_dynasty.game;
using Moq;
using Xunit;

namespace game;

public class GameTests
{
    private readonly Mock<Random> _mockRandom;
    private readonly Game _game;

    public GameTests()
    {
        _mockRandom = new Mock<Random>();
        _game = new Game(_mockRandom.Object);
    }

    [Fact]
    public void PlaceBet_ShouldReturnFalseAndZeroAmount_WhenBetIsLost()
    {
        // Arrange
        _mockRandom.Setup(r => r.Next(1, 101)).Returns(50);

        // Act
        var result = _game.PlaceBet(100);

        // Assert
        Assert.False(result.IsWin);
        Assert.Equal(0, result.WinAmount);
    }

    [Fact]
    public void PlaceBet_ShouldReturnTrueAndDoubleAmount_WhenBetIsWonWithDoubleBonus()
    {
        // Arrange
        _mockRandom.Setup(r => r.Next(1, 101)).Returns(90);

        // Act
        var result = _game.PlaceBet(100);

        // Assert
        Assert.True(result.IsWin);
        Assert.Equal(200, result.WinAmount);
    }

    [Fact]
    public void PlaceBet_ShouldReturnTrueAndHightAmout_WhenBetIsWonWithHighBonus()
    {
        // Arrange
        _mockRandom.SetupSequence(r => r.Next(1, 101)).Returns(91);
        _mockRandom.Setup(r => r.Next(2, 11)).Returns(5);

        // Act
        var result = _game.PlaceBet(100);

        // Assert
        Assert.True(result.IsWin);
        Assert.Equal(500, result.WinAmount);
    }
}

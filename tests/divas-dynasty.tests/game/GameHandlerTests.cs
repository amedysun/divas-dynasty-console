using System;
using Xunit;
using Moq;
using divas_dynasty.wallet;
using divas_dynasty.shared;
using divas_dynasty.game;

namespace game;

public class GameHandlerTests
{
    private readonly Mock<IHandler<DepositCommand>> _mockDepositHandler;
    private readonly Mock<IHandler<WithdrawCommand>> _mockWithdrawHandler;
    private readonly Mock<IHandler<BetCommand>> _mockBetHandler;
    private readonly GameHandler _gameHandler;

    public GameHandlerTests()
    {
        _mockDepositHandler = new Mock<IHandler<DepositCommand>>();
        _mockWithdrawHandler = new Mock<IHandler<WithdrawCommand>>();
        _mockBetHandler = new Mock<IHandler<BetCommand>>();
        _gameHandler = new GameHandler(_mockDepositHandler.Object, _mockWithdrawHandler.Object, _mockBetHandler.Object);
    }

    [Fact]
    public void Handle_ShouldThrowInvalidOperationException_WhenInputIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle(string.Empty));
    }

    [Fact]
    public void Handle_ShouldThrowInvalidOperationException_WhenInputIsInvalidFormat()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle("deposit"));
    }

    [Fact]
    public void Handle_ShouldThrowInvalidOperationException_WhenInputIsInvalidAmount()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle("deposit abc"));
    }

    [Fact]
    public void Handle_ShouldInvokeDepositHandler_WhenCommandIsDeposit()
    {
        _gameHandler.Handle("deposit 100");
        _mockDepositHandler.Verify(h => h.Handle(It.Is<DepositCommand>(c => c.Amount == 100)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldInvokeWithdrawHandler_WhenCommandIsWithdraw()
    {
        _gameHandler.Handle("withdraw 50");
        _mockWithdrawHandler.Verify(h => h.Handle(It.Is<WithdrawCommand>(c => c.Amount == 50)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldInvokeBetHandler_WhenCommandIsBet()
    {
        _gameHandler.Handle("bet 25");
        _mockBetHandler.Verify(h => h.Handle(It.Is<BetCommand>(c => c.Amount == 25)), Times.Once);
    }

    [Fact]
    public void Handle_ShoulThrowsInvalidOperationException_WhenCommandIsInvalid()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle("invalid 100"));
    }
}

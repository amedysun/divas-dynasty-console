using System;
using Xunit;
using Moq;
using domain.shared;
using domain.wallet.deposit;
using domain.wallet.withdraw;
using domain.bet;
using domain.game.console;

namespace game;

public class GameHandlerTests
{
    private readonly Mock<IHandler<DepositCommand, DepositResponse>> _mockDepositHandler;
    private readonly Mock<IHandler<WithdrawCommand, WithdrawResponse>> _mockWithdrawHandler;
    private readonly Mock<IHandler<BetCommand, BetResponse>> _mockBetHandler;
    private readonly GameHandler _gameHandler;

    public GameHandlerTests()
    {
        _mockDepositHandler = new Mock<IHandler<DepositCommand, DepositResponse>>();
        _mockWithdrawHandler = new Mock<IHandler<WithdrawCommand, WithdrawResponse>>();
        _mockBetHandler = new Mock<IHandler<BetCommand, BetResponse>>();
        _gameHandler = new GameHandler(_mockDepositHandler.Object, _mockWithdrawHandler.Object, _mockBetHandler.Object);
    }

    [Fact]
    public void Handle_ShouldThrowInvalidOperationException_WhenInputIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle(new GameCommand(string.Empty)));
    }

    [Fact]
    public void Handle_ShouldThrowInvalidOperationException_WhenInputIsInvalidFormat()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle(new GameCommand("deposit")));
    }

    [Fact]
    public void Handle_ShouldThrowInvalidOperationException_WhenInputIsInvalidAmount()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle(new GameCommand("deposit abc")));
    }

    [Fact]
    public void Handle_ShouldInvokeDepositHandler_WhenCommandIsDeposit()
    {
        _gameHandler.Handle(new GameCommand("deposit 100"));
        _mockDepositHandler.Verify(h => h.Handle(It.Is<DepositCommand>(c => c.Amount == 100)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldInvokeDepositHandler_WhenCommandIsUpperCaseDeposit()
    {
        _gameHandler.Handle(new GameCommand("DEPOSIT 100"));
        _mockDepositHandler.Verify(h => h.Handle(It.Is<DepositCommand>(c => c.Amount == 100)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldInvokeWithdrawHandler_WhenCommandIsWithdraw()
    {
        _gameHandler.Handle(new GameCommand("withdraw 50"));
        _mockWithdrawHandler.Verify(h => h.Handle(It.Is<WithdrawCommand>(c => c.Amount == 50)), Times.Once);
    }

    [Fact]
    public void Handle_ShouldInvokeBetHandler_WhenCommandIsBet()
    {
        _gameHandler.Handle(new GameCommand("bet 25"));
        _mockBetHandler.Verify(h => h.Handle(It.Is<BetCommand>(c => c.Amount == 25)), Times.Once);
    }

    [Fact]
    public void Handle_ShoulThrowsInvalidOperationException_WhenCommandIsInvalid()
    {
        Assert.Throws<InvalidOperationException>(() => _gameHandler.Handle(new GameCommand("invalid 100")));
    }
}

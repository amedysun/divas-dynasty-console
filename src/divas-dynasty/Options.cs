using CommandLine;

namespace divas_dynasty;

internal class Options
{
    [Option('d', "deposit", Required = false, HelpText = "Deposit amount.")]
    public decimal? DepositAmount { get; set; }

    [Option('w', "withdraw", Required = false, HelpText = "Withdraw amount.")]
    public decimal? WithdrawAmount { get; set; }

    [Option('b', "bet", Required = false, HelpText = "Bet amount.")]
    public decimal? BetAmount { get; set; }

    [Option('e', "exit", Required = false, HelpText = "Exit the game.")]
    public bool Exit { get; set; }
}

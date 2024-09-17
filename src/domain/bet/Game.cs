namespace domain.game;

public class Game(Random random)
{
    public BetResult PlaceBet(decimal betAmount)
    {
        int outcome = random.Next(1, 101);

        return outcome switch
        {
            <= 50 => new BetResult(false, 0), // 50% chance to lose
            <= 90 => new BetResult(true, betAmount * 2), // 40% chance to win up to x2 the bet amount
            _ => new BetResult(true, betAmount * random.Next(2, 11)), // 10% chance to win between x2 and x10 the bet amount
        };
    }
}

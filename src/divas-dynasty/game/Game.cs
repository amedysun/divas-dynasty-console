namespace divas_dynasty.game;

public class Game
{
    private readonly Random _random;

    public Game() =>
        _random = new Random();

    public decimal PlaceBet(decimal betAmount)
    {
        int outcome = _random.Next(1, 101);

        return outcome switch
        {
            <= 50 => 0, // 50% chance to lose
            <= 90 => betAmount * 2, // 40% chance to win up to x2 the bet amount
            _ => betAmount * _random.Next(2, 11), // 10% chance to win between x2 and x10 the bet amount
        };
    }
}

public class HighScore
{
    public string Name { get; private set; }
    public int Score { get; private set; }
    public int Coins { get; private set; }

    public HighScore(string name, int score, int coins)
    {
        Name = name;
        Score = score;
        Coins = coins;
    }
}
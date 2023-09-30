namespace C__Blackjack.Data;
public class PlayerData // information about the player
{
    public string? Name { get; set; }
    public double Balance { get; set; }

    private int wins;
    private int loses;
    public int Wins
    {
        get => wins;
        set
        {
            if (value < 0)
                throw new Exception("The log file was corrupted");
            wins = value;
        }
    }
    public int Loses
    {
        get => loses;
        set
        {
            if (value < 0)
                throw new Exception("The log file was corrupted");
            loses = value;
        }
    }
    public PlayerData(string? name, double balance, int wins, int loses)
    {
        Name = name;
        Balance = balance;
        Wins = wins;
        Loses = loses;
    }
    public override string ToString() =>
        $"{Name} {Balance} {Wins} {Loses}";
}

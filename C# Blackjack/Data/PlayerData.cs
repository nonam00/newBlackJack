namespace C__Blackjack.Data;

// information about the player
[Serializable]
public class PlayerData 
{
    public string Name { get; set; }
    public double Balance { get; set; }

    private int wins;
    private int loses;

    public int Wins
    {
        get => wins;
        set
        {
            if (value < 0)
                throw new ArgumentException("File with players data was corrupted");
            wins = value;
        }
    }
    public int Loses
    {
        get => loses;
        set
        {
            if (value < 0)
                throw new Exception("File with players data file was corrupted");
            loses = value;
        }
    }
    public PlayerData(string name, double balance, int wins, int loses)
    {
        Name = name;
        Balance = balance;
        Wins = wins;
        Loses = loses;
    }
    public override string ToString() =>
        $"{Name} {Balance} {Wins} {Loses}";
}

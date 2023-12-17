using System.Text.Json;

namespace C__Blackjack.Data;
public class DataHandler
{
    private readonly string fileName = "PlayersData.json"; // name of file with data about players
    private List<PlayerData> data = new List<PlayerData>(); //information about all players
	public PlayerData CurrentPlayer { get; set; }
    private int id; // Current user id in data list
	public DataHandler()
    {
        if (!File.Exists(fileName))
        {
            File.Create(fileName);
        }

		data = JsonSerializer.Deserialize<List<PlayerData>>(File.ReadAllText(fileName))
            ?? throw new FileNotFoundException($"{fileName} was corrupted");
    }

	// Determining the current user
	public void SetCurrentUser(string name)
    {
        for(int i=0; i<data.Count; i++)
        {
            if (data[i].Name == name)
            {
                CurrentPlayer = data[i];
                id = i;
                Console.WriteLine($"Welcome back {name}");
                return;
            }
        }
        NewPlayer(name);
    }

	//Creating a new player
	private void NewPlayer(string name)
    {
        double balance;
        bool _try = false;
        while(!_try)
        {
            Console.Clear();
            Console.WriteLine("What is your balance?");
            _try = Double.TryParse(Console.ReadLine(), out balance) && balance>=0;
            if (balance == 0)
            {
                Console.WriteLine("Welcome back when you get money");
            }
            else if(_try)
            {
                CurrentPlayer = new PlayerData(name, balance, 0, 0);
                id = data.Count;
                data.Add(CurrentPlayer);
            }
        }
    }

	//Display current user statistics
	public void PrintStatistics()
    {
        Console.WriteLine(
            $"Balance: {CurrentPlayer.Balance}\n" +
            $"Wins: {CurrentPlayer.Wins}\n" +
            $"Loses: {CurrentPlayer.Loses}\n");
    }

	//Writing updated data to a file before exiting the program
	public void Exit()
    {
        if (id > data.Count) return;

        data[id] = CurrentPlayer;

		var options = new JsonSerializerOptions { WriteIndented = true };
		string jsonString = JsonSerializer.Serialize(data, options);
		File.WriteAllText("PlayersData.json", jsonString);
	}
}

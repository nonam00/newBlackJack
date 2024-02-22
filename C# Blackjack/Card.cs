using System.Text.Json;

namespace C__Blackjack;

[Serializable]
public class Card
{
    public static List<string> pack = new List<string>(); //card package data(names only)

    private static int pack_count = 52; //cards counter

    private Random rnd = new Random();

    public string Name { get; set; }
    public int Mark { get; set; }

    //Card package initialize from json file
    public static void PackInit()
    {
        string fileName = "..\\..\\..\\Data\\CardPackage.json";

        if(!File.Exists(fileName))
        {
			throw new FileNotFoundException($"{fileName} was corrupted");
		}

        string jsonString = File.ReadAllText(fileName);

        pack = JsonSerializer.Deserialize<List<string>>(jsonString)
            ?? throw new FileNotFoundException($"{fileName} was corrupted");

		pack_count = 52;
	}

    public Card()
    {
        Name = pack[rnd.Next(0, pack_count--)];
        pack.Remove(Name);

        if (Name == "J" || Name == "Q" || Name == "K")
        {
            Mark = 10;
        }
        else if (Name == "A")
        {
            Mark = 11;
        }
        else
        {
            Mark = Convert.ToInt32(Name);
        }
    }
}

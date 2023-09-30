namespace C__Blackjack;

public class Card
{
    public static List<string> pack = new List<string>(); //колода карт
    private static int pack_count = 52; //счётчик количества карт в колоде
    private Random rnd = new Random();

    public string Name { get; set; } //имя карты
    public int Mark { get; set; } //значение карты в очках
    public static void PackInit() //инициализация колоды карт
    {
        for (int i = 2; i <= 10; i++)
            for (int j = 0; j < 4; j++)
                pack.Add(i.ToString());
        for (int i = 0; i < 4; i++)
        {
            pack.Add("J");
            pack.Add("Q");
            pack.Add("K");
            pack.Add("A");
        }
        pack_count = 52;
    }
    public Card()
    {
        Name = pack[rnd.Next(0, pack_count--)];
        pack.Remove(Name);

        if (Name == "J" || Name == "Q" || Name == "K")
            Mark = 10;
        else if (Name == "A")
            Mark = 11;
        else
            Mark = Convert.ToInt32(Name);
    }
}

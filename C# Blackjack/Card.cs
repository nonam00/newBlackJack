namespace C__Blackjack
{
    internal class Card //entity of the card
    {
        public static List<string> pack = new(); //колода карт
        private static int pack_count = 52; //счётчик количества карт в колоде 

        private string name; //имя карты
        private int mark; //значение карты в очках
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
        }
        public Card()
        {
            Random rnd = new();

            name = pack[rnd.Next(0, pack_count--)];
            pack.Remove(name);

            if (name == "J" || name == "Q" || name == "K")
                mark = 10;
            else if (name == "A")
                mark = 11;
            else
                mark = Convert.ToInt32(name);
        }
        public string Name
        { get { return name; } }
        public int Mark
        {
            get { return mark; }
            set { mark = value; }
        }
    }
}

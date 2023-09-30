namespace C__Blackjack.Players;

public class Dealer : Hand
{
    public void Print(int type)
    {
        Console.WriteLine("Dealer's cards:\n");
        for (int i = 0; i < cards.Count; i++)
            Console.Write("##############\t");
        Console.WriteLine();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < cards.Count(); j++)
            {
                if (!(type == 1 && j == 1))
                {
                    if (i == 1)
                    {
                        Console.Write($"#  {cards[j].Name}");
                        if (cards[j].Name == "10")
                            Console.Write("        #\t");
                        else
                            Console.Write("         #\t");
                        continue;
                    }
                    else if (i == 5)
                    {
                        Console.Write($"#         {cards[j].Name}");
                        if (cards[j].Name == "10")
                            Console.Write(" #\t");
                        else
                            Console.Write("  #\t");
                        continue;
                    }
                }
                if (i == 7)
                    Console.Write("##############\t");
                else
                    Console.Write("#            #\t");
            }
            Console.WriteLine();
        }
    }
}

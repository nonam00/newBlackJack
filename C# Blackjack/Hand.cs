using static System.Formats.Asn1.AsnWriter;

namespace C__Blackjack
{
    internal class Hand
    {
        private List<Card> cards = new();
        private int score;
        //distribution of cards to the player
        public Hand()
        {
            score = 0;
            for (int i = 0; i < 2; i++)
                this.AddCard();
        }

        //prints player cards
        public void Print(int type)
        {
            if (type == 0)
                Console.WriteLine("Your cards:\n");
            else
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

        //adds a card to the person
        public void AddCard()
        {
            Card temp = new();
            score += temp.Mark;
            cards.Add(temp);
        }

        //do a split
        public void Split()
        {
            score -= cards[cards.Count - 1].Mark;
            cards.RemoveAt(cards.Count - 1);
            this.AddCard();
        }

        //ñhanges the value of card A to 1 if it is in hand
        public void BustCancel()
        {
            foreach(Card item in cards)
                if (item.Name == "A" && item.Mark == 11)
                {
                    item.Mark = 1;
                    score -= 10;
                    break;
                }
        }

        //checking for the ability to split
        public bool SplitCheck() => cards.Count == 2 && cards[0].Mark == cards[1].Mark;

        //checks about score
        public bool auto_win()
        {
            if ((cards[0].Name == "A" && cards[1].Name == "A") || score == 21)
                return true;
            else
                return false;
        }

        //bust check
        public bool Bust() => score > 21;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}

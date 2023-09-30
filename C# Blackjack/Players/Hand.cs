namespace C__Blackjack.Players;

public abstract class Hand
{
    protected List<Card> cards = new List<Card>();
    public int Score { get; set; }
    //distribution of cards to the player
    public Hand()
    {
        Score = 0;
        for (int i = 0; i < 2; i++)
            AddCard();
    }

    //adds a card to the person
    public void AddCard()
    {
        Card temp = new();
        Score += temp.Mark;
        cards.Add(temp);
    }

    //do a split
    public void Split()
    {
        Score -= cards[cards.Count - 1].Mark;
        cards.RemoveAt(cards.Count - 1);
        AddCard();
    }

    //ñhanges the value of card A to 1 if it is in hand
    public void BustCancel()
    {
        for (int i = 0; i < cards.Count; i++)
            if (cards[i].Name == "A" && cards[i].Mark == 11)
            {
                cards[i].Mark = 1;
                Score -= 10;
                break;
            }
    }

    //checking for the ability to split
    public bool SplitCheck() => cards.Count == 2 && cards[0].Mark == cards[1].Mark;

    //checks about score
    public bool auto_win() => cards[0].Name == "A" && cards[1].Name == "A" || Score == 21;

    //bust check
    public bool Bust() => Score > 21;
}

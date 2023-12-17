namespace C__Blackjack.Players;

public abstract class Hand
{
    protected List<Card> cards = new List<Card>();
    public int Score { get; set; }

    // Distribution of cards to the player
    public Hand()
    {
        Score = 0;
        for (int i = 0; i < 2; i++)
            AddCard();
    }

    // Adds a card to the person
    public void AddCard()
    {
        Card temp = new();
        Score += temp.Mark;
        cards.Add(temp);
    }

    public void Split()
    {
        Score -= cards[cards.Count - 1].Mark;
        cards.RemoveAt(cards.Count - 1);
        AddCard();
    }

    // Changes the value of card A to 1 if it is in hand
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

    // Checking for the ability to split
    public bool SplitCheck() => cards.Count == 2 && cards[0].Mark == cards[1].Mark;

	// Check for the opportunity to win immediately
	public bool Autowin() => cards[0].Name == "A" && cards[1].Name == "A" || Score == 21;

    // Bust check
    public bool Bust() => Score > 21;
}

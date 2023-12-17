namespace C__Blackjack;

using C__Blackjack.Data;
using C__Blackjack.Players;
public class Game
{
    private Dealer dealer;
    private Player player;

    private DataHandler dataHandler = new DataHandler();

    private double money;
    public Game()
    {
        Authorization();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1 - Take a sit\n" +
                "2 - Your statistics\n" +
                "3 - Top up balance\n" +
                "4 - Exit\n");
            string? choice = Console.ReadLine();
            Console.Clear();

            if (choice == "1")
            {
                if (money > 0)
                    Table();
                else
                {
                    Console.WriteLine("Top up your balance or leave the casino");
                    Console.ReadKey();
                }
            }
            else if (choice == "2")
            {
                dataHandler.PrintStatistics();
                Console.ReadKey();
            }
            else if (choice == "3")
            {
                TopUp();
            }
            else if (choice == "4")
            {
                Exit();
                break;
            }
        }
    }

    // Main game process 
    private void Table()
    {
        while (money > 0)
        {
            Console.Clear();
            Card.PackInit();

            Console.WriteLine("Money: " + money);

            Console.WriteLine("Make a deal: (to leave the game place a bet of 0)");

            double bet;
            bool _try = Double.TryParse(Console.ReadLine(), out bet);

            if (!_try || bet > money || bet < 0)
                continue;

            if (bet == 0) // Game ends if price will be equal to 0
            {
                Console.WriteLine("Thanks for the game");
                Console.WriteLine("Money: " + money);
                break;
            }

            money -= bet;

            dealer = new Dealer();
            player = new Player();

            string? choice;
            while (true)
            {
                Console.Clear();
                PrintAll(dealer, player, 1);

                if (player.Autowin() || dealer.Autowin()) break;

                DisplayChoices(bet);
                choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "Stand")
                    break;

                else if (choice == "Hit")
                {
                    player.AddCard();

                    if (player.Autowin()) break;
                    else if (player.Bust())
                    {
                        player.BustCancel();
                        if (player.Score > 21) break;
                    }
                }
                else if (choice == "Double" && bet * 2 <= money + bet)
                {
                    bet *= 2;
                    Console.WriteLine($"Stavka: {bet}\n");
                    Thread.Sleep(2000);
                    player.AddCard();
                    if (player.Bust())
                    {
                        player.BustCancel();
                        if (player.Score > 21)
                            break;
                    }
                    break;
                }
                else if (player.SplitCheck() && choice == "Split")
                    player.Split();
            }

            Console.Clear();

            PrintAll(dealer, player, 2);

            if (player.Autowin())
            {
                if (dealer.Autowin())
                {
                    Console.WriteLine("Draw");
                    money += bet;
                }
                else
                {
                    Console.WriteLine("21!");
                    money += 2.5 * bet;
                    Console.WriteLine("+" + bet * 1.5);
                }
            }

            else if (player.Bust())
            {
                Console.WriteLine("Bust");
                Final('-', bet);
            }

            else if (player.Score > dealer.Score)
            {
                dealer.BehaviorOnLose(player.Score);

                PrintAll(dealer, player, 2);

                if (dealer.Bust())
                {
                    Console.WriteLine("Win");
                    Final('+', bet);
                    money += bet * 2;
                }
            }

            if (dealer.Score == player.Score)
            {
                Console.WriteLine("Draw");
                money += bet;
            }
            else if (dealer.Autowin() || (!dealer.Bust() && player.Score < dealer.Score))
            {
                Console.WriteLine("Dealer wins");
                Final('-', bet);
            }

            AfterGameDataUpdate();

            Console.ReadKey();
            Card.pack.Clear();
        }
        if (money <= 0)
        {
            Console.WriteLine("\nCasino always wins");
        }
    }

    // Display of available actions
    private void DisplayChoices(double stavka)
    {
        Console.Write("Your choice: Stand, Hit");

        if (stavka * 2 <= money + stavka)
            Console.Write(", Double");

        if (player.SplitCheck())
            Console.Write(", Split");

        Console.WriteLine();
    }
	
    // Display game table
    private void PrintAll(Dealer dealer, Player player, int gameStatus)
    {
        dealer.Print(gameStatus);
        Console.WriteLine("\n\n");

        player.Print();
        Console.WriteLine("\n");
    }

    // Display game results
    private void Final(char operation, double bet)
    {
        Console.WriteLine(operation.ToString() + bet.ToString());
    }

    private void AfterGameDataUpdate()
    {
		if (money < dataHandler.CurrentPlayer.Balance)
		{
			dataHandler.CurrentPlayer.Loses++;
		}

		else if (money > dataHandler.CurrentPlayer.Balance)
		{
			dataHandler.CurrentPlayer.Wins++;
		}

		dataHandler.CurrentPlayer.Balance = money;
	}

	// User authorization
	private void Authorization()
    {
        string name = string.Empty;
        while(name==string.Empty)
        {
			Console.WriteLine("Welcome to the Casino\n" +
			    "What's your name");
			name = Console.ReadLine();
            Console.Clear();
        }

        dataHandler.SetCurrentUser(name);

        money = dataHandler.CurrentPlayer.Balance;

        Console.Clear();
    }

	// Replenishment of balance
	private void TopUp()
    {
        double amount;
        bool _try = false;

        while(!_try)
        {
            Console.WriteLine("Enter the amount to top up");
           _try = Double.TryParse(Console.ReadLine(), out amount) && amount > 0;
			if (_try)
			{
				money += amount;
				dataHandler.CurrentPlayer.Balance = money;
			}
            Console.Clear();
		}
    }

	// Player leaves the casino, data saving
	private void Exit() 
    {
        dataHandler.Exit();
        Console.WriteLine("Come back again!");
    }
}

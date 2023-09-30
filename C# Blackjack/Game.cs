namespace C__Blackjack;

using C__Blackjack.Data;
using C__Blackjack.Players;
public class Game
{
    private Hand dealer;
    private Hand player;

    private DataHandler dataHandler;

    private double money;
    public Game()
    {
        dataHandler = new DataHandler();
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

    private void Table()
    {
        while (money > 0)
        {
            Console.Clear();
            Card.PackInit();
            Console.WriteLine("Money: " + money);

            Console.WriteLine("Make a deal:");
            double stavka;
            bool _try = Double.TryParse(Console.ReadLine(), out stavka);

            if (!_try || stavka > money || stavka < 0)
                continue;

            if (stavka == 0) //game ends if price will be equal to 0
            {
                Console.WriteLine("Thanks for the game");
                Console.WriteLine("Money: " + money);
                break;
            }

            money -= stavka;

            dealer = new Dealer();
            player = new Player();

            string? choice;
            while (true)
            {
                Console.Clear();
                AllPrint(dealer, player, 1);
                if (player.auto_win() || dealer.auto_win())
                    break;
                PrintChoices(stavka);
                choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "Stand")
                    break;

                else if (choice == "Hit")
                {
                    player.AddCard();
                    if (player.auto_win())
                        break;
                    else if (player.Bust())
                    {
                        player.BustCancel();
                        if (player.Score > 21)
                            break;
                    }
                }
                else if (choice == "Double" && stavka * 2 <= money + stavka)
                {
                    stavka *= 2;
                    Console.WriteLine($"Stavka: {stavka}\n");
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

            AllPrint(dealer, player, 2);

            if (player.auto_win())
            {
                if (dealer.auto_win())
                {
                    Console.WriteLine("Draw");
                    money += stavka;
                }
                else
                {
                    Console.WriteLine("21!");
                    money += 2.5 * stavka;
                    Console.WriteLine("+" + stavka * 1.5);
                }
            }

            else if (player.Bust())
            {
                Console.WriteLine("Bust");
                Final('-', stavka);
            }

            else if (player.Score > dealer.Score)
            {
                DealerBehavior();

                if (dealer.Bust())
                {
                    Console.WriteLine("Win");
                    Final('+', stavka);
                    money += stavka * 2;
                }
            }

            if (dealer.Score == player.Score)
            {
                Console.WriteLine("Draw");
                money += stavka;
            }
            else if (dealer.auto_win() || (!dealer.Bust() && player.Score < dealer.Score))
            {
                Console.WriteLine("Dealer wins");
                Final('-', stavka);
            }

            if (money < dataHandler.CurrentPlayer.Balance)
                dataHandler.CurrentPlayer.Loses++;

            else if(money > dataHandler.CurrentPlayer.Balance)
                dataHandler.CurrentPlayer.Wins++;

            Console.ReadKey();
            Card.pack.Clear();
            dataHandler.CurrentPlayer.Balance = money;
        }
        if (money <= 0)
            Console.WriteLine("\nCasino always wins");
    }

    private void PrintChoices(double stavka)
    {
        Console.Write("Your choice: Stand, Hit");

        if (stavka * 2 <= money + stavka)
            Console.Write(", Double");

        if (player.SplitCheck())
            Console.Write(", Split");

        Console.WriteLine();
    }

    private void DealerBehavior() // A function that determines the behavior of the dealer in cases where the player has a better hand
    {
        while (dealer.Score < player.Score && !dealer.Bust())
        {
            Thread.Sleep(2000);
            Console.Clear();

            Console.WriteLine("Dealer hits\n");

            dealer.AddCard();

            if (dealer.Bust())
                dealer.BustCancel();

            AllPrint(dealer, player, 2);
        }
    }

    //print game table
    private void AllPrint(Hand dealer, Hand player, int game)
    {
        (dealer as Dealer).Print(game);
        Console.WriteLine("\n\n");

        (player as Player).Print();
        Console.WriteLine("\n");
    }

    //print game results
    private void Final(char operation, double stavka)
    {
        Console.WriteLine(operation.ToString() + stavka.ToString());
    }
    private void Authorization() // user authorization
    {
        Console.WriteLine("Welcome to the Casino\n" +
            "What's your name");
        string? name = Console.ReadLine();
        dataHandler.SetCurr(name);
        money = dataHandler.CurrentPlayer.Balance;
        Console.Clear();
    }

    private void TopUp()
    {
        Console.WriteLine("Enter the amount to top up");
        double amount;
        bool _try = Double.TryParse(Console.ReadLine(), out amount);
        if (_try && amount>0)
        {
            money += amount;
            dataHandler.CurrentPlayer.Balance = money;
        }
    }

    private void Exit() //Leave the casino, save data
    {
        dataHandler.Exit();
        Console.WriteLine("Come back again!");
    }
}

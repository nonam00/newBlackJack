using C__Blackjack;

namespace blackjack
{
    internal class Program
    {
        //print game results
        private static void final(char operation, double stavka, double money)
        {
            Console.WriteLine(operation.ToString() + stavka.ToString());
        }

        //print game table
        private static void all_print(Hand dealer, Hand player, int game)
        {
            dealer.Print(game);
            Console.WriteLine("\n\n");

            player.Print(0);
            Console.WriteLine("\n");
        }
        private static void Main(string[] args)
        {
            Console.Write("Your budget: ");
            try
            {
                double money = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("\n");
                if (money > 0)
                {
                    while (money > 0)
                    {
                        Card.PackInit();
                        Console.WriteLine("Money: " + money);

                        Console.WriteLine("Make a deal:");
                        double stavka = Convert.ToDouble(Console.ReadLine());

                        if (stavka == 0) //game ends if price will be equal to 0
                        {
                            Console.WriteLine("Thanks for the game");
                            Console.WriteLine("Money: " + money);
                            break;
                        }
                        if (stavka > money || stavka < 0)
                        {
                            Console.ReadKey();
                            continue;
                        }

                        money -= stavka;

                        Hand dealer = new();
                        Hand player = new();

                        bool split = false;

                        string choice;
                        while (true)
                        {
                            Console.Clear();
                            all_print(dealer, player, 1);
                            if (player.auto_win() || dealer.auto_win())
                                break;
                            Console.Write("Your choice: Stand, Hit");
                            if (stavka * 2 <= money + stavka)
                                Console.Write(", Double ");
                            if (player.SplitCheck())
                            {
                                Console.Write(", Split");
                                split = true;
                            }
                            Console.WriteLine();
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
                                Console.WriteLine($"Stavka: stavka\n");
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
                            else if (split && choice == "Split")
                                player.Split();
                        }

                        Console.Clear();

                        all_print(dealer, player, 2);

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

                        else if (dealer.auto_win())
                        {
                            Console.WriteLine("Dealer wins");
                            final('-', stavka, money);
                        }

                        else if (player.Score < dealer.Score)
                        {
                            Console.WriteLine("Dealer wins");
                            final('-', stavka, money);
                        }

                        else if (dealer.Score == player.Score)
                        {
                            Console.WriteLine("Draw");
                            money += stavka;
                        }

                        else if (player.Bust())
                        {
                            Console.WriteLine("Bust");
                            final('-', stavka, money);
                        }

                        else if (player.Score > dealer.Score)
                        {
                            while ((dealer.Score <= player.Score) && !dealer.Bust())
                            {
                                Thread.Sleep(2000);
                                Console.Clear();

                                Console.WriteLine("Dealer hits\n");

                                dealer.AddCard();

                                if (dealer.Bust())
                                    dealer.BustCancel();

                                all_print(dealer, player, 2);
                            }

                            if (dealer.Score == player.Score)
                            {
                                Console.WriteLine("Draw");
                                money += stavka;
                            }
                            else if (dealer.Bust() || player.Score > dealer.Score)
                            {
                                Console.WriteLine("Win");
                                final('+', stavka, money);
                                money += stavka * 2;
                            }
                            else
                            {
                                Console.WriteLine("Dealer wins");
                                final('-', stavka, money);
                            }
                        }
                        Console.ReadKey();
                        Console.Clear();
                        Card.pack.Clear();
                    }
                    if (money <= 0)
                        Console.WriteLine("\nCasino always wins");
                }
                else
                    Console.WriteLine("You don't have any money");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
    }
}
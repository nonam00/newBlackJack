namespace C__Blackjack.Data;
public class DataHandler
{
    private List<PlayerData>? data;
    public PlayerData CurrentPlayer { get; set; }
   
    private int id; // индекс текущего пользователя в списке для обновления данных 
    public DataHandler()
    {
        data = new List<PlayerData>();
        using StreamReader sr = new StreamReader("Data.txt"); //открытие потока на чтение
        try
        {
            while(!sr.EndOfStream)
            {
                string[] player = sr.ReadLine().Split(' '); //чтение строки файла, форматирование и запись в данных в массив
                data.Add(new PlayerData (player[0], Double.Parse(player[1]), int.Parse(player[2]), int.Parse(player[3])));
            }
        }
        catch
        {
            throw new Exception("The log file was corrupted");
        }
    }
    public void SetCurr(string? name) // установление текущего пользователя 
    {
        for(int i=0; i<data.Count; i++)
        {
            if (data[i].Name == name)
            {
                CurrentPlayer = data[i];
                id = i;
                Console.WriteLine($"Welcome back {name}");
                return;
            }
        }
        NewPlayer(name);
    }
    private void NewPlayer(string? name) // создание нового пользователя
    {
        Console.WriteLine("What is your balance?");
        double balance;
        bool _try = Double.TryParse(Console.ReadLine(), out balance);
        if(_try && balance > 0)
        {
            CurrentPlayer = new PlayerData(name, balance, 0, 0);
            id = data.Count;
            data.Add(CurrentPlayer);
        }
        else
        {
            Console.WriteLine("Welcome back when you get money");
        }
    }
    public void PrintStatistics() // вывод статистики текущего пользователя
    {
        Console.WriteLine(
            $"Balance: {CurrentPlayer.Balance}\n" +
            $"Wins: {CurrentPlayer.Wins}\n" +
            $"Loses: {CurrentPlayer.Loses}\n");
    }
    public void Exit() //запись обновлённых данных в файл перед завершением программы
    { 
        data[id] = CurrentPlayer;
        using StreamWriter sw = new StreamWriter($"Data.txt");
        for(int i=0; i<data.Count;i++)
        {
            sw.WriteLine(data[i]);
        }
    }
}

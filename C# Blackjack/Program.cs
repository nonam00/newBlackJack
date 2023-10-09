//в случае, если не удаётся найти файл, необходимо проверить текущую директорию проекта и переместить файл лога туда
// .../C# Blackjack/ или .../C# Blackjack/bin/Debug/net7.0/ (текущая)

using C__Blackjack;

try { Game game = new Game(); }
catch (Exception e) { Console.WriteLine(e.Message); }

// See https://aka.ms/new-console-template for more information

using Tic_A_Tac;

Console.Clear();
Console.WriteLine("Starting Game!");
Console.WriteLine("Press Q or ESC to quit...");

var game = new Game();
game.Run();

using System;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;

// TODO: I need to get rid of this
using CodeCompete.DotNet.TicTacToe.Players;

namespace CodeCompete.DotNet.PlayerHost
{
    // TODO: Do I need to make "State" more specific?
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DotNetPlayerHost");

            int i = 0;
            string id = args[i++];
            Console.WriteLine(id);
            string inPath = args[i++];
            Console.WriteLine(inPath);
            string outPath = args[i++];
            Console.WriteLine(outPath);

            GameState<string[][]> state = JsonConvert.DeserializeObject<GameState<string[][]>>(System.IO.File.ReadAllText(inPath));
            // TODO: Use DI/IoC container to fix this because an abstract getter can't be used for static classes
            GamePlayer<string[][]> player = new SimpleComputerTicTacToePlayer(id);
            GameMove<string[][]> move = player.DoMove(state);

            System.IO.File.WriteAllText(outPath, JsonConvert.SerializeObject(move));
        }
    }
}

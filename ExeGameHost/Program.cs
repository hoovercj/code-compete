using System;
using System.IO;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;
using CodeCompete.DotNet.TicTacToe;
using CodeCompete.DotNet.TicTacToe.Players;

namespace CodeComplete.DotNet.GameHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Exe Game Host:");

            int i = 0;
            string playerId1 = args[i++];
            Console.WriteLine(playerId1);
            string playerPath1 = args[i++];
            Console.WriteLine(playerPath1);
            string playerId2 = args[i++];
            Console.WriteLine(playerId2);
            string playerPath2 = args[i++];
            Console.WriteLine(playerPath2);
            string resultsPath = args[i++];
            Console.WriteLine(resultsPath);
            string finalResultsPath = Path.Combine(resultsPath, "results.json");

            Console.WriteLine("Read all parameters!");

            var player1 = new PlayerProxy<string[][]>(playerId1, resultsPath, playerPath1);
            var player2 = new PlayerProxy<string[][]>(playerId2, resultsPath, playerPath2);

            // TODO: Use IoC or something to avoid hardcoding game
            var game = new TicTacToeGame(new GamePlayer<string[][]>[] { player1, player2});

            var result = game.PlayGame();

            Console.WriteLine($"The winner is: {result.Winner.Id}");
            Console.WriteLine($"Writing final results to {finalResultsPath}");
            File.WriteAllText(Path.Combine(resultsPath, "results.json"), JsonConvert.SerializeObject(result));
        }
    }
}

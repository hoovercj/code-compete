using System;
using System.IO;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;
using CodeCompete.DotNet.TicTacToe.Games;

namespace CodeCompete.DotNet.GameHost
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            string playerId1 = args[i++];
            string playerPath1 = args[i++];
            string playerId2 = args[i++];
            string playerPath2 = args[i++];
            string resultsPath = args[i++];
            string finalResultsPath = Path.Combine(resultsPath, "results.json");

            var player1 = new PlayerProxy<object>(playerId1, resultsPath, playerPath1);
            var player2 = new PlayerProxy<object>(playerId2, resultsPath, playerPath2);

            var game = new CodeCompete.DotNet.TicTacToe.Games.TicTacToe(new GamePlayer<object>[] { player1, player2});

            var result = game.PlayGame();

            File.WriteAllText(Path.Combine(resultsPath, "results.json"), JsonConvert.SerializeObject(result));
        }
    }
}

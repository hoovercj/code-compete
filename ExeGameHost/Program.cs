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
            int i = 0;
            string playerId1 = args[i++];
            string playerPath1 = args[i++];
            string playerId2 = args[i++];
            string playerPath2 = args[i++];
            string resultsPath = args[i++];

            var player1 = new PlayerProxy(playerId1, resultsPath, playerPath1);
            var player2 = new PlayerProxy(playerId2, resultsPath, playerPath2);

            // TODO: Use IoC or something to avoid hardcoding game
            var game = new TicTacToeGame(new GamePlayer[] { player1, player2});

            var result = game.PlayGame();

            File.WriteAllText(resultsPath, JsonConvert.SerializeObject(result));
        }
    }
}

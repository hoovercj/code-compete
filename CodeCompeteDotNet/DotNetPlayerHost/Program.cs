using System;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.TicTacToe.Players;
using CodeCompete.DotNet.TicTacToe;

namespace CodeCompete.DotNet.PlayerHost
{
    // TODO: Do I need to make "State" more specific?
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            string id = args[i++];
            string inPath = args[i++];
            string outPath = args[i++];

            TicTacToeGame state = JsonConvert.DeserializeObject<TicTacToeGame>(System.IO.File.ReadAllText(inPath));
            // TODO: Use DI/IoC container to fix this because an abstract getter can't be used for static classes
            var player = new SimpleComputerTicTacToePlayer(id);
            TicTacToeMove move = (TicTacToeMove)player.DoMove(state);

            System.IO.File.WriteAllText(outPath, JsonConvert.SerializeObject(move));
        }
    }
}

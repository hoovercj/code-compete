using System;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;

// TODO: Find a way to avoid referencing the namespace directly
using CodeCompete.DotNet.TicTacToe.Players;

namespace CodeCompete.DotNet.PlayerHost
{
    class StateProvider : IGameStateProvider
    {
        private string path;
        public StateProvider(string path)
        {
            this.path = path;
        }

        public GameState<T> ProvideState<T>()
        {
            return JsonConvert.DeserializeObject<GameState<T>>(System.IO.File.ReadAllText(this.path));
        }
    }

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

            System.IO.File.WriteAllText(outPath, JsonConvert.SerializeObject(
                    // TODO: Find a way to avoid referencing the player class directly
                    new SimpleComputerTicTacToePlayer(id).DoMove(new StateProvider(inPath))
                )
            );
        }
    }
}

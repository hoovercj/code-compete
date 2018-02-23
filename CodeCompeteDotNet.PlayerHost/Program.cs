using System;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;
using CodeCompete.DotNet.GameName;

namespace CodeCompete.DotNet.GameHost
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            string id = args[i++];
            string inPath = args[i++];
            string outPath = args[i++];

            var player = new PlayerName();
            player.Id = id;

            System.IO.File.WriteAllText(outPath, JsonConvert.SerializeObject(
                    player.DoMove(new StateProvider(inPath))
                )
            );
        }
    }


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
}
using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;


namespace CodeCompete.DotNet.Implementation
{
    public class PlayerProxy : GamePlayer
    {
        private readonly string id;

        private readonly string exe;

        private readonly string cwd;

        public string Id => id;

        public PlayerProxy(string id, string cwd, string exe)
        {
            this.id = id;
            this.cwd = cwd;
            this.exe = exe;
        }

        public IGameMove DoMove(IGameState game)
        {
            string resultsDir = Path.Combine(this.cwd, this.id);
            Console.WriteLine($"Enure results directory exists: {resultsDir}");
            Directory.CreateDirectory(resultsDir);
            string statePath = Path.Combine(resultsDir, "state.json");
            string movePath = Path.Combine(resultsDir, "move.json");
            string args = $"{this.id} \"{statePath}\" \"{movePath}\"";

            Console.WriteLine($"Write state to file: {statePath}");
            File.WriteAllText(statePath, JsonConvert.SerializeObject(game));

            Process process = Process.Start(this.exe, args);
            process.WaitForExit();

            Console.WriteLine($"Read new move from file: ${movePath}");
            IGameMove move = JsonConvert.DeserializeObject<IGameMove>(File.ReadAllText(movePath));

            // TODO: clean up files

            return move;
        }
    }
}
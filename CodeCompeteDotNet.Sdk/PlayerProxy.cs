using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using CodeCompete.DotNet.Interfaces;


namespace CodeCompete.DotNet.Implementation
{
    public class PlayerProxy<T> : GamePlayer<T>
    {
        private readonly string id;

        private readonly string exe;

        private readonly string cwd;

        public override string Id => id;

        public PlayerProxy(string id, string cwd, string exe)
        {
            this.id = id;
            this.cwd = cwd;
            this.exe = exe;
        }

        public override GameMove<T> DoMove(IGameStateProvider stateProvider)
        {
            string resultsDir = Path.Combine(this.cwd, this.id);
            Console.WriteLine($"Enure results directory exists: {resultsDir}");
            Directory.CreateDirectory(resultsDir);
            string statePath = Path.Combine(resultsDir, "state.json");
            string movePath = Path.Combine(resultsDir, "move.json");
            string args = $"{this.id} \"{statePath}\" \"{movePath}\"";

            Console.WriteLine($"Write state to file: {statePath}");
            File.WriteAllText(statePath, JsonConvert.SerializeObject(stateProvider.ProvideState<T>()));

            Console.WriteLine($"Starting player process: {this.exe} {string.Join(" ", args)}");
            Process process = Process.Start(this.exe, args);
            process.WaitForExit();

            Console.WriteLine($"Read new move from file: {movePath}");
            GameMove<T> move = JsonConvert.DeserializeObject<GameMove<T>>(File.ReadAllText(movePath));

            // TODO: clean up files

            return move;
        }
    }
}
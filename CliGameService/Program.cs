using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace CodeCompete.GameService
{
    class CliGameService
    {
        static void Main(string[] args)
        {
            string directory = Environment.CurrentDirectory;

            if (args.Length > 0)
            {
                string candidate = args[0];
                if (Directory.Exists(candidate))
                {
                    directory = candidate;
                }
                else
                {
                    Console.WriteLine($"Using CWD because provided directory could not be found: {candidate}");
                }
            }

            string resultsDirectory = Path.Combine(directory, "results");
            if (!Directory.Exists(resultsDirectory)) {
                Directory.CreateDirectory(resultsDirectory);
            }

            string[] gameDirectories = Directory.GetDirectories(directory).Where(d => d != resultsDirectory).ToArray();

            int gameChoice;
            while (!PromptUserForChoice("game", gameDirectories, out gameChoice)) {};
            string gameDir = gameDirectories[gameChoice];
            string[] playerDirectories = Directory.GetDirectories(gameDir);

            int playerChoice1;
            while (!PromptUserForChoice("player", playerDirectories, out playerChoice1)) {};
            string playerDirectory1 = playerDirectories[playerChoice1];

            int playerChoice2;
            while (!PromptUserForChoice("player", playerDirectories, out playerChoice2)) {};
            string playerDirectory2 = playerDirectories[playerChoice2];

            string playerId1 = "Player1";//args[1];
            string playerExe1 = Directory.GetFiles(playerDirectory1).First(f => Path.GetExtension(f) == ".exe");//args[2]
            string playerId2 = "Player2";//args[3];
            string playerExe2 = Directory.GetFiles(playerDirectory2).First(f => Path.GetExtension(f) == ".exe");//args[4]
            string resultsPath = resultsDirectory;//args[5];

            string gameExe = Directory.GetFiles(gameDir).First(f => Path.GetExtension(f) == ".exe");
            string gameArgs = $"{playerId1} {playerExe1} {playerId2} {playerExe2} {resultsPath}";

            Process gameProcess = Process.Start(gameExe, gameArgs);
            gameProcess.WaitForExit();
        }

        static bool PromptUserForChoice(string name, string[] options, out int choice, int maxAttempts = 5)
        {
            Console.WriteLine($"Pick a {name}:");
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i]}");
            }

            bool success = false;
            int attempt = 0;

            while (!success && attempt < maxAttempts)
            {
                Console.WriteLine($"Select a {name} above by typing the number and pressing enter");
                success = Int32.TryParse(Console.ReadLine(), out int parsedChoice);

                if (parsedChoice < 0 && parsedChoice >= options.Length) {
                    success = false;
                }

                if (success)
                {
                    choice = parsedChoice;

                    Console.WriteLine($"You selected {options[choice]}");

                    return true;
                }
                else
                {
                     attempt++;
                }
            }
            choice = -1;
            return false;
        }
    }
}

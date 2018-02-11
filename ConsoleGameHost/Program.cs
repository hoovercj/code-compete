using System;
using CodeCompeteDotNet;
using ConsoleTicTacToePlayer;
using SimpleComputerTicTacToePlayer;
using TicTacToeDotNet;

namespace ConsoleGameHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new ConsoleTicTacToePlayer.ConsoleTicTacToePlayer("1");
            var player2 = new SimpleComputerTicTacToePlayer.SimpleComputerTicTacToePlayer("2");
            var game = new TicTacToeGame(new GamePlayer[] { player1, player2});

            var result = game.PlayGame();
            var finalState = ((TicTacToeMove)(result.GameMoves[result.GameMoves.Length - 1])).Board;

            for (int i = 0; i < finalState.Length; i++)
            {
                var row = finalState[i];

                for (int j = 0; j < row.Length; j++)
                {
                    Console.Write(string.Format("{0} ", row[j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

            if (result.Winner != null)
            {
                Console.WriteLine("Winner: " + result.Winner.Id);
            }
        }
    }
}

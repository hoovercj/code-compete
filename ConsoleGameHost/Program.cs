using System;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.TicTacToe.Games;
using CodeCompete.DotNet.TicTacToe.Players;

namespace CodeCompete.DotNet.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new SimpleComputerPlayer();
            player1.Id = "1";
            var player2 = new ConsolePlayer();
            player2.Id = "2";
            var game = new CodeCompete.DotNet.TicTacToe.Games.TicTacToe(new GamePlayer<string[][]>[] { player1, player2});

            var result = game.PlayGame();
            var finalState = result.GameMoves[result.GameMoves.Length - 1].State;

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

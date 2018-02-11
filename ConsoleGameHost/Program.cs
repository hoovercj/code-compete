using System;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.TicTacToe;
using CodeCompete.DotNet.TicTacToe.Players;

namespace CodeComplete.DotNet.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var player1 = new ConsoleTicTacToePlayer("1");
            var player2 = new SimpleComputerTicTacToePlayer("2");
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

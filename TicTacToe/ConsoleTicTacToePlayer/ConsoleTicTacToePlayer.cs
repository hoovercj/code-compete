using System;
using System.Linq;
using System.Collections.Immutable;
using CodeCompete.DotNet.Interfaces;

namespace CodeCompete.DotNet.TicTacToe.Players
{
    public class ConsoleTicTacToePlayer : GamePlayer<string[][]>
    {
        private string id;

        public override string Id => id;

        public ConsoleTicTacToePlayer(string id)
        {
            this.id = id;
        }

        public override GameMove<string[][]> DoMove(IGameStateProvider stateProvider)
        {
            var state = stateProvider.ProvideState<string[][]>();
            var states = state.GameMoves;
            GameMove<string[][]> lastState = states[states.Length -1];
            string[][] board = lastState.State;

            this.PrintBoard(board);

            int chosenColumn = -1;
            int chosenRow = -1;

            do {
                Console.WriteLine("Choose where to place an X by selecting one of the available numbers from 1-9");

                string rawChoice = Console.ReadLine();
                if (!Int32.TryParse(rawChoice, out int parsedChoice))
                {
                    continue;
                }

                int choice = parsedChoice - 1;

                chosenRow = choice / board.Length;
                chosenColumn = choice % board.Length;
            } while(!ValidateChoice(board, chosenRow, chosenColumn));

            string[][] newBoard = board.Select(s => s.ToArray()).ToArray();

            newBoard[chosenRow][chosenColumn] = this.id;

            return new GameMove<string[][]>(this.Id, newBoard);
        }

        private bool ValidateChoice(string[][] board, int row, int col)
        {
            if (row >= board.Length || col >= board[row].Length)
            {
                return false;
            }

            return String.IsNullOrWhiteSpace(board[row][col]);
        }

        private void PrintBoard(string[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                var row = board[i];

                for (int j = 0; j < row.Length; j++)
                {
                    string playerId = row[j];
                    string symbol = " ";
                    if (playerId == this.Id)
                    {
                        symbol = "X";
                    }
                    else if (!String.IsNullOrWhiteSpace(playerId))
                    {
                        symbol = "O";
                    }
                    else
                    {
                        symbol = ((j + 1) + (row.Length * i)).ToString();
                    }

                    Console.Write($"{symbol} ");
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

        }
    }
}

using System;
using System.Collections.Immutable;

namespace console_tictactoe
{
    public class ConsoleTicTacToePlayer : GamePlayer
    {
        private string id;

        public string Id => id;

        public ConsoleTicTacToePlayer(string id)
        {
            this.id = id;
        }

        public IGameMove DoMove(IGameState state)
        {
            var states = state.GameMoves;
            TicTacToeMove lastState = (TicTacToeMove)states[states.Length -1];
            var board = lastState.Board;

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

            ImmutableArray<string> newRow = board[chosenRow].SetItem(chosenColumn, this.Id);
            ImmutableArray<ImmutableArray<string>> newBoard = lastState.Board.SetItem(chosenRow, newRow);

            return new TicTacToeMove(this.Id, newBoard);
        }

        private bool ValidateChoice(ImmutableArray<ImmutableArray<string>> board, int row, int col)
        {
            if (row >= board.Length || col >= board[row].Length)
            {
                return false;
            }

            return String.IsNullOrWhiteSpace(board[row][col]);
        }

        private void PrintBoard(ImmutableArray<ImmutableArray<string>> board)
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

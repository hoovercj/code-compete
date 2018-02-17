using System;
using System.Linq;
using System.Collections.Immutable;
using CodeCompete.DotNet.Interfaces;

namespace CodeCompete.DotNet.TicTacToe.Players
{
    public class SimpleComputerTicTacToePlayer : GamePlayer<string[][]>
    {
        private readonly string id;

        public override string Id => id;

        public SimpleComputerTicTacToePlayer(string id)
        {
            this.id = id;
        }

        public override GameMove<string[][]> DoMove(GameState<string[][]> state)
        {
            var states = state.GameMoves;
            GameMove<string[][]> lastState = states[states.Length -1];

            string[][] board = (string[][])lastState.State;

            for (int r = 0; r < board.Length; r++)
            {
                string[] row = board[r];
                for (int c = 0; c < row.Length; c++)
                {
                    if (String.IsNullOrWhiteSpace(row[c]))
                    {
                        string[][] newBoard = board.Select(s => s.ToArray()).ToArray();
                        newBoard[r][c] = this.Id;

                        return new GameMove<string[][]>(this.Id, newBoard);
                    }
                }
            }

            // If we got here then there was no valid move for us to make
            return lastState;
        }
    }
}

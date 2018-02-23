using System;
using System.Linq;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.TicTacToe;

namespace CodeCompete.DotNet.TicTacToe
{
    public class SimpleComputerPlayer : GamePlayer<Move>
    {
        public SimpleComputerPlayer()
        {
        }

        public override GameMove<Move> DoMove(IGameStateProvider stateProvider)
        {
            var state = stateProvider.ProvideState<Move>();
            var states = state.GameMoves;
            GameMove<Move> lastState = states[states.Length -1];

            string[][] board = lastState.State.Board;

            for (int r = 0; r < board.Length; r++)
            {
                string[] row = board[r];
                for (int c = 0; c < row.Length; c++)
                {
                    if (string.IsNullOrWhiteSpace(row[c]))
                    {
                        string[][] newBoard = board.Select(s => s.ToArray()).ToArray();
                        newBoard[r][c] = this.Id;

                        return new GameMove<Move>(this.Id, new Move { Board = newBoard });
                    }
                }
            }

            // If we got here then there was no valid move for us to make
            return lastState;
        }
    }
}
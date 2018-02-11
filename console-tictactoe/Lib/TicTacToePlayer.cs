using System;
using System.Collections.Immutable;

namespace console_tictactoe
{
    public class TicTacToePlayer : GamePlayer
    {
        private string id;

        public string Id => id;

        public TicTacToePlayer(string id)
        {
            this.id = id;
        }

        public IGameMove DoMove(IGameState state)
        {
            var states = state.GameMoves;
            TicTacToeMove lastState = (TicTacToeMove)states[states.Length -1];

            for (int r = 0; r < lastState.Board.Length; r++)
            {
                ImmutableArray<string> row = lastState.Board[r];
                for (int c = 0; c < row.Length; c++)
                {
                    if (String.IsNullOrWhiteSpace(row[c]))
                    {
                        ImmutableArray<string> newRow = row.SetItem(c, this.Id);
                        ImmutableArray<ImmutableArray<string>> newBoard = lastState.Board.SetItem(r, newRow);

                        return new TicTacToeMove(this.Id, newBoard);
                    }
                }
            }

            // If we got here then there was no valid move for us to make
            return lastState;
        }
    }
}

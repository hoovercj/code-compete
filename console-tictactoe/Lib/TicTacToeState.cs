using System.Collections.Generic;
using System.Collections.Immutable;

namespace console_tictactoe
{
    using TicTacToeBoard = ImmutableArray<ImmutableArray<string>>;

    public class TicTacToeMove : IGameMove
    {

        public string PlayerId { get { return lastPlayerId; } }

        public object State { get { return this.Board; } }

        public readonly string lastPlayerId;
        public readonly TicTacToeBoard Board;

        public TicTacToeMove() : this(null, ImmutableArray.Create(
                ImmutableArray.Create<string>(null, null, null),
                ImmutableArray.Create<string>(null, null, null),
                ImmutableArray.Create<string>(null, null, null)
            )) { }

        public TicTacToeMove(string lastPlayerId, TicTacToeBoard board)
        {
            this.lastPlayerId = lastPlayerId;
            this.Board = board;
        }
    }
}
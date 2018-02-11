using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CodeCompete.DotNet.Interfaces;

namespace CodeCompete.DotNet.Implementation
{
    public abstract class AbstractGame : Game
    {
        // Serializable functions to satisfy the IGameState interface
        public IPlayer[] Players { get { return players.ToArray(); } }
        public IGameMove[] GameMoves { get { return moves.ToArray(); } }

        public abstract IPlayer Winner { get; }

        public abstract bool IsOver { get; }

        // Runtime instance variables
        protected ImmutableArray<GamePlayer> players;
        protected ImmutableArray<IGameMove> moves;

        public IGameState PlayGame()
        {
            while(!this.IsOver)
            {
                this.RequestMove(this.CurrentPlayer);
            }

            return this;
        }

        private void RequestMove(GamePlayer player)
        {
            BeforeMove();
            IGameMove newMove = this.DoMove(player, this);
            this.moves = this.moves.Add(newMove);
            AfterMove();
        }

        private IGameMove DoMove(GamePlayer player, IGameState state)
        {
            IGameMove move = player.DoMove(state);
            if (this.ValidateMove(state, move))
            {
                return move;
            }

            throw new Exception("Illegal move");
        }

        protected abstract GamePlayer CurrentPlayer { get; }

        protected abstract bool ValidateMove(IGameState game, IGameMove move);

        protected virtual void BeforeMove() {}
        protected virtual void AfterMove() {}
    }
}

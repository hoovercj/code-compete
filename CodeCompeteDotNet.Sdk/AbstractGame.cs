using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CodeCompete.DotNet.Interfaces;

namespace CodeCompete.DotNet.Implementation
{
    public class IllegalMoveException : Exception
    {
        public IllegalMoveException() {}
        public IllegalMoveException(string message) : base(message) {}
        public IllegalMoveException(string message, Exception inner) : base(message, inner) {}
    }

    public abstract class AbstractGame<T> : IGame<T>
    {
        // Abstract/virtual properties and methods available to subclasses
        // to execute the game
        protected abstract GamePlayer<T> Winner { get; }

        protected abstract bool IsOver { get; }

        protected abstract GamePlayer<T> CurrentPlayer { get; }

        protected abstract bool ValidateMove(GameState<T> game, GameMove<T> move);

        // Immutable collections used by subclasses but controlled by this class
        protected ImmutableArray<GamePlayer<T>> players;
        protected ImmutableArray<GameMove<T>> moves;

        // Public property used to serialize the game state
        public GameState<T> GameState {
            get {
                return new GameState<T>(
                    players.ToArray(),
                    moves.ToArray(),
                    this.Winner,
                    this.IsOver
                );
            }
        }

        // Public Interface Methods

        public GameState<T> PlayGame()
        {
            while(!this.GameState.IsOver)
            {
                this.RequestMove(this.CurrentPlayer);
            }

            return this.GameState;
        }

        public virtual void BeforeMove() {}
        public virtual void AfterMove() {}

        // Private methods

        private void RequestMove(GamePlayer<T> player)
        {
            BeforeMove();
            GameMove<T> newMove = this.DoMove(player, this.GameState);
            this.moves = this.moves.Add(newMove);
            AfterMove();
        }

        private GameMove<T> DoMove(GamePlayer<T> player, GameState<T> state)
        {

            GameMove<T> move = player.DoMove(new SimpleGameStateProvider(state));
            if (this.ValidateMove(state, move))
            {
                return move;
            }

            throw new IllegalMoveException("Illegal move");
        }
    }
}

﻿using System;

namespace CodeCompete.DotNet.Interfaces
{
    // Runtime Interfaces
    public interface IGame<T>
    {
        GameState<T> GameState { get; }

        GameState<T> PlayGame();

        void BeforeMove();

        void AfterMove();
    }

    // Serialization Interfaces
    public class GameState<T>
    {
        public GamePlayer<T>[] Players { get; }

        public GameMove<T>[] GameMoves { get; }

        public GamePlayer<T> Winner { get; }

        public bool IsOver { get; }

        public GameState(GamePlayer<T>[] players, GameMove<T>[] gameMoves, GamePlayer<T> winner, bool isOver)
        {
            this.Players = players;
            this.GameMoves = gameMoves;
            this.Winner = winner;
            this.IsOver = isOver;
        }
    }

    public class GameMove<T>
    {
        public virtual string PlayerId { get; }

        public virtual T State { get; }

        public GameMove(string playerId, T state)
        {
            this.PlayerId = playerId;
            this.State = state;
        }
    }

    public class GamePlayer<T>
    {
        public virtual string Id { get; }

        public virtual GameMove<T> DoMove(GameState<T> game) { return null; }
    }
}
using System;

namespace console_tictactoe
{
    public interface IGameState
    {
        IPlayer[] Players { get; }

        IGameMove[] GameMoves { get; }

        IPlayer Winner { get; }

        bool IsOver { get; }
    }

    public interface IGameMove
    {
        string PlayerId { get; }

        object State { get; }
    }

    public interface IPlayer
    {
        string Id { get; }
    }
}
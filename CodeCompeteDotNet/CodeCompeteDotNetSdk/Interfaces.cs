using System;

namespace CodeCompete.DotNet.Interfaces
{
    // Runtime Interfaces
    public interface Game : IGameState
    {
        IGameState PlayGame();
    }

    public interface GamePlayer : IPlayer {
        IGameMove DoMove(IGameState game);
    }

    // Serialization Interfaces
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

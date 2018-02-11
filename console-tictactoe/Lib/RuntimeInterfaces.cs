using System;
using System.Collections.Immutable;

namespace console_tictactoe
{
    public interface Game : IGameState
    {
        // IGameState PlayGame(GamePlayer[] players, IGameState initialState);
        IGameState PlayGame();
    }

    public interface GamePlayer : IPlayer {
        IGameMove DoMove(IGameState game);
    }
}

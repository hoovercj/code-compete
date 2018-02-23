using System;
using System.Linq;
using CodeCompete.DotNet.Interfaces;

namespace CodeCompete.DotNet.GameName
{
    public class PlayerName : GamePlayer<object>
    {
        public PlayerName() {}

        public override GameMove<object> DoMove(IGameStateProvider stateProvider)
        {
            return null;
        }
    }
}
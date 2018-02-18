using System.Collections.Generic;
using System.Collections.Immutable;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;
using Newtonsoft.Json;

namespace CodeCompete.DotNet.GameName.Games
{
    public class GameName : AbstractGame<object>
    {

        protected override GamePlayer<object> Winner => null;
        protected override bool IsOver => false;
        protected override GamePlayer<object> CurrentPlayer => this.players[0];

        public GameName(GamePlayer<object>[] players)
        {
        }

        public override void BeforeMove() {}
        public override void AfterMove() {}

        protected override bool ValidateMove(GameState<object> game, GameMove<object> move)
        {
            return true;
        }
    }
}
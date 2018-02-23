using System.Collections.Generic;
using System.Collections.Immutable;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;
using Newtonsoft.Json;

namespace CodeCompete.DotNet.GameName
{
    public class GameName : AbstractGame<Move>
    {

        protected override GamePlayer<Move> Winner => null;
        protected override bool IsOver => false;
        protected override GamePlayer<Move> CurrentPlayer => this.players[0];

        public GameName(GamePlayer<Move>[] players)
        {
        }

        public override void BeforeMove() {}
        public override void AfterMove() {}

        protected override bool ValidateMove(GameState<Move> game, GameMove<Move> move)
        {
            return true;
        }
    }
}
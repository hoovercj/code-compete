// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using CodeCompete.DotNet.Implementation;
// using CodeCompete.DotNet.Interfaces;
// using System;

// namespace CodeCompete.DotNet.Implementation.Test
// {
//     [TestClass]
//     public class AbstractGameTestSuite
//     {
//         AbstractGame sut;

//         [TestInitialize()]
//         public void Initialize()
//         {
//             sut = new MockAbstractGame();
//         }

//         [TestMethod]
//         public void PlayersIsNullBeforeGameStart()
//         {
//             Assert.IsNull(sut.Players);
//         }

//         [TestMethod]
//         public void GameMovesIsNullBeforeGameStart()
//         {
//             Assert.IsNull(sut.GameMoves);
//         }

//         // TODO: Change AbstractGame so that it can be tested
//     }

//     class MockAbstractGame : AbstractGame
//     {
//         Func<GameState, IGameMove, bool> validateMove => (state, move) => true;
//         Func<bool> isOver => () => false;
//         Func<IPlayer> winner => () => null;

//         Func<GamePlayer> currentPlayer => () => null;

//         public override IPlayer Winner => winner();

//         public override bool IsOver => isOver();

//         protected override GamePlayer CurrentPlayer => currentPlayer();

//         protected override bool ValidateMove(GameState game, IGameMove move)
//         {
//             return validateMove(game, move);
//         }
//     }
// }

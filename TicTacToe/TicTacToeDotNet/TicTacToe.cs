using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;

namespace CodeCompete.DotNet.TicTacToe
{
    using TicTacToeBoard = ImmutableArray<ImmutableArray<string>>;

    public class TicTacToeMove : IGameMove
    {
        public string PlayerId { get { return lastPlayerId; } }

        public object State { get { return this.Board; } }

        public readonly string lastPlayerId;
        public readonly TicTacToeBoard Board;

        public TicTacToeMove() : this(null, ImmutableArray.Create(
                ImmutableArray.Create<string>(null, null, null),
                ImmutableArray.Create<string>(null, null, null),
                ImmutableArray.Create<string>(null, null, null)
            )) { }

        public TicTacToeMove(string lastPlayerId, TicTacToeBoard board)
        {
            this.lastPlayerId = lastPlayerId;
            this.Board = board;
        }
    }

    public class TicTacToeGame : AbstractGame
    {
        private bool isOver;
        private IPlayer winner;
        private int currentPlayerIndex = 0;

        private ImmutableDictionary<string, int> playerToNumberMap;

        public override IPlayer Winner => winner;
        public override bool IsOver => isOver;
        protected override GamePlayer CurrentPlayer => players[currentPlayerIndex];

        public TicTacToeGame(GamePlayer[] players)
        {
            if (players.Length != 2) throw new ArgumentOutOfRangeException(nameof(players), "Tic Tac Toe requires exactly 2 players");
            if (players[0].Id == players[1].Id) throw new ArgumentException(nameof(players), "Players cannot have the same Id");

            this.players = players.ToImmutableArray();

            playerToNumberMap = new Dictionary<string, int> {
                { players[0].Id, -1 },
                { players[1].Id, 1 }
            }
            .ToImmutableDictionary();

            TicTacToeMove move = new TicTacToeMove();
            this.moves = ImmutableArray.Create((IGameMove)move);
        }

        protected GamePlayer GetWinner(TicTacToeMove state)
        {
            int[] colSums = new int[3];
            int[] rowSums = new int[3];
            int diagSum1 = 0;
            int diagSum2 = 0;

            GamePlayer winner = null;

            for (int r = 0; r < state.Board.Length; r++)
            {
                ImmutableArray<string> row = state.Board[r];
                for (int c = 0; c < row.Length; c++)
                {
                    string currentPlayerId = row[c];
                    if (String.IsNullOrWhiteSpace(currentPlayerId))
                    {
                        continue;
                    }

                    int currentPlayerNum = playerToNumberMap[currentPlayerId];

                    rowSums[r] += currentPlayerNum;
                    if ((winner = CheckScore(rowSums[r])) != null) { return winner; }

                    colSums[c] += currentPlayerNum;
                    if ((winner = CheckScore(colSums[c])) != null) { return winner; }

                    if (r == c) {
                        diagSum1 += currentPlayerNum;
                        if ((winner = CheckScore(diagSum1)) != null) { return winner; }
                    }

                    if (r + c == row.Length - 1)
                    {
                        diagSum2 += currentPlayerNum;
                        if ((winner = CheckScore(diagSum2)) != null) { return winner; }
                    }
                }
            }

            return null;
        }

        private GamePlayer CheckScore(int score)
        {
            // TODO: abstract the connection between players and the score
            if (score == -3)
            {
                return this.players[0];
            }
            else if (score == 3)
            {
                return this.players[1];
            }
            else
            {
                return null;
            }
        }

        protected bool MovesLeft(TicTacToeMove state)
        {
            for (int r = 0; r < state.Board.Length; r++)
            {
                ImmutableArray<string> row = state.Board[r];
                for (int c = 0; c < row.Length; c++)
                {
                    if (String.IsNullOrWhiteSpace(row[c])) {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override bool ValidateMove(IGameState game, IGameMove move)
        {
            TicTacToeMove typedMove = (TicTacToeMove)move;

            string expectedPlayerId = this.CurrentPlayer.Id;
            string actualPlayerId = typedMove.PlayerId;

            if (actualPlayerId != expectedPlayerId)
            {
                throw new Exception($"Move was made by the wrong player. Expected {expectedPlayerId}, received {actualPlayerId}.");
            }

            return true;

            // TODO: proper validation;
        }

        protected override void AfterMove()
        {
            TicTacToeMove lastMove = (TicTacToeMove)this.moves[this.moves.Length - 1];
            this.winner = this.GetWinner(lastMove);
            this.isOver = this.winner != null || !this.MovesLeft(lastMove);

            this.currentPlayerIndex = ++this.currentPlayerIndex % 2;
        }
    }
}

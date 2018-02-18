using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using CodeCompete.DotNet.Interfaces;
using CodeCompete.DotNet.Implementation;
using Newtonsoft.Json;

namespace CodeCompete.DotNet.TicTacToe
{
    public class TicTacToeGame : AbstractGame<string[][]>
    {
        private bool isOver;
        private GamePlayer<string[][]> winner;
        private int currentPlayerIndex = 0;

        private ImmutableDictionary<string, int> playerToNumberMap;

        protected override GamePlayer<string[][]> Winner => winner;
        protected override bool IsOver => isOver;
        protected override GamePlayer<string[][]> CurrentPlayer => players[currentPlayerIndex];

        public TicTacToeGame(GamePlayer<string[][]>[] players)
        {
            if (players.Length != 2) throw new ArgumentOutOfRangeException(nameof(players), "Tic Tac Toe requires exactly 2 players");
            if (players[0].Id == players[1].Id) throw new ArgumentException(nameof(players), "Players cannot have the same Id");

            this.players = players.ToImmutableArray();

            playerToNumberMap = new Dictionary<string, int> {
                { players[0].Id, -1 },
                { players[1].Id, 1 }
            }
            .ToImmutableDictionary();

            GameMove<string[][]> move = new GameMove<string[][]>(null, new string[][] {
                new string[] {null, null, null},
                new string[] {null, null, null},
                new string[] {null, null, null}
            });

            this.moves = ImmutableArray.Create(move);
        }

        public override void AfterMove()
        {
            var lastMove = this.moves[this.moves.Length - 1];
            this.winner = this.GetWinner(lastMove);
            this.isOver = this.winner != null || !this.MovesLeft(lastMove);

            this.currentPlayerIndex = ++this.currentPlayerIndex % 2;
        }

        protected GamePlayer<string[][]> GetWinner(GameMove<string[][]> move)
        {
            int[] colSums = new int[3];
            int[] rowSums = new int[3];
            int diagSum1 = 0;
            int diagSum2 = 0;

            GamePlayer<string[][]> winner = null;

            string[][] board = move.State;

            for (int r = 0; r < board.Length; r++)
            {
                string[] row = board[r];
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

        private GamePlayer<string[][]> CheckScore(int score)
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

        protected override bool ValidateMove(GameState<string[][]> game, GameMove<string[][]> move)
        {

            string expectedPlayerId = this.CurrentPlayer.Id;
            string actualPlayerId = move.PlayerId;

            if (actualPlayerId != expectedPlayerId)
            {
                throw new Exception($"Move was made by the wrong player. Expected {expectedPlayerId}, received {actualPlayerId}.");
            }

            return true;

            // TODO: proper validation;
        }

        private bool MovesLeft(GameMove<string[][]> move)
        {
            string[][] board = move.State;

            for (int r = 0; r < board.Length; r++)
            {
                string[] row = board[r];
                for (int c = 0; c < row.Length; c++)
                {
                    if (String.IsNullOrWhiteSpace(row[c])) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

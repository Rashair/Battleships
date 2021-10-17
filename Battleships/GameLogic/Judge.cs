using System;
using Battleships.Grid;

namespace Battleships.GameLogic
{
    public class Judge
    {
        private readonly Board board1;
        private readonly Board board2;

        public Judge(Board board1, Board board2)
        {
            this.board1 = board1;
            this.board2 = board2;
        }

        public bool Shoot(Guid playerToken, int y, int x)
        {
            var board = GetEnemyBoardForPlayerToken(playerToken);
            return board.Shoot(y, x);
        }

        public bool DidPlayerWin(Guid playerToken)
        {
            var board = GetEnemyBoardForPlayerToken(playerToken);
            return board.UpFields == 0;
        }

        private Board GetEnemyBoardForPlayerToken(Guid playerToken)
        {
            if (board1.PlayerToken == playerToken)
            {
                return board2;
            }
            else if (board2.PlayerToken == playerToken)
            {
                return board1;
            }
            else
            {
                throw new InvalidOperationException($"Invalid player token: {playerToken}");
            }
        }
    }
}

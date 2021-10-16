using Battleships.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class Player
    {
        private readonly Board ownBoard;
        private readonly Random random;
        private readonly bool[][] shootingBoard;

        private int BoardWidth => ownBoard.Width;
        private int BoardHeight => ownBoard.Height;

        public Guid Token { get; }

        public Player(Board board, Random? random = null)
        {
            this.Token = Guid.NewGuid();
            this.ownBoard = board;
            board.InitPlayerToken(this);

            this.random = random ?? new();

            this.shootingBoard = new bool[BoardHeight][];
            for (int i = 0; i < BoardHeight; ++i)
                this.shootingBoard[i] = new bool[BoardWidth];
        }

        public bool ShootBoardField(Judge judge)
        {
            int y = random.Next(BoardHeight);
            int x = random.Next(BoardWidth);

            int initY = y;
            int initX = x;
            while (shootingBoard[y][x])
            {
                (y, x) = GetNextFieldWithinBoard(y, x);

                if (x == initX && y == initY)
                {
                    throw new InvalidOperationException("All fields were already shot");
                }
            }

            var wasShotSuccessful = judge.Shoot(Token, y, x);
            shootingBoard[y][x] = true;
            return wasShotSuccessful;
        }

        private (int y, int x) GetNextFieldWithinBoard(int y, int x)
        {
            ++x;
            if (x == BoardWidth)
            {
                x = 0;
                ++y;
                if (y == BoardHeight)
                    y = 0;
            }

            return (y, x);
        }
    }
}

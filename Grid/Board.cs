using System;
using Battleships.Ships;

namespace Battleships.Grid
{
    public class Board
    {
        private static readonly Random random = new();

        private readonly Field[][] board;

        public int Width => board[0].Length;
        public int Height => board.Length;

        public Board(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentException("Argument cannot be less or equal to 0", nameof(width));
            if (height <= 0)
                throw new ArgumentException("Argument cannot be less or equal to 0", nameof(height));

            board = new Field[height][];
            for (int i = 0; i < height; ++i)
            {
                board[i] = new Field[width];
            }
        }


        public void Init(Ship[] ships)
        {
            foreach (var ship in ships)
            {
                var position = GetRandomPositionForShip(ship.Size);
                Put(ship, position);
            }
        }

        private Position GetRandomPositionForShip(int size)
        {
            Direction dir = random.Next(2) == 0 ? Direction.Down : Direction.Right;
            int maxHeight = Height;
            int maxWidth = Width;
            if (dir == Direction.Down)
                maxHeight -= size;
            else // dir == Direction.Right
                maxWidth -= size;

            int yStart = random.Next(maxHeight);
            int xStart = random.Next(maxWidth);

            // TODO: Validate if not taken

            return new Position
            {
                YStart = yStart,
                XStart = xStart,
                Dir = dir
            };
        }

        private void Put(Ship ship, Position pos)
        {

        }
    }
}

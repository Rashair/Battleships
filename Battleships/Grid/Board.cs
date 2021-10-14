using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Ships;

namespace Battleships.Grid
{
    public class Board
    {
        private readonly Random random;
        private readonly Field[][] board;

        public int Width => board[0].Length;
        public int Height => board.Length;
        public int UpFields { get; set; }

        public Field this[int y, int x]
        {
            get
            {
                if (y >= Height)
                    throw new ArgumentException("Y can't be greater than height", nameof(y));
                if (x >= Width)
                    throw new ArgumentException("X can't be greater than width", nameof(x));

                return board[y][x];
            }
        }

        public Board(int width, int height, Random random = null)
        {
            this.random = random ?? new();

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
            var areShipsValid = AreShipsValidForInit(ships, out var errors);
            if (!areShipsValid)
                throw new ArgumentException($"Ships are not valid for this board: {string.Join(Environment.NewLine, errors)}",
                    nameof(ships));

            foreach (var ship in ships)
            {
                var position = GetRandomPositionForShip(ship.Size);
                Put(ship, position);
            }
        }

        public bool AreShipsValidForInit(Ship[] ships, out IEnumerable<string> errors)
        {
            errors = BrokenRules(ships);
            return !errors.Any();
        }

        private IEnumerable<string> BrokenRules(Ship[] ships)
        {
            yield break;
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

using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Ships;

namespace Battleships.Grid
{
    public class Board
    {
        public const int MaxPositioningAttempts = 1000;

        private readonly Random random;
        private readonly Field[][] board;

        public int Width => board[0].Length;
        public int Height => board.Length;
        public int BoardArea => Height * Width;
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


        public void Init(IEnumerable<Ship> ships)
        {
            var areShipsValid = AreShipsValidForInit(ships, out var errors);
            if (!areShipsValid)
                throw new ArgumentException($"Ships are not valid for this board: {string.Join(Environment.NewLine, errors)}",
                    nameof(ships));

            foreach (var ship in ships)
            {
                var position = GetRandomValidPositionForShip(ship.Size);
                Put(ship, position);
            }
        }

        public bool AreShipsValidForInit(IEnumerable<Ship> ships, out IEnumerable<string> errors)
        {
            errors = BrokenRules(ships);
            return !errors.Any();
        }

        private IEnumerable<string> BrokenRules(IEnumerable<Ship> ships)
        {
            int shipsAreaSum = 0;
            foreach (var ship in ships)
            {
                if (ship.Size > Width && ship.Size > Height)
                {
                    yield return $"Ship size: {ship.Size} cannot exceed both board dimensions: {Height}x{Width}!";
                }
                shipsAreaSum += ship.Size;
            }

            if (shipsAreaSum > BoardArea)
                yield return $"Sum of ships size:{shipsAreaSum} cannot exceed board area: {BoardArea}!";
        }

        private Position GetRandomValidPositionForShip(int size)
        {
            Position pos;
            int attempt = 0;
            var allowedDirections = GetDirectionsAllowedForShip(size).ToList();
            do
            {
                ++attempt;
                var dir = allowedDirections[random.Next(allowedDirections.Count)];

                int maxHeight = Height - size * dir.GetYCoefficient();
                int maxWidth = Width - size * dir.GetXCoefficient();
                int yStart = random.Next(maxHeight);
                int xStart = random.Next(maxWidth);

                pos = new Position
                {
                    YStart = yStart,
                    XStart = xStart,
                    Dir = dir
                };
            } while (attempt < MaxPositioningAttempts && !IsPositionValid(pos, size));

            if (attempt == MaxPositioningAttempts)
                throw new TimeoutException("Unable to place the ship correctly");

            return pos;
        }

        /// <summary>
        /// Return directions allowed for ship based on its size 
        ///  and dimensions of the board.
        /// </summary>
        private IEnumerable<Direction> GetDirectionsAllowedForShip(int shipSize)
        {
            if (shipSize <= Height)
                yield return Direction.Down;

            if (shipSize <= Width)
                yield return Direction.Right;
        }

        private bool IsPositionValid(Position pos, int shipSize)
        {
            int yCoefficient = pos.Dir.GetYCoefficient();
            int xCoefficient = pos.Dir.GetXCoefficient();

            for (int i = 0; i < shipSize; ++i)
            {
                if (board[pos.YStart + i * yCoefficient][pos.XStart + i * xCoefficient] != Field.Empty)
                    return false;
            }

            return true;
        }

        private void Put(Ship ship, Position pos)
        {
            SetFieldsForShip(ship, pos, Field.ShipUp);
        }

        private void Remove(Ship ship, Position pos)
        {
            SetFieldsForShip(ship, pos, Field.Empty);
        }

        private void SetFieldsForShip(Ship ship, Position pos, Field value)
        {
            int yCoefficient = pos.Dir.GetYCoefficient();
            int xCoefficient = pos.Dir.GetXCoefficient();

            for (int i = 0; i < ship.Size; ++i)
            {
                board[pos.YStart + i * yCoefficient][pos.XStart + i * xCoefficient] = value;
                if (value == Field.ShipUp)
                    ++UpFields;
            }
        }
    }
}

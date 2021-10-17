using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Battleships.Grid.Helpers;
using Battleships.Ships;

namespace Battleships.Grid
{
    public class Board
    {
        public const int MaxPositioningAttempts = 100;

        private readonly Random random;
        private readonly Field[][] board;

        public int Width => board[0].Length;
        public int Height => board.Length;
        public int BoardArea => Height * Width;
        public int UpFields { get; private set; }
        public Guid PlayerToken { get; private set; }

        public Board(int height, int width, Random? random = null)
        {
            this.random = random ?? new();
            if (height <= 0)
            {
                throw new ArgumentException("Argument cannot be less or equal to 0", nameof(height));
            }

            if (width <= 0)
            {
                throw new ArgumentException("Argument cannot be less or equal to 0", nameof(width));
            }

            board = new Field[height][];
            for (int i = 0; i < height; ++i)
            {
                board[i] = new Field[width];
            }
        }

        public Field this[int y, int x]
        {
            get
            {
                ValidateCoordinates(y, x);
                return board[y][x];
            }
        }

        private void ValidateCoordinates(int y, int x)
        {
            if (y >= Height)
            {
                throw new ArgumentException("Y coordinate can't be greater than height", nameof(y));
            }

            if (x >= Width)
            {
                throw new ArgumentException("X coordinate can't be greater than width", nameof(x));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ships"></param>
        /// <exception cref="System.TimeoutException">When board initialization fails <see cref="MaxPositioningAttempts"/> times</exception>
        public void Init(IEnumerable<Ship> ships)
        {
            if (UpFields > 0)
            {
                throw new InvalidOperationException($"Board is already initialized.");
            }

            var areShipsValid = AreShipsValidForInit(ships, out var errors);
            if (!areShipsValid)
            {
                throw new ArgumentException($"Ships are not valid for this board:{Environment.NewLine}" +
                    string.Join(Environment.NewLine, errors),
                    nameof(ships));
            }

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
                    yield return $"Ship size: {ship.Size} cannot exceed both board dimensions: {Height}x{Width}.";
                }
                shipsAreaSum += ship.Size;
            }

            if (shipsAreaSum > BoardArea)
            {
                yield return $"Sum of ships size: {shipsAreaSum} cannot exceed board area: {BoardArea}.";
            }
        }

        private ShipPosition GetRandomValidPositionForShip(int size)
        {
            ShipPosition pos;
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

                pos = new ShipPosition
                {
                    YStart = yStart,
                    XStart = xStart,
                    Dir = dir
                };
            } while (attempt < MaxPositioningAttempts && !IsPositionValid(pos, size));

            if (attempt == MaxPositioningAttempts)
            {
                throw new TimeoutException("Unable to place the ship correctly");
            }

            return pos;
        }

        /// <summary>
        /// Return directions allowed for ship based on its size 
        ///  and dimensions of the board.
        /// </summary>
        private IEnumerable<Direction> GetDirectionsAllowedForShip(int shipSize)
        {
            if (shipSize <= Height)
            {
                yield return Direction.Down;
            }

            if (shipSize <= Width)
            {
                yield return Direction.Right;
            }
        }

        private bool IsPositionValid(ShipPosition pos, int shipSize)
        {
            int yCoefficient = pos.Dir.GetYCoefficient();
            int xCoefficient = pos.Dir.GetXCoefficient();

            for (int i = 0; i < shipSize; ++i)
            {
                if (board[pos.YStart + i * yCoefficient][pos.XStart + i * xCoefficient] != Field.Empty)
                {
                    return false;
                }
            }

            return true;
        }

        private void Put(Ship ship, ShipPosition pos)
        {
            int yCoefficient = pos.Dir.GetYCoefficient();
            int xCoefficient = pos.Dir.GetXCoefficient();

            for (int i = 0; i < ship.Size; ++i)
            {
                board[pos.YStart + i * yCoefficient][pos.XStart + i * xCoefficient] = Field.Ship;
                ++UpFields;
            }
        }

        public bool Shoot(int y, int x)
        {
            ValidateCoordinates(y, x);

            switch (board[y][x])
            {
                case Field.Ship:
                    board[y][x] = Field.ShotShip;
                    --UpFields;
                    return true;

                case Field.ShotShip:
                    throw new InvalidOperationException("Cannot shot ship again.");

                case Field.Empty:
                    board[y][x] = Field.ShotEmpty;
                    return false;

                case Field.ShotEmpty:
                    throw new InvalidOperationException("Cannot shot empty field again.");

                default:
                    throw new InvalidOperationException("Unhandled Field case.");
            }
        }

        internal void InitPlayerToken(Guid playerToken)
        {
            if (playerToken == Guid.Empty)
            {
                throw new InvalidOperationException("Empty player token.");
            }

            if (PlayerToken != Guid.Empty)
            {
                throw new InvalidOperationException("Player token was already set.");
            }

            PlayerToken = playerToken;
        }

        public override string? ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('-', 2 * Width);
            stringBuilder.AppendLine();

            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    stringBuilder.Append(board[y][x].ToFormattedString());
                    stringBuilder.Append('|');
                }
                stringBuilder.AppendLine();
            }

            stringBuilder.Append('-', 2 * Width);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}

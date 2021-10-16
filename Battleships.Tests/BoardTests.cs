using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Grid;
using Battleships.Ships;
using Xunit;

namespace Battleships.Tests
{
    public class BoardTests
    {
        public const int DefaultTimeoutMs = 1000;

        private readonly Random random = new(1234);

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_Valid_TestData))]
        public void Init_ShouldGenerateAllShips(
            int boardHeight, int boardWidth,
            int carriersNum,
            int battleshipsNum,
            int cruisersNum,
            int submarinesNum,
            int destroyersNum)
        {
            // Arrange
            var board = new Board(boardHeight, boardWidth, random);
            var ships = GenerateShips(carriersNum, battleshipsNum, cruisersNum, submarinesNum, destroyersNum);

            // Act
            board.Init(ships);

            // Assert
            // Area sum matches
            var shipsAreaSum = ships.Sum(x => x.Size);
            Assert.Equal(shipsAreaSum, board.UpFields);

            // Up-fields set correctly
            int upFieldsCount = 0;
            for (int i = 0; i < board.Height; ++i)
            {
                for (int j = 0; j < board.Width; ++j)
                {
                    if (board[i, j] == Field.ShipUp)
                        ++upFieldsCount;
                }
            }
            Assert.Equal(upFieldsCount, board.UpFields);
        }

        public static IEnumerable<object[]> BoardAndShips_Valid_TestData =>
        new List<object[]>
        {
            new object[] { 10, 10,  // even size
                5, 4, 3, 3, 2 },
            new object[] { 9, 9,    // odd size
                5, 4, 3, 3, 2 },
            new object[] { 25, 5,   // very high
                5, 4, 3, 3, 2 },
            new object[] { 5, 24,   // very wide
                5, 4, 3, 3, 2 },
            new object[]{ 12, 12,  // only destroyers
                0, 0, 0, 0, 18},
            new object[]{ 20, 1,   // only carriers + full board
                20, 0, 0, 0, 0},
            new object[]{ 3, 3,    // small board
                1, 1, 1, 0, 0},
            new object[]{ 9, 9,    // empty
                0, 0, 0, 0, 0}
        };

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_Timeout_TestData))]
        public void Init_WhenItIsNotPossibleToGenerateValidPositions_ShouldThrowTimeoutException(
            int boardHeight, int boardWidth,
            int carriersNum,
            int battleshipsNum,
            int cruisersNum,
            int submarinesNum,
            int destroyersNum)
        {
            // Arrange
            var board = new Board(boardHeight, boardWidth, random);
            var ships = GenerateShips(carriersNum, battleshipsNum, cruisersNum, submarinesNum, destroyersNum);

            // Act && Assert
            var ex = Assert.Throws<TimeoutException>(() => board.Init(ships));
            Assert.Contains("unable to place", ex.Message, StringComparison.InvariantCultureIgnoreCase);
        }

        public static IEnumerable<object[]> BoardAndShips_Timeout_TestData =>
        new List<object[]>
        {
            new object[] { 2, 24,  // regression - generates ship outside of board
                    5, 4, 3, 3, 2 },
        };

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_ExceedArea_TestData))]
        public void Init_WhenShipsAreaExceedBoardArea_ShouldThrow(
            int boardHeight, int boardWidth,
            int carriersNum,
            int battleshipsNum,
            int cruisersNum,
            int submarinesNum,
            int destroyersNum)
        {
            // Arrange
            var board = new Board(boardHeight, boardWidth, random);
            var ships = GenerateShips(carriersNum, battleshipsNum, cruisersNum, submarinesNum, destroyersNum);

            // Act && Assert
            var ex = Assert.Throws<ArgumentException>(() => board.Init(ships));
            Assert.Contains("exceed", ex.Message, StringComparison.InvariantCultureIgnoreCase);
        }

        public static IEnumerable<object[]> BoardAndShips_ExceedArea_TestData =>
            new List<object[]>
            {
                new object[] { 4, 4,   // even size
                    5, 4, 3, 3, 2 },
                new object[] { 3, 3,   // odd size
                    5, 4, 3, 3, 2 },
                new object[] { 25, 2,  // very high
                    6, 4, 4, 4, 2 },
                new object[] { 2, 24,  // very wide
                    5, 4, 3, 3, 3 },
                new object[]{ 10, 10,  // only destroyers
                    0, 0, 0, 0, 21},
                new object[]{ 20, 1,   // only carriers
                    21, 0, 0, 0, 0},
                new object[]{ 3, 3,    // small board
                    6, 1, 1, 0, 0},
            };

        [Fact]
        public void Init_WhenShipSizeExceedsDimensionOfBoard_ShouldThrow()
        {

        }

        private List<Ship> GenerateShips(
           int carriersNum = 5,
           int battleshipsNum = 4,
           int cruisersNum = 3,
           int submarinesNum = 3,
           int destroyersNum = 2)
        {
            var ships = new List<Ship>();

            for (int i = 0; i < carriersNum; ++i)
                ships.Add(new Carrier());

            for (int i = 0; i < battleshipsNum; ++i)
                ships.Add(new Battleship());

            for (int i = 0; i < cruisersNum; ++i)
                ships.Add(new Cruiser());

            for (int i = 0; i < submarinesNum; ++i)
                ships.Add(new Submarine());

            for (int i = 0; i < destroyersNum; ++i)
                ships.Add(new Destroyer());

            return ships;
        }
    }
}

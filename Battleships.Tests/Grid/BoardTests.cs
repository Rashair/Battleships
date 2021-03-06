using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Settings;
using Battleships.Ships;
using Battleships.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Battleships.Grid.Tests
{
    public class BoardTests : TestsBase
    {
        private static Random RandomInstance => new(1234);

        public BoardTests()
        {
            serviceManager.InitDefault();
            serviceManager.Replace(ServiceDescriptor.Transient(f => RandomInstance));
        }

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_Valid_TestData))]
        public void Init_ShouldGenerateAllShips(GameSettings gameSettings)
        {
            // Arrange
            var (board, ships) = CreateBoardAndShips(gameSettings);

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
                    if (board[i, j] == Field.Ship)
                    {
                        ++upFieldsCount;
                    }
                }
            }
            Assert.Equal(upFieldsCount, board.UpFields);
        }

        private (Board board, List<Ship> ships) CreateBoardAndShips(GameSettings gameSettings)
        {
            var serviceProvider = serviceManager.GetServiceProvider();
            var boardFactory = serviceProvider.GetRequiredService<BoardFactory>();
            var board = boardFactory.Create(gameSettings.BoardHeight, gameSettings.BoardWidth);

            var shipsGenerator = serviceProvider.GetRequiredService<ShipsGenerator>();
            var ships = shipsGenerator.Generate(gameSettings);

            return (board, ships);
        }

        public static IEnumerable<object[]> BoardAndShips_Valid_TestData =>
        new List<object[]>
        {
            GenerateTestCase(new GameSettings   // even size 
            {
                BoardHeight = 10,
                BoardWidth = 10,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(2)
             .Add<Cruiser>(3)
             .Add<Submarine>(3)
             .Add<Destroyer>(4)
            ),
            GenerateTestCase(new GameSettings     // odd size 
            {
                BoardHeight = 9,
                BoardWidth = 9,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(2)
             .Add<Cruiser>(3)
             .Add<Submarine>(3)
             .Add<Destroyer>(4)
            ),
            GenerateTestCase(new GameSettings    // very high 
            {
                BoardHeight = 25,
                BoardWidth = 5,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(2)
             .Add<Cruiser>(3)
             .Add<Submarine>(3)
             .Add<Destroyer>(4)
            ),
            GenerateTestCase(new GameSettings    // very wide 
            {
                BoardHeight = 5,
                BoardWidth = 24,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(2)
             .Add<Cruiser>(3)
             .Add<Submarine>(3)
             .Add<Destroyer>(4)
            ),
            GenerateTestCase(new GameSettings   // only destroyers 
            {
                BoardHeight = 12,
                BoardWidth = 12,
            }
             .Add<Carrier>(18)
             .Add<Battleship>(0)
             .Add<Cruiser>(0)
             .Add<Submarine>(0)
             .Add<Destroyer>(0)
            ),
            GenerateTestCase(new GameSettings    // only destroyers + full board 
            {
                BoardHeight = 20,
                BoardWidth = 2,
            }
             .Add<Carrier>(0)
             .Add<Battleship>(0)
             .Add<Cruiser>(0)
             .Add<Submarine>(0)
             .Add<Destroyer>(10)
            ),
            GenerateTestCase(new GameSettings     // small board 
            {
                BoardHeight = 3,
                BoardWidth = 3,
            }
             .Add<Carrier>(0)
             .Add<Battleship>(0)
             .Add<Cruiser>(1)
             .Add<Submarine>(1)
             .Add<Destroyer>(1)
            ),
            GenerateTestCase(new GameSettings     // empty 
            {
                BoardHeight = 9,
                BoardWidth = 9,
            }
             .Add<Carrier>(0)
             .Add<Battleship>(0)
             .Add<Cruiser>(0)
             .Add<Submarine>(0)
             .Add<Destroyer>(0)
            ),
        };

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_Timeout_TestData))]
        public void Init_WhenItIsNotPossibleToGenerateValidPositions_ShouldThrowTimeoutException(
           GameSettings gameSettings)
        {
            // Arrange
            var (board, ships) = CreateBoardAndShips(gameSettings);

            // Act && Assert
            var ex = Assert.Throws<TimeoutException>(() => board.Init(ships));
            Assert.Contains("unable to place", ex.Message, StringComparison.InvariantCultureIgnoreCase);
        }

        public static IEnumerable<object[]> BoardAndShips_Timeout_TestData =>
        new List<object[]>
        {
            GenerateTestCase(new GameSettings   // regression - generates ship outside of board 
            {
                BoardHeight = 2,
                BoardWidth = 24,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(3)
             .Add<Cruiser>(3)
             .Add<Submarine>(4)
             .Add<Destroyer>(5)
            ),
        };

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_ExceedArea_TestData))]
        public void Init_WhenShipsAreaExceedBoardArea_ShouldThrow(
            GameSettings gameSettings)
        {
            // Arrange
            var (board, ships) = CreateBoardAndShips(gameSettings);

            // Act && Assert
            var ex = Assert.Throws<ArgumentException>(() => board.Init(ships));
            Assert.Matches("exceed.*?area", ex.Message);
        }

        public static IEnumerable<object[]> BoardAndShips_ExceedArea_TestData =>
            new List<object[]>
            {
               GenerateTestCase(new GameSettings    // 1 x 1 
                {
                BoardHeight = 1,
                BoardWidth = 1,
            }
             .Add<Carrier>(0)
             .Add<Battleship>(0)
             .Add<Cruiser>(0)
             .Add<Submarine>(0)
             .Add<Destroyer>(1)
                ),
                GenerateTestCase(new GameSettings    // even size 
                {
                BoardHeight = 4,
                BoardWidth = 4,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(2)
             .Add<Cruiser>(3)
             .Add<Submarine>(3)
             .Add<Destroyer>(4)
                ),
                GenerateTestCase(new GameSettings    // odd size 
                {
                BoardHeight = 3,
                BoardWidth = 3,
            }
             .Add<Carrier>(1)
             .Add<Battleship>(2)
             .Add<Cruiser>(3)
             .Add<Submarine>(3)
             .Add<Destroyer>(4)
                ),
                GenerateTestCase(new GameSettings   // very high 
                {
                BoardHeight = 25,
                BoardWidth = 2,
            }
             .Add<Carrier>(2)
             .Add<Battleship>(4)
             .Add<Cruiser>(4)
             .Add<Submarine>(4)
             .Add<Destroyer>(6)
                ),
                GenerateTestCase(new GameSettings   // very wide 
                {
                BoardHeight = 2,
                BoardWidth = 24,
            }
             .Add<Carrier>(3)
             .Add<Battleship>(3)
             .Add<Cruiser>(3)
             .Add<Submarine>(4)
             .Add<Destroyer>(5)
                ),
                GenerateTestCase(new GameSettings   // only carriers 
                {
                BoardHeight = 10,
                BoardWidth = 10,
            }
             .Add<Carrier>(21)
             .Add<Battleship>(0)
             .Add<Cruiser>(0)
             .Add<Submarine>(0)
             .Add<Destroyer>(0)
                ),
                GenerateTestCase(new GameSettings    // only destroyers 
                {
                BoardHeight = 20,
                BoardWidth = 1,
            }
             .Add<Carrier>(0)
             .Add<Battleship>(0)
             .Add<Cruiser>(0)
             .Add<Submarine>(0)
             .Add<Destroyer>(21)
                ),
                GenerateTestCase(new GameSettings     // small board 
                {
                BoardHeight = 3,
                BoardWidth = 3,
            }
             .Add<Carrier>(0)
             .Add<Battleship>(0)
             .Add<Cruiser>(1)
             .Add<Submarine>(1)
             .Add<Destroyer>(6)
                ),
            };

        [Theory(Timeout = DefaultTimeoutMs)]
        [MemberData(nameof(BoardAndShips_ShipSizeExceedsDimensions_TestData))]
        public void Init_WhenShipSizeExceedsDimensionOfBoard_ShouldThrow(
            GameSettings gameSettings)
        {
            var (board, ships) = CreateBoardAndShips(gameSettings);

            // Act && Assert
            var ex = Assert.Throws<ArgumentException>(() => board.Init(ships));
            Assert.Matches("exceed.*?dimension", ex.Message);
        }

        public static IEnumerable<object[]> BoardAndShips_ShipSizeExceedsDimensions_TestData =>
           new List<object[]>
           {
                GenerateTestCase(new GameSettings    // area fits, but not dimensions 
                {
                    BoardHeight = 3,
                    BoardWidth = 3,
            }
                 .Add<Carrier>(0)
                 .Add<Battleship>(1)
                 .Add<Cruiser>(0)
                 .Add<Submarine>(0)
                 .Add<Destroyer>(0)
                ),
                GenerateTestCase(new GameSettings    // area fits, but not dimensions 
                {
                    BoardHeight = 2,
                    BoardWidth = 3,
            }
                 .Add<Carrier>(0)
                 .Add<Battleship>(1)
                 .Add<Cruiser>(0)
                 .Add<Submarine>(0)
                 .Add<Destroyer>(0)
                ),
                GenerateTestCase(new GameSettings    // one fits, second not 
                {
                    BoardHeight = 2,
                    BoardWidth = 3,
            }
                 .Add<Carrier>(0)
                 .Add<Battleship>(1)
                 .Add<Cruiser>(0)
                 .Add<Submarine>(1)
                 .Add<Destroyer>(0)
                ),
                GenerateTestCase(new GameSettings    // only biggest does not fit 
                {
                    BoardHeight = 4,
                    BoardWidth = 4,
            }
                 .Add<Carrier>(1)
                 .Add<Battleship>(1)
                 .Add<Cruiser>(1)
                 .Add<Submarine>(1)
                 .Add<Destroyer>(1)
                ),
           };
    }
}

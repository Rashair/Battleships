using System;
using System.Linq;
using Battleships.Grid;
using Battleships.Ships;
using Xunit;

namespace Battleships.Tests
{
    public class BoardTests
    {
        private readonly Random random = new();

        [Fact]
        public void Init_ShouldGenerateAllShips()
        {
            // Arrange
            var board = new Board(10, 10, random);
            var ships = GetTestData_Init_ShouldGenerateAllShips();

            // Act
            board.Init(ships);

            // Assert
            var shipsAreaSum = ships.Select(x => x.Size).Sum();
            Assert.Equal(shipsAreaSum, board.UpFields);
        }

        private Ship[] GetTestData_Init_ShouldGenerateAllShips()
        {
            return Array.Empty<Ship>();
        }


        [Fact]
        public void Init_WhenShipsSizeExceedBoardArea_ShouldThrow()
        {

        }

        [Fact]
        public void Init_WhenShipSizeExceedsDimensionOfBoard_ShouldThrow()
        {

        }
    }
}

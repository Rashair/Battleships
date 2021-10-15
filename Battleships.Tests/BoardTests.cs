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

        private List<Ship> GetTestData_Init_ShouldGenerateAllShips(
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

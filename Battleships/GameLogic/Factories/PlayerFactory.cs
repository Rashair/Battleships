using System;
using Battleships.Grid;
using Battleships.IO;

namespace Battleships.GameLogic.Factories
{
    public class PlayerFactory
    {
        private readonly IIOManager ioManager;
        private readonly Random random;

        public PlayerFactory(IIOManager ioManager,
            Random random)
        {
            this.ioManager = ioManager;
            this.random = random;
        }

        public Player Create(Board board, string name)
        {
            return new Player(ioManager,
                board,
                name,
                new Random(random.Next()));
        }
    }
}

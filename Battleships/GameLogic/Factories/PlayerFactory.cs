using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Grid;
using Battleships.IO;

namespace Battleships.GameLogic
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

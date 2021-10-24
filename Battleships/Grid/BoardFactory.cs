using System;

namespace Battleships.Grid
{
    public class BoardFactory
    {
        private readonly Random random;

        public BoardFactory(Random random)
        {
            this.random = random;
        }

        public Board Create(int height, int width)
        {
            return new Board(height, width, new Random(random.Next()));
        }
    }
}

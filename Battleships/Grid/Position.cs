namespace Battleships.Grid
{
    public class Position
    {
        public int YStart { get; init; }
        public int XStart { get; init; }
        public Direction Dir { get; init; }

        public Position MoveBy(int x = 0, int y = 0)
        {
            return new Position()
            {
                XStart = XStart + x,
                YStart = YStart + y,
                Dir = Dir,
            };
        }
    }
}

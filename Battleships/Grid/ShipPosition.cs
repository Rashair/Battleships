namespace Battleships.Grid
{
    public record ShipPosition
    {
        public int YStart { get; init; }
        public int XStart { get; init; }
        public Direction Dir { get; init; }
    }
}

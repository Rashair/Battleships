namespace Battleships
{
    public record GameSettings
    {
        public int BoardHeight { get; init; } = 10;
        public int BoardWidth { get; init; } = 10;

        public int CarriersNum { get; init; } = 1;
        public int BattleshipsNum { get; init; } = 2;
        public int CruisersNum { get; init; } = 3;
        public int SubmarinesNum { get; init; } = 3;
        public int DestroyersNum { get; init; } = 4;
    }
}
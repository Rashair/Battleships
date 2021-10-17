namespace Battleships.Settings
{
    public record GameSettings
    {
        public int BoardHeight { get; init; } = 12;
        public int BoardWidth { get; init; } = 12;

        public int CarriersNum { get; init; } = 1;
        public int BattleshipsNum { get; init; } = 2;
        public int CruisersNum { get; init; } = 3;
        public int SubmarinesNum { get; init; } = 3;
        public int DestroyersNum { get; init; } = 4;

        public override string ToString()
        {
            const int padRight = 8;

            return $@"Board size: {BoardHeight}x{BoardHeight}

| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier         |  5   |      {CarriersNum}{"|",padRight}
| Battleship      |  4   |      {BattleshipsNum}{"|",padRight}
| Cruiser         |  3   |      {CruisersNum}{"|",padRight}
| Submarine       |  3   |      {SubmarinesNum}{"|",padRight}
| Destroyer       |  2   |      {DestroyersNum}{"|",padRight}
";
        }
    }
}
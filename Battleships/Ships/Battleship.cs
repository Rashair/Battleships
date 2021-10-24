namespace Battleships.Ships
{
    public sealed record Battleship : Ship
    {
        public override int Size => 4;

        public override int DefaultCount => 2;
    }
}

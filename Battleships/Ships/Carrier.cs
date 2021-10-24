namespace Battleships.Ships
{
    public sealed record Carrier : Ship
    {
        public override int Size => 5;

        public override int DefaultCount => 1;
    }
}

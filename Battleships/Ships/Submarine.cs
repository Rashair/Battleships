namespace Battleships.Ships
{
    public sealed record Submarine : Ship
    {
        public override int Size => 3;

        public override int DefaultCount => 3;
    }
}

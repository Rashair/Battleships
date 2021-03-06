namespace Battleships.Ships
{
    public sealed record Destroyer : Ship
    {
        public override int Size => 2;

        public override int DefaultCount => 4;
    }
}

namespace Battleships.Ships
{
    public sealed record Supercarrier : Ship
    {
        public override int Size => 10;

        public override int DefaultCount => 0;
    }
}

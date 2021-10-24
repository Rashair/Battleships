namespace Battleships.Ships
{
    public abstract record Ship
    {
        public abstract int Size { get; }

        public virtual int DefaultCount => 1;

        public string Name => GetType().Name;
    }
}
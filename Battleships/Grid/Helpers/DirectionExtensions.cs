using Battleships.Grid;

internal static class DirectionExtensions
{
    internal static int GetXCoefficient(this Direction dir)
    {
        return dir switch
        {
            Direction.Right => 1,
            Direction.Down => 0,
            _ => 0,
        };
    }

    internal static int GetYCoefficient(this Direction dir)
    {
        return dir switch
        {
            Direction.Right => 0,
            Direction.Down => 1,
            _ => 0,
        };
    }
}
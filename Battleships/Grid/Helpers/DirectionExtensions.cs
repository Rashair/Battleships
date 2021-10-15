using Battleships.Grid;

public static class DirectionExtensions
{
    public static int GetXCoefficient(this Direction dir)
    {
        return dir switch
        {
            Direction.Right => 1,
            Direction.Down => 0,
            _ => 0,
        };
    }

    public static int GetYCoefficient(this Direction dir)
    {
        return dir switch
        {
            Direction.Right => 0,
            Direction.Down => 1,
            _ => 0,
        };
    }

    public static Direction FromInt(int val)
    {
        return val == 0 ? Direction.Down : Direction.Right;
    }
}
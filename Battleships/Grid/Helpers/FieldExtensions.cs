namespace Battleships.Grid.Helpers
{
    public static class FieldExtensions
    {
        public static string ToFormattedString(this Field field)
        {
            return field switch
            {
                Field.Empty => " ",
                Field.ShotEmpty => "-",
                Field.Ship => "O",
                Field.ShotShip => "X",
                _ => "",
            };
        }

        public static bool WasShot(this Field field)
        {
            return field == Field.ShotEmpty || field == Field.ShotShip;
        }
    }
}

namespace Battleships.Grid.Helpers
{
    public static class FieldExtensions
    {
        public static string ToFormattedString(this Field field)
        {
            return field switch
            {
                Field.Empty => " ",
                Field.ShipUp => "O", 
                Field.ShipDown => "X",
                _ => "",
            };
        }
    }
}

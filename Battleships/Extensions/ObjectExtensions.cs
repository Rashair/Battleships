namespace Battleships.Extensions
{
    public static class ObjectExtensions
    {
        public static string PadRight(this object obj, int num)
        {
            if (obj == null)
            {
                return "";
            }

            return obj.ToString()!.PadRight(num);
        }
    }
}

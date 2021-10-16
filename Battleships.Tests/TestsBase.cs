namespace Battleships.Tests
{
    public class TestsBase
    {
        public const int DefaultTimeoutMs = 1000;

        protected static object[] GenerateTestCase(object obj)
        {
            return new object[] { obj };
        }
    }
}

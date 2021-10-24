using System;

namespace Battleships.Tests
{
    public class TestsBase
    {
        public const int DefaultTimeoutMs = 1000;

        protected readonly ServiceManager serviceManager;

        public TestsBase()
        {
            serviceManager = new ServiceManager();
        }

        protected static object[] GenerateTestCase(object obj)
        {
            return new object[] { obj };
        }
    }
}

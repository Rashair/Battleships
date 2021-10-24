using System;
using System.IO;
using System.Text;

namespace Battleships.IO
{
    public class IOManager : IIOManager
    {
        private readonly TextReader reader;
        private readonly TextWriter writer;

        public IOManager(TextReader? inputReader = null)
        {
            Console.OutputEncoding = Encoding.UTF8;
            reader = inputReader ?? Console.In;
            writer = Console.Out;
        }

        public bool GetBooleanInput(string question)
        {
            Write($"{question} (y/n): ");
            return ReadLine() == "y";
        }

        public int? GetIntegerInput(string msg)
        {
            Write($"{msg}: ");
            return ReadNumericInput();
        }

        public int? ReadNumericInput()
        {
            var input = ReadLine()!.Trim();
            return int.TryParse(input, out int result) ? result : null;
        }

        public string? ReadLine()
        {
            return reader.ReadLine();
        }

        public void Write(string msg)
        {
            writer.Write(msg);
        }

        public void WriteLine(string msg = "")
        {
            writer.WriteLine(msg);
        }
    }
}

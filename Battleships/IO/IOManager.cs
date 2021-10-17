using Battleships.Grid;
using Battleships.Grid.Helpers;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Battleships.Settings
{
    public class IOManager
    {
        private readonly TextReader reader;
        private readonly TextWriter writer;

        public IOManager(TextReader? inputReader = null)
        {
            Console.OutputEncoding = Encoding.UTF8;
            this.reader = inputReader ?? Console.In;
            this.writer = Console.Out;
        }

        public void WriteSettings(GameSettings gameSettings)
        {
            const int padRight = 8;
            WriteLine("The default settings are:");
            WriteLine($@"Board size: {gameSettings.BoardHeight}x{gameSettings.BoardHeight}

| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier         |  5   |      {gameSettings.CarriersNum}{"|",padRight}
| Battleship      |  4   |      {gameSettings.BattleshipsNum}{"|",padRight}
| Cruiser         |  3   |      {gameSettings.CruisersNum}{"|",padRight}
| Submarine       |  3   |      {gameSettings.SubmarinesNum}{"|",padRight}
| Destroyer       |  2   |      {gameSettings.DestroyersNum}{"|",padRight}
");
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
            this.writer.Write(msg);
        }

        public void WriteLine(string msg = "")
        {
            this.writer.WriteLine(msg);
        }

        public void WriteBoard(Board board)
        {
            WriteRow(2 * board.Width, "-");
            for (int y = 0; y < board.Height; ++y)
            {
                for (int x = 0; x < board.Width; ++x)
                {
                    Write(board[y, x].ToFormattedString());
                    Write("|");
                }
                WriteLine();
            }
            WriteRow(2 * board.Width, "-");
            WriteLine();
        }

        private void WriteRow(int width, string character)
        {
            for (int x = 0; x < width; ++x)
                Write(character);
            WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Battleships.Extensions;
using Battleships.Ships;

namespace Battleships.Settings
{
    public class GameSettings
    {
        private readonly Dictionary<Ship, int> shipsNum;

        internal GameSettings()
        {
            shipsNum = new Dictionary<Ship, int>();
            BoardHeight = 12;
            BoardWidth = 12;
        }

        public GameSettings(IEnumerable<Ship> shipTypes)
            : this()
        {
            foreach (var ship in shipTypes)
            {
                shipsNum.Add(ship, ship.DefaultCount);
            }
        }

        public IEnumerable<Ship> AllShips => shipsNum.Keys;
        public IReadOnlyDictionary<Ship, int> AllShipsWithCount => shipsNum;

        public int BoardHeight { get; set; }
        public int BoardWidth { get; set; }

        public override string ToString()
        {
            string typeHeader = "  Class of ship  ";
            string sizeHeader = "Size";
            string countHeader = "No. of ships";

            var builder = new StringBuilder($"Board size: {BoardHeight}x{BoardHeight}")
                .AppendLine()
                .AppendLine()
                .AppendLine($"| {typeHeader} | {sizeHeader} | {countHeader} |")
                .AppendLine($"| {LineSeparator(typeHeader.Length)} |" +
                    $" {LineSeparator(sizeHeader.Length)} |" +
                    $" {LineSeparator(countHeader.Length)} |");

            foreach (var entry in shipsNum)
            {
                var ship = entry.Key;
                builder.Append($"|  {ship.Name.PadRight(GetPad(typeHeader))} ")
                       .Append($"|  {ship.Size.PadRight(GetPad(sizeHeader))} ")
                       .AppendLine($"|  {entry.Value.PadRight(GetPad(countHeader))} |");
            }

            return builder.ToString();
        }

        private static string LineSeparator(int length)
        {
            return new string('-', length);
        }

        private static int GetPad(string header)
        {
            return header.Length - 1;
        }

        public void SetShipCount(Ship ship, int count)
        {
            shipsNum[ship] = count;
        }

        public GameSettings Add<T>(int count)
            where T : Ship
        {
            var instance = (Ship)Activator.CreateInstance(typeof(T))!;
            shipsNum.Add(instance, count);
            return this;
        }
    }
}
using System.Collections.Generic;
using Battleships.Settings;

namespace Battleships.Ships
{
    public static class ShipsGenerator
    {
        public static List<Ship> Generate(GameSettings gameSettings)
        {
            var ships = new List<Ship>();

            for (int i = 0; i < gameSettings.CarriersNum; ++i)
            {
                ships.Add(new Carrier());
            }

            for (int i = 0; i < gameSettings.BattleshipsNum; ++i)
            {
                ships.Add(new Battleship());
            }

            for (int i = 0; i < gameSettings.CruisersNum; ++i)
            {
                ships.Add(new Cruiser());
            }

            for (int i = 0; i < gameSettings.SubmarinesNum; ++i)
            {
                ships.Add(new Submarine());
            }

            for (int i = 0; i < gameSettings.DestroyersNum; ++i)
            {
                ships.Add(new Destroyer());
            }

            return ships;
        }
    }
}

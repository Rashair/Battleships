using System;
using System.Collections.Generic;
using Battleships.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.Ships
{
    public class ShipsGenerator
    {
        private readonly IServiceProvider serviceProvider;

        public ShipsGenerator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public List<Ship> Generate(GameSettings gameSettings)
        {
            var ships = new List<Ship>();
            foreach (var entry in gameSettings.AllShipsWithCount)
            {
                for (int i = 0; i < entry.Value; ++i)
                {
                    var ship = (Ship)serviceProvider.GetRequiredService(entry.Key.GetType());
                    ships.Add(ship);
                }
            }

            return ships;
        }
    }
}

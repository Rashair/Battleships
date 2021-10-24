using System.Collections.Generic;
using Battleships.IO;
using Battleships.Ships;

namespace Battleships.Settings
{
    public class SettingsManager
    {
        private readonly IIOManager ioManager;

        public GameSettings GameSettings { get; private set; }


        public SettingsManager(IIOManager ioManager,
            IEnumerable<Ship> shipTypes)
        {
            this.ioManager = ioManager;

            GameSettings = new();
            foreach (var ship in shipTypes)
            {
                GameSettings.AddShip(ship, ship.DefaultCount);
            }
        }

        public GameSettings InitalizeGameSettings()
        {
            ioManager.WriteLine("The default settings are:");
            ioManager.WriteLine(GameSettings.ToString());

            HandleSettingsModification();
            ioManager.WriteLine("The updated settings are:");
            ioManager.WriteLine(GameSettings.ToString());

            return GameSettings;
        }

        private void HandleSettingsModification()
        {
            var wantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify default settings?");
            if (!wantsToModifyDefaultSettings)
            {
                return;
            }

            int? input = ioManager.GetIntegerInput("Provide board height");
            if (input.HasValue)
            {
                GameSettings.BoardHeight = input.Value;
            }

            input = ioManager.GetIntegerInput("Provide board width");
            if (input.HasValue)
            {
                GameSettings.BoardWidth = input.Value;
            }

            var stillWantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify number of ships of each type?");
            if (!stillWantsToModifyDefaultSettings)
            {
                return;
            }

            var ships = GameSettings.AllShips;
            foreach (var ship in ships)
            {
                input = ioManager.GetIntegerInput($"Provide {ship.Name} no.");
                if (input.HasValue)
                {
                    GameSettings.SetShipCount(ship, input.Value);
                }
            }
            ioManager.WriteLine();
        }
    }
}

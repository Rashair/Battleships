using Battleships.IO;

namespace Battleships.Settings
{
    public class SettingsManager
    {
        private readonly IIOManager ioManager;
        private readonly GameSettings gameSettings;

        public SettingsManager(IIOManager ioManager,
            GameSettings gameSettings)
        {
            this.ioManager = ioManager;
            this.gameSettings = gameSettings;
        }

        public GameSettings InitalizeGameSettings()
        {
            ioManager.WriteLine("The default settings are:");
            ioManager.WriteLine(gameSettings.ToString());

            HandleSettingsModification();
            ioManager.WriteLine("The updated settings are:");
            ioManager.WriteLine(gameSettings.ToString());

            return gameSettings;
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
                gameSettings.BoardHeight = input.Value;
            }

            input = ioManager.GetIntegerInput("Provide board width");
            if (input.HasValue)
            {
                gameSettings.BoardWidth = input.Value;
            }

            var stillWantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify number of ships of each type?");
            if (!stillWantsToModifyDefaultSettings)
            {
                return;
            }

            var ships = gameSettings.AllShipsOrderedBySize;
            foreach (var ship in ships)
            {
                input = ioManager.GetIntegerInput($"Provide {ship.Name} no.");
                if (input.HasValue)
                {
                    gameSettings.SetShipCount(ship, input.Value);
                }
            }
            ioManager.WriteLine();
        }
    }
}

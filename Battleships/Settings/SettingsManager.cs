using Battleships.IO;

namespace Battleships.Settings
{
    public class SettingsManager
    {
        private readonly IIOManager ioManager;

        public GameSettings GameSettings { get; private set; }

        public SettingsManager(IIOManager ioManager,
            GameSettings? gameSettings = null)
        {
            this.ioManager = ioManager;
            GameSettings = gameSettings ?? new();
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
                GameSettings = GameSettings with { BoardHeight = input.Value };
            }

            input = ioManager.GetIntegerInput("Provide board width");
            if (input.HasValue)
            {
                GameSettings = GameSettings with { BoardWidth = input.Value };
            }

            var stillWantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify number of ships of each type?");
            if (!stillWantsToModifyDefaultSettings)
            {
                return;
            }

            input = ioManager.GetIntegerInput("Provide carriers no.");
            if (input.HasValue)
            {
                GameSettings = GameSettings with { CarriersNum = input.Value };
            }

            input = ioManager.GetIntegerInput("Provide battleships no.");
            if (input.HasValue)
            {
                GameSettings = GameSettings with { BattleshipsNum = input.Value };
            }

            input = ioManager.GetIntegerInput("Provide cruisers no.");
            if (input.HasValue)
            {
                GameSettings = GameSettings with { CruisersNum = input.Value };
            }

            input = ioManager.GetIntegerInput("Provide submarines no.");
            if (input.HasValue)
            {
                GameSettings = GameSettings with { SubmarinesNum = input.Value };
            }

            input = ioManager.GetIntegerInput("Provide destroyers no.");
            if (input.HasValue)
            {
                GameSettings = GameSettings with { DestroyersNum = input.Value };
            };
            ioManager.WriteLine();
        }


    }
}

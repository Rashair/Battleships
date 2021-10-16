using System.IO;

namespace Battleships.Settings
{
    public class SettingsManager
    {
        private readonly IOManager ioManager;

        private GameSettings gameSettings;

        public SettingsManager(IOManager? ioManager = null,
            GameSettings? gameSettings = null)
        {
            this.ioManager = ioManager ?? new IOManager();

            this.gameSettings = gameSettings ?? new();
        }

        public GameSettings Initalize()
        {
            ioManager.WriteSettings(gameSettings);

            return HandleSettingsModification();
        }

        private GameSettings HandleSettingsModification()
        {
            var wantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify default settings?");
            if (!wantsToModifyDefaultSettings)
                return gameSettings;

            int? input = ioManager.GetIntegerInput("Provide board height");
            if (input.HasValue)
                gameSettings = gameSettings with { BoardHeight = input.Value };

            input = ioManager.GetIntegerInput("Provide board width");
            if (input.HasValue)
                gameSettings = gameSettings with { BoardWidth = input.Value };

            var stillWantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify number of ships of each type?");
            if (!stillWantsToModifyDefaultSettings)
                return gameSettings;

            input = ioManager.GetIntegerInput("Provide carriers no.");
            if (input.HasValue)
                gameSettings = gameSettings with { CarriersNum = input.Value };

            input = ioManager.GetIntegerInput("Provide battleships no.");
            if (input.HasValue)
                gameSettings = gameSettings with { BattleshipsNum = input.Value };

            input = ioManager.GetIntegerInput("Provide cruisers no.");
            if (input.HasValue)
                gameSettings = gameSettings with { CruisersNum = input.Value };

            input = ioManager.GetIntegerInput("Provide submarines no.");
            if (input.HasValue)
                gameSettings = gameSettings with { SubmarinesNum = input.Value };

            input = ioManager.GetIntegerInput("Provide destroyers no.");
            if (input.HasValue)
                gameSettings = gameSettings with { DestroyersNum = input.Value }; ;

            return gameSettings;
        }
    }
}

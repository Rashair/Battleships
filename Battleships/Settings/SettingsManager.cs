using System;
using System.IO;
using Battleships.Grid;
using Battleships.Ships;

namespace Battleships.Settings
{
    public class SettingsManager
    {
        private readonly IOManager ioManager;

        private GameSettings gameSettings;

        public SettingsManager(IOManager ioManager,
            GameSettings? gameSettings = null)
        {
            this.ioManager = ioManager;
            this.gameSettings = gameSettings ?? new();
        }

        public (Board board1, Board board2) InitializeGame()
        {
            bool failedToInit = false;
            Board board1;
            Board board2;
            do
            {
                var gameSettings = InitalizeGameSettings();

                board1 = new Board(gameSettings.BoardHeight, gameSettings.BoardWidth);
                board2 = new Board(gameSettings.BoardHeight, gameSettings.BoardWidth);

                var ships = ShipsGenerator.Generate(gameSettings);

                bool shouldRetry = false;
                do
                {
                    try
                    {
                        board1.Init(ships);
                        board2.Init(ships);
                    }
                    catch (Exception ex)
                    {
                        failedToInit = true;
                        ioManager.WriteLine($"Failed to initialize ships: {Environment.NewLine}"
                            + ex);
                        shouldRetry = ioManager.GetBooleanInput("Do you want to retry init?");
                    }
                } while (shouldRetry);
            } while (failedToInit);

            return (board1, board2);
        }

        public GameSettings InitalizeGameSettings()
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

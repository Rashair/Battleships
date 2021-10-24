using System;
using Battleships.Grid;
using Battleships.IO;
using Battleships.Settings;
using Battleships.Ships;

namespace Battleships.Init
{
    public class GameInitializer
    {
        private readonly IIOManager ioManager;
        private readonly SettingsManager settingsManager;
        private readonly BoardFactory boardFactory;
        private readonly ShipsGenerator shipsGenerator;

        public GameInitializer(IIOManager ioManager,
            SettingsManager settingsManager,
            BoardFactory boardFactory,
            ShipsGenerator shipsGenerator)
        {
            this.ioManager = ioManager;
            this.settingsManager = settingsManager;
            this.boardFactory = boardFactory;
            this.shipsGenerator = shipsGenerator;
        }

        public (Board board1, Board board2) Initialize()
        {
            Board board1;
            Board board2;
            bool failedToInit;
            do
            {
                var gameSettings = settingsManager.InitalizeGameSettings();

                board1 = boardFactory.Create(gameSettings.BoardHeight, gameSettings.BoardWidth);
                board2 = boardFactory.Create(gameSettings.BoardHeight, gameSettings.BoardWidth);

                var ships = shipsGenerator.Generate(gameSettings);

                bool shouldRetry = false;
                do
                {
                    try
                    {
                        board1.Init(ships);
                        board2.Init(ships);
                        failedToInit = false;
                    }
                    catch (TimeoutException ex)
                    {
                        failedToInit = true;
                        ioManager.WriteLine($"Failed to initialize ships: " + ex.Message);
                        shouldRetry = ioManager.GetBooleanInput("Do you want to retry initialization?");
                    }
                    catch (Exception ex)
                    {
                        failedToInit = true;
                        shouldRetry = false;
                        ioManager.WriteLine($"Unexpected error when trying to initialize ships: {Environment.NewLine}"
                           + ex);
                    }
                } while (shouldRetry);
            } while (failedToInit);



            return (board1, board2);
        }
    }
}

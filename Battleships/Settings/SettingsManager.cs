using System;
using Battleships.Grid;
using Battleships.Ships;

namespace Battleships.Settings
{
    public class SettingsManager
    {
        private readonly IIOManager ioManager;
        private readonly Random random;

        public GameSettings GameSettings { get; private set; }

        public SettingsManager(IIOManager ioManager,
            GameSettings? gameSettings = null,
            Random? random = null)
        {
            this.ioManager = ioManager;
            GameSettings = gameSettings ?? new();
            this.random = random ?? new();
        }

        public (Board board1, Board board2) InitializeGame()
        {
            Board board1;
            Board board2;
            bool failedToInit;
            do
            {
                GameSettings = InitalizeGameSettings();

                board1 = CreateBoard(GameSettings.BoardHeight, GameSettings.BoardWidth);
                board2 = CreateBoard(GameSettings.BoardHeight, GameSettings.BoardWidth);

                var ships = ShipsGenerator.Generate(GameSettings);

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
                        shouldRetry = ioManager.GetBooleanInput("Do you want to retry init?");
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

        public GameSettings InitalizeGameSettings()
        {
            ioManager.WriteLine("The default settings are:");
            ioManager.WriteLine(GameSettings.ToString());

            return HandleSettingsModification();
        }

        private GameSettings HandleSettingsModification()
        {
            var wantsToModifyDefaultSettings = ioManager
                .GetBooleanInput("Do you want to modify default settings?");
            if (!wantsToModifyDefaultSettings)
            {
                return GameSettings;
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
                return GameSettings;
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

            return GameSettings;
        }

        private Board CreateBoard(int height, int width)
        {
            return new Board(height, width, new Random(random.Next()));
        }
    }
}

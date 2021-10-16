using Battleships;
using Battleships.Grid;
using Battleships.Settings;
using System;


var ioManager = new IOManager();

ioManager.WriteLine("Welcome in Battleships Game!");
ioManager.WriteLine("----------------------------");

var settingsManager = new SettingsManager(ioManager);

bool failedToInit = false;
Board boardA;
Board boardB;
do
{
    var gameSettings = settingsManager.Initalize();

    boardA = new Board(gameSettings.BoardHeight, gameSettings.BoardWidth);
    boardB = new Board(gameSettings.BoardHeight, gameSettings.BoardWidth);

    var ships = ShipsGenerator.Generate(gameSettings);

    bool shouldRetry = false;

    do
    {
        try
        {
            boardA.Init(ships);
            boardB.Init(ships);
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

ioManager.WriteLine("The boards are:");
ioManager.WriteBoard(boardA);
ioManager.WriteBoard(boardB);

ioManager.ReadLine();
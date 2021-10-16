using Battleships;
using Battleships.Grid;
using Battleships.Settings;
using System;


var ioManager = new IOManager();

ioManager.WriteLine("Welcome in Battleships Game!");
ioManager.WriteLine("----------------------------");

var settingsManager = new SettingsManager(ioManager);

bool failedToInit = false;
Board board1;
Board board2;
do
{
    var gameSettings = settingsManager.Initalize();

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

ioManager.WriteLine("The boards are:");
ioManager.WriteBoard(board1);
ioManager.WriteBoard(board2);

var judge = new Judge(board1, board2);
var player1 = new Player(board1);
var player2 = new Player(board2);
var game = new Game(ioManager, judge, player1, player2);

var shouldStartGame = ioManager.GetBooleanInput("Do you want to start the game?");
if (shouldStartGame)
{
    Player? winner;
    do
    {
        game.MakeATurn();
        ioManager.WriteBoard(board1);
        ioManager.WriteBoard(board2);
    } while (!game.IsGameFinished(out winner));

    if (winner == null)
    {
        ioManager.WriteLine("It's a draw!");
    }
    else if (winner == player1)
    {
        ioManager.WriteLine("Player1 won!!!");
    }
    else if (winner == player2)
    {
        ioManager.WriteLine("Player2 won!!!");
    }
}


ioManager.ReadLine();
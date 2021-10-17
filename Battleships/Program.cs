using Battleships.GameLogic;
using Battleships.Grid;
using Battleships.Settings;


var ioManager = new IOManager();
ioManager.WriteLine("Welcome in Battleships Game!");
ioManager.WriteLine("----------------------------");

var settingsManager = new SettingsManager(ioManager);
(Board board1, Board board2) = settingsManager.InitializeGame();

var judge = new Judge(board1, board2);
var player1 = new Player(ioManager, board1, "Player-1");
var player2 = new Player(ioManager, board2, "Player-2");
var game = new Game(ioManager, judge, player1, player2);

player1.DisplayState();
player2.DisplayState();

game.Start();

ioManager.WriteLine("Exiting...");
ioManager.ReadLine();
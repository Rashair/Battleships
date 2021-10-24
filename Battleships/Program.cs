using System.Runtime.CompilerServices;
using Battleships.DI;
using Battleships.GameLogic.Factories;
using Battleships.Grid;
using Battleships.Init;
using Battleships.IO;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Battleships.Tests")]


{
    var serviceManager = new ServiceManager();
    serviceManager.InitDefault();
    var serviceProvider = serviceManager.GetServiceProvider();

    var ioManager = serviceProvider.GetRequiredService<IIOManager>();
    ioManager.WriteLine("Welcome in Battleships Game!");
    ioManager.WriteLine("----------------------------");

    var gameInitializer = serviceProvider.GetService<GameInitializer>()!;
    (Board board1, Board board2) = gameInitializer.Initialize();

    var judgeFactory = serviceProvider.GetRequiredService<JudgeFactory>();
    var judge = judgeFactory.Create(board1, board2);

    var playerFactory = serviceProvider.GetRequiredService<PlayerFactory>();
    var player1 = playerFactory.Create(board1, "Player-1");
    var player2 = playerFactory.Create(board2, "Player-2");

    var gameFactory = serviceProvider.GetRequiredService<GameFactory>();
    var game = gameFactory.Create(judge, player1, player2);

    ioManager.WriteLine("Initial states of players:");
    player1.DisplayState();
    player2.DisplayState();

    game.Start();

    ioManager.WriteLine("Exiting...");
    ioManager.ReadLine();
}
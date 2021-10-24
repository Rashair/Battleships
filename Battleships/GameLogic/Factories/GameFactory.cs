using Battleships.GameLogic;
using Battleships.IO;

public class GameFactory
{
    private readonly IIOManager ioManager;

    public GameFactory(IIOManager ioManager)
    {
        this.ioManager = ioManager;
    }

    public Game Create(Judge judge, Player player1, Player player2)
    {
        return new Game(ioManager, judge, player1, player2);
    }
}
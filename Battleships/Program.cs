using Battleships;
using static System.Console;

WriteLine("Welcome in Battleships Game!");
WriteLine("----------------------------");

var gameSettings = new GameSettings();
WriteLine("The default settings are:");
const int padRight = 8;
WriteLine($@"Board size: {gameSettings.BoardHeight}x{gameSettings.BoardHeight}

| Class of ship   | Size | No. of ships |
| --------------- | ---- | ------------ |
| Carrier         |  5   |      {gameSettings.CarriersNum}{"|",padRight}
| Battleship      |  4   |      {gameSettings.BattleshipsNum}{"|",padRight}
| Cruiser         |  3   |      {gameSettings.CruisersNum}{"|",padRight}
| Submarine       |  3   |      {gameSettings.SubmarinesNum}{"|",padRight}
| Destroyer       |  2   |      {gameSettings.DestroyersNum}{"|",padRight}
");


gameSettings = HandleSettingsModification(gameSettings);


GameSettings HandleSettingsModification(GameSettings gameSettings)
{
    Write("Do you want to modify default settings? (y/n): ");
    var wantsToModifyDefaultSettings = ReadLine() == "y";
    if (!wantsToModifyDefaultSettings)
        return gameSettings;

    Write("Provide board height: ");
    int? input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { BoardHeight = input.Value };

    Write("Provide board width: ");
    input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { BoardWidth = input.Value };

    Write("Do you want to modify number of ships of each type? (y/n): ");
    var stillWantsToModifyDefaultSettings = ReadLine() == "y";
    if (!stillWantsToModifyDefaultSettings)
        return gameSettings;

    Write("Provide carriers no.: ");
    input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { CarriersNum = input.Value };

    Write("Provide battleships no.: ");
    input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { BattleshipsNum = input.Value };

    Write("Provide cruisers no.: ");
    input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { CruisersNum = input.Value };

    Write("Provide submarines no.: ");
    input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { SubmarinesNum = input.Value };

    Write("Provide destroyers no.: ");
    input = ReadNumericInput();
    if (input.HasValue)
        gameSettings = gameSettings with { DestroyersNum = input.Value }; ;

    return gameSettings;
}

int? ReadNumericInput()
{
    var input = ReadLine()!.Trim();
    return int.TryParse(input, out int result) ? result : null;
}


Read();
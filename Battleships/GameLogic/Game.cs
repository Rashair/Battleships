using Battleships.Settings;

namespace Battleships.GameLogic
{
    public class Game
    {
        private readonly IOManager ioManager;
        private readonly Judge judge;
        private readonly Player player1;
        private readonly Player player2;

        public Game(IOManager ioManager,
            Judge judge,
            Player player1,
            Player player2)
        {
            this.ioManager = ioManager;
            this.judge = judge;
            this.player1 = player1;
            this.player2 = player2;
        }

        public void Start()
        {
            var shouldStartGame = ioManager.GetBooleanInput("Do you want to start the game?");
            if (shouldStartGame)
            {
                var winner = Run();

                if (winner == null)
                {
                    ioManager.WriteLine("It's a draw!");
                }
                else
                {
                    ioManager.WriteLine($"{winner.Name} won!!!");
                }
            }
        }

        /// <summary>
        /// Main loop of the game, runs until game is finished.
        /// </summary>
        /// <returns>Winner of the game</returns>
        private Player? Run()
        {
            Player? winner;
            while (!IsGameFinished(out winner))
            {
                MakeATurn();
            }

            return winner;
        }

        private void MakeATurn()
        {
            player1.ShootField(judge);
            player2.ShootField(judge);

            player1.DisplayState();
            player2.DisplayState();
        }

        public bool IsGameFinished(out Player? winner)
        {
            var player1Won = judge.DidPlayerWin(player1.Token);
            var player2Won = judge.DidPlayerWin(player2.Token);
            var isGameFinished = player1Won || player2Won;

            winner = null;
            if (isGameFinished)
            {
                winner = GetWinner(player1Won, player2Won);
            }

            return isGameFinished;
        }

        private Player? GetWinner(bool player1Won, bool player2Won)
        {
            if (player1Won && !player2Won)
            {
                return player1;
            }
            else if (player2Won && !player1Won)
            {
                return player2;
            }

            // Winner is null when we have draw.
            return null;
        }
    }
}

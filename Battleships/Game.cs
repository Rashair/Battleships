using Battleships.Grid;
using Battleships.Settings;
using System;

namespace Battleships
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Winner of the game</returns>
        public Player? Run()
        {
            Player? winner;
            while (!IsGameFinished(out winner))
            {
                MakeATurn();
            }

            return winner;
        }

        public void MakeATurn()
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
                winner = GetWinner(player1Won, player2Won);

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

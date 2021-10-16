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

        public void MakeATurn()
        {
            player1.ShootBoardField(judge);

            player2.ShootBoardField(judge);
        }

        public bool IsGameFinished(out Player? winner)
        {
            var player1Won = judge.DidPlayerWin(player1.Token);
            var player2Won = judge.DidPlayerWin(player2.Token);
            var isGameFinished = player1Won || player2Won;

            if (player1Won && !player2Won)
            {
                winner = player1;

            }
            else if (player2Won && !player1Won)
            {
                winner = player2;
            }
            else
            {
                // Draw or game not finished
                winner = null;
            }

            return isGameFinished;
        }
    }
}

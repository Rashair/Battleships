using System;
using Battleships.Grid;
using Battleships.Settings;

namespace Battleships.GameLogic
{
    public class Player
    {
        private readonly IIOManager ioManager;
        private readonly Random random;
        private readonly bool[][] shootingBoard;

        public string Name { get; }
        public Guid Token { get; }
        public Board Board { get; }

        public Player(IIOManager ioManager, Board board, string name, Random? random = null)
        {
            this.ioManager = ioManager;
            Board = board;
            Name = name;
            Token = Guid.NewGuid();

            board.InitPlayerToken(Token);

            this.random = random ?? new();
            shootingBoard = new bool[Board.Height][];
            for (int i = 0; i < Board.Height; ++i)
            {
                shootingBoard[i] = new bool[Board.Width];
            }
        }

        public bool ShootField(Judge judge)
        {
            int y = random.Next(Board.Height);
            int x = random.Next(Board.Width);

            int initY = y;
            int initX = x;
            while (shootingBoard[y][x])
            {
                (y, x) = GetNextFieldWithinBoard(y, x);

                if (x == initX && y == initY)
                {
                    throw new InvalidOperationException("All fields were already shot");
                }
            }

            return ShootField(judge, y, x);
        }

        private bool ShootField(Judge judge, int y, int x)
        {
            var wasShotSuccessful = judge.Shoot(Token, y, x);
            // We always want to set shooting board after judge.Shoot in case something happens.
            // In that case we wouldn't want Player to think that they already had shot a field that they actually didn't. 
            shootingBoard[y][x] = true;
            return wasShotSuccessful;
        }

        private (int y, int x) GetNextFieldWithinBoard(int y, int x)
        {
            ++x;
            if (x == Board.Width)
            {
                x = 0;
                ++y;
                if (y == Board.Height)
                {
                    y = 0;
                }
            }

            return (y, x);
        }

        public void DisplayState()
        {
            ioManager.WriteLine($"{Name}: ");
            ioManager.WriteLine(Board.ToString()!);
        }
    }
}

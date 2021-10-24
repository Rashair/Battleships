using Battleships.Grid;

namespace Battleships.GameLogic.Factories
{
    public class JudgeFactory
    {
        public Judge Create(Board board1, Board board2)
        {
            return new Judge(board1, board2);
        }
    }
}

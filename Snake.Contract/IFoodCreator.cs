using Snake.Contract.Models;

namespace Snake.Contract
{
    public interface IFoodCreator
    {
        Food CreateFoodPiece(int maxXPosition, int maxYPosition);
    }
}

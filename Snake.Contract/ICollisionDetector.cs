using Snake.Contract.Models;
using System.Collections.Generic;

namespace Snake.Contract
{
    public interface ICollisionDetector
    {
        bool SnakeBorderCollisionHappened(IEnumerable<SnakePart> snake, int borderHeight, int borderWidth);

        bool SnakeHitsHimselfCollisionHappened(IEnumerable<SnakePart> snake);

        bool SnakeFoodCollisionHappened(IEnumerable<SnakePart> snake, Food food);
    }
}

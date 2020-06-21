using Snake.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Contract
{
    public interface ICollisionDetector
    {
        bool SnakeBorderCollisionHappened(IEnumerable<SnakePart> snake, int borderHeight, int borderWidth, int objectSizeX, int objectSizeY);

        bool SnakeHitsHimselfCollisionHappened(IEnumerable<SnakePart> snake);

        bool SnakeFoodCollisionHappened(IEnumerable<SnakePart> snake, Food food);
    }
}

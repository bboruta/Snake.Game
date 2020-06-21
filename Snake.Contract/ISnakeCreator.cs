using Snake.Contract.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Contract
{
    public interface ISnakeCreator
    {
        IEnumerable<SnakePart> CreateHeadOnlySnake();

        void GrowSnake(IEnumerable<SnakePart> snake);

        void KillSnake(IEnumerable<SnakePart> snake);
    }
}

﻿using Snake.Contract;
using Snake.Contract.Models;
using System.Collections.Generic;
using System.Linq;

namespace Snake.Domain
{
    public class CollisionDetector : ICollisionDetector
    {
        public bool SnakeBorderCollisionHappened(IEnumerable<SnakePart> snake, int borderHeight, int borderWidth)
        {
            if (snake.First().X < 0 || snake.First().Y < 0
                || snake.First().X >= borderHeight
                || snake.First().Y >= (borderWidth))
            {
                return true;
            }

            return false;
        }

        public bool SnakeFoodCollisionHappened(IEnumerable<SnakePart> snake, Food food)
        {
            if (snake.First().X == food.X &&
                snake.First().Y == food.Y)
            {
                return true;
            }

            return false;
        }

        public bool SnakeHitsHimselfCollisionHappened(IEnumerable<SnakePart> snake)
        {
            for (int j = 1; j < snake.Count(); j++)
            {
                if (snake.First().X == snake.ElementAt(j).X &&
                   snake.First().Y == snake.ElementAt(j).Y)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

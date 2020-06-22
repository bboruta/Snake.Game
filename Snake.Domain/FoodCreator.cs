using Snake.Contract;
using Snake.Contract.Models;
using System;

namespace Snake.Domain
{
    public class FoodCreator : IFoodCreator
    {
        private Random _random = new Random();

        public Food CreateFoodPiece(int maxXPosition, int maxYPosition)
        {
            var x = _random.Next(0, maxXPosition);
            var y = _random.Next(0, maxYPosition);

            return new Food(x, y);
        }
    }
}

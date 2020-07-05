using NUnit.Framework;
using Snake.Contract.Models;
using Snake.Domain;
using System.Collections.Generic;

namespace Snake.Tests.Unit
{
    [TestFixture]
    public class CollistionDetectorTest
    {
        private CollisionDetector _uut;
        private const int borderHeight = 100;
        private const int borderWidth = 100;

        private const int foodXPosition = 50;
        private const int foodYPosition = 50;

        [SetUp]
        protected void SetUp()
        {
            _uut = new CollisionDetector();
        }

        [TestCase(borderHeight, 0)]
        [TestCase(0, borderWidth)]
        [TestCase(borderHeight, borderWidth)]
        [TestCase(borderHeight + 1, 0)]
        [TestCase(borderHeight * 2, 0)]
        [TestCase(0, borderWidth + 1)]
        [TestCase(0, borderWidth * 2)]
        public void WhenSnakeHeadReachesBorderThenCollisionShouldHappen(int snakeHeadXPosition, int snakeHeadYPosition)
        {
            // arrange
            // first element is a head
            IEnumerable<SnakePart> snake = new List<SnakePart>()
            {
                { new SnakePart(snakeHeadXPosition, snakeHeadYPosition) }
            };

            // act
            var snakeHitsGameBorder = _uut.SnakeBorderCollisionHappened(snake, borderHeight, borderWidth);

            // assert
            Assert.IsTrue(snakeHitsGameBorder);
        }

        [TestCase(0, 0)]
        [TestCase(borderHeight - 1, borderWidth - 1)]
        [TestCase(borderHeight / 2, borderWidth / 2)]
        public void WhenSnakeHeadDoesNotReachBorderThenNoCollisionShouldHappen(int snakeHeadXPosition, int snakeHeadYPosition)
        {
            // arrange
            // first element is a head
            IEnumerable<SnakePart> snake = new List<SnakePart>()
            {
                { new SnakePart(snakeHeadXPosition, snakeHeadYPosition) }
            };

            // act
            var snakeHitsGameBorder = _uut.SnakeBorderCollisionHappened(snake, borderHeight, borderWidth);

            // assert
            Assert.IsFalse(snakeHitsGameBorder);
        }

        [TestCase(50, 50, foodXPosition, foodYPosition)]
        public void WhenSnakeHeadReachesFoodThenCollisionShouldHappen(
            int snakeHeadXPosition, int snakeHeadYPosition,
            int foodXPosition, int foodYPosition)
        {
            // arrange
            // first element is a head
            IEnumerable<SnakePart> snake = new List<SnakePart>()
            {
                { new SnakePart(snakeHeadXPosition, snakeHeadYPosition) }
            };

            Food food = new Food(foodXPosition, foodYPosition);

            // act
            var snakeHitsFood = _uut.SnakeFoodCollisionHappened(snake, food);

            // assert
            Assert.IsTrue(snakeHitsFood);
        }

        [TestCase(0, 0, foodXPosition, foodYPosition)]
        [TestCase(100, 100, foodXPosition, foodYPosition)]
        [TestCase(49, 50, foodXPosition, foodYPosition)]
        [TestCase(50, 49, foodXPosition, foodYPosition)]
        public void WhenSnakeDoesNotHeadReachFoodThenNoCollisionShouldHappen(
            int snakeHeadXPosition, int snakeHeadYPosition,
            int foodXPosition, int foodYPosition)
        {
            // arrange
            // first element is a head
            IEnumerable<SnakePart> snake = new List<SnakePart>()
            {
                { new SnakePart(snakeHeadXPosition, snakeHeadYPosition) }
            };

            Food food = new Food(foodXPosition, foodYPosition);

            // act
            var snakeHitsFood = _uut.SnakeFoodCollisionHappened(snake, food);

            // assert
            Assert.IsFalse(snakeHitsFood);
        }

        [Test]
        public void WhenSnakeHeadHitsOwnBodyThenCollisionShouldHappen()
        {
            // arrange
            // first element is a head
            IEnumerable<SnakePart> snake = new List<SnakePart>()
            {
                { new SnakePart(50, 50) },
                { new SnakePart(50, 51) },
                { new SnakePart(50, 52) },
                { new SnakePart(50, 53) },
                { new SnakePart(50, 54) },
                { new SnakePart(50, 50) },
            };

            // act
            var snakeHitsItself = _uut.SnakeHitsHimselfCollisionHappened(snake);

            // assert
            Assert.IsTrue(snakeHitsItself);
        }

        [Test]
        public void WhenSnakeHeadDoNotTouchBodyThenNoCollisionShouldHappen()
        {
            // arrange
            // first element is a head
            IEnumerable<SnakePart> snake = new List<SnakePart>()
            {
                { new SnakePart(50, 50) },
                { new SnakePart(50, 51) },
                { new SnakePart(50, 52) },
                { new SnakePart(50, 53) },
                { new SnakePart(50, 54) },
            };

            // act
            var snakeHitsItself = _uut.SnakeHitsHimselfCollisionHappened(snake);

            // assert
            Assert.IsFalse(snakeHitsItself);
        }
    }
}

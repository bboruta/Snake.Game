using NUnit.Framework;
using Snake.Contract.Enums;
using Snake.Domain;

namespace Snake.Tests.Unit
{
    [TestFixture]
    public class DirectionChangeDetectorTests
    {
        private DirectionChangeDetector _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new DirectionChangeDetector();
        }

        [TestCase(SnakeDirection.Up, SnakeDirection.Down)]
        [TestCase(SnakeDirection.Down, SnakeDirection.Up)]
        [TestCase(SnakeDirection.Right, SnakeDirection.Left)]
        [TestCase(SnakeDirection.Left, SnakeDirection.Right)]
        public void NoDirectionChangeIfNewDirectionIsOpposite(SnakeDirection current, SnakeDirection opposite)
        {
            // act
            var directionAfter = _uut.GetNewDirectionIfChanged(current, opposite);

            // assert
            Assert.AreEqual(current, directionAfter);
        }

        [TestCase(SnakeDirection.Up, SnakeDirection.Up)]
        [TestCase(SnakeDirection.Down, SnakeDirection.Down)]
        [TestCase(SnakeDirection.Right, SnakeDirection.Right)]
        [TestCase(SnakeDirection.Left, SnakeDirection.Left)]
        public void NoDirectionChangeIfDirectionTheSame(SnakeDirection current, SnakeDirection nextButSame)
        {
            // act
            var directionAfter = _uut.GetNewDirectionIfChanged(current, nextButSame);

            // assert
            Assert.AreEqual(current, directionAfter);
        }

        [TestCase(SnakeDirection.Up, SnakeDirection.Left)]
        [TestCase(SnakeDirection.Up, SnakeDirection.Right)]
        [TestCase(SnakeDirection.Down, SnakeDirection.Left)]
        [TestCase(SnakeDirection.Down, SnakeDirection.Right)]
        [TestCase(SnakeDirection.Right, SnakeDirection.Up)]
        [TestCase(SnakeDirection.Right, SnakeDirection.Down)]
        [TestCase(SnakeDirection.Left, SnakeDirection.Up)]
        [TestCase(SnakeDirection.Left, SnakeDirection.Down)]
        public void DirectionChangesIfDoneToProperDirection(SnakeDirection current, SnakeDirection next)
        {
            // act
            var directionAfter = _uut.GetNewDirectionIfChanged(current, next);

            // assert
            Assert.AreEqual(next, directionAfter);
        }
    }
}

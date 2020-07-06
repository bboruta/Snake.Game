using Snake.Contract;
using Snake.Contract.Enums;
using System.Collections.Generic;

namespace Snake.Domain
{
    public class DirectionChangeDetector : IDirectionChangeDetector
    {
        private Dictionary<SnakeDirection, SnakeDirection> _oppositeDirections;
        public DirectionChangeDetector()
        {
            _oppositeDirections = new Dictionary<SnakeDirection, SnakeDirection>()
            {
                {SnakeDirection.Up, SnakeDirection.Down },
                {SnakeDirection.Down, SnakeDirection.Up },
                {SnakeDirection.Left, SnakeDirection.Right },
                {SnakeDirection.Right, SnakeDirection.Left }
            };
        }

        public SnakeDirection GetNewDirectionIfChanged(SnakeDirection currentDirection, SnakeDirection nextPotentialDirection)
        {            
            if (_oppositeDirections[currentDirection] != nextPotentialDirection)
            {
                return nextPotentialDirection;
            }

            return currentDirection;
        }
    }
}

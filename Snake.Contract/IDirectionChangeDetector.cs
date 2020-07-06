using Snake.Contract.Enums;

namespace Snake.Contract
{
    public interface IDirectionChangeDetector
    {
        SnakeDirection GetNewDirectionIfChanged(SnakeDirection currentDirection, SnakeDirection nextPotentialDirection);
    }
}

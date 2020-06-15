using System;

namespace Snake.Contract
{
    public class SnakePart
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SnakePart()
        {
            X = 0;
            Y = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Contract
{
    public class Food
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Food()
        {
            X = 0;
            Y = 0;
        }
    }
}

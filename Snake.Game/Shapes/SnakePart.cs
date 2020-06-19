using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Snake.Game.Shapes
{
    public class SnakePart : ShapeBase
    {
        public SnakePart(int x, int y, int xSize, int ySize, Brush brush) : base(x, y, xSize, ySize, brush)
        {
        }
    }
}

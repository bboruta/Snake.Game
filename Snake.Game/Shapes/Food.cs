using System.Windows;
using System.Windows.Media;

namespace Snake.Game.Shapes
{
    public class Food : ShapeBase
    {
        public Food(int x, int y, int xSize, int ySize, Brush brush) : base(x, y, xSize, ySize, brush)
        {
        }
    }
}

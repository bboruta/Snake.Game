using System.Windows;
using System.Windows.Media;

namespace Snake.Game.Shapes
{
    public abstract class ShapeBase : ViewModelBase
    {
        private int _x;
        private int _y;
        private int _xSize;
        private int _ySize;

        public int X
        {
            get => _x;
            set
            {
                SetProperty(ref _x, value);
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                SetProperty(ref _y, value);
            }
        }

        public int XLogicalPosition
        {
            get => _x / _xSize;
        }

        public int YLogicalPosition
        {
            get => _y / _ySize; 
        }

        public Geometry Geometry { get; set; }
        public Brush Fill { get; set; }

        public ShapeBase(int x, int y, int xSize, int ySize, Brush brush)
        {
            X = x;
            Y = y;
            var rectangle = new Rect(0, 0, xSize, ySize);
            var rectangleGeometry = new RectangleGeometry(rectangle);

            Geometry = rectangleGeometry;
            Fill = brush;

            _xSize = xSize;
            _ySize = ySize;
        }
    }
}

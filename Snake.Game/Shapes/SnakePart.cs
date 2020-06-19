using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Snake.Game.Shapes
{
    public class SnakePart : ViewModelBase
    {
        private int _x;          

        private int _y;

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
        public Geometry Geometry { get; set; }
        public Brush Fill { get; set; }

        public SnakePart()
        {
            X = 0;
            Y = 0;
            Geometry = new EllipseGeometry { RadiusX = 50, RadiusY = 50 };
            Fill = Brushes.Red;
        }

        public SnakePart(int x, int y, int xSize, int ySize, Brush brush)
        {
            X = x;
            Y = y;
            var rectangle = new Rect(0, 0, xSize, ySize);

            var rectangleGeometry = new RectangleGeometry(rectangle);
            //var rectangleGeometry = Geometry = new EllipseGeometry { RadiusX = 10, RadiusY = 10 };
            //rectangleGeometry.

            Geometry = rectangleGeometry;
            Fill = brush;
        }
    }
}

using Snake.Contract.Models;
using Snake.Game.Shapes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace Snake.Game.ExtensionMethods
{
    public static class Mapper
    {
        public static IEnumerable<SnakePart> MapToSnakePartCollection(this IEnumerable<SnakePartShape> snakeParts)
        {
            var convertedList = snakeParts.Select(part => new SnakePart(part.XLogicalPosition, part.YLogicalPosition)).ToList();
            var collection = new ObservableCollection<SnakePart>(convertedList);

            return collection;
        }

        public static IEnumerable<SnakePartShape> MapToSnakePartShapeCollection(
            this IEnumerable<SnakePart> snakeParts,
            int xSize,
            int ySize,
            Brush headBrush,
            Brush bodyBrush)
        {
            var convertedList = snakeParts.Select(part => new SnakePartShape(part.X, part.Y, xSize, ySize, bodyBrush)).ToList();
            convertedList.First().Fill = headBrush;
            var collection = new ObservableCollection<SnakePartShape>(convertedList);

            return collection;
        }

        public static Food MapToFood(this FoodShape foodShape)
        {
            return new Food(foodShape.XLogicalPosition, foodShape.YLogicalPosition);
        }
    }
}

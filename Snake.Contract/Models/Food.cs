namespace Snake.Contract.Models
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

        public Food(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

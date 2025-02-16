namespace L3ave.Entities
{
    public class Point
    {
        public double X;

        public double Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point()
        {
            X = 0.0;
            Y = 0.0;
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }

        public Point Offset(double x, double y)
        {
            return new Point(X + x, Y + y);
        }

        public Point Offset(Point delta)
        {
            return Offset(delta.X, delta.Y);
        }
    }
}

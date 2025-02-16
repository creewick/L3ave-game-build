using System;

namespace L3ave.Entities
{
    public class Rect
    {
        public double X;

        public double Y;

        public double Width;

        public double Height;

        public double Left => X;

        public double Top => Y;

        public double Right => X + Width;

        public double Bottom => Y + Height;

        public Rect(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rect(Point p1, Point p2)
        {
            X = Math.Min(p1.X, p2.X);
            Y = Math.Min(p1.Y, p2.Y);
            Width = Math.Abs(p1.X - p2.X);
            Height = Math.Abs(p1.Y - p2.Y);
        }

        public bool HasPoint(Point point)
        {
            return point.X >= Left
                && point.X <= Right
                && point.Y >= Top
                && point.Y <= Bottom;
        }
    }
}

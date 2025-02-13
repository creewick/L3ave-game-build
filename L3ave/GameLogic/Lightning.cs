using L3ave.Models;
using L3ave.Drawing;

namespace L3ave.GameLogic
{
    public class Lightning
    {
        private static int StepSize = 20;

        private int count;

        private Cell[,] field;

        private bool[,] visited;

        private Queue<Point> queue;

        public Lightning(Cell[,] field)
        {
            queue = new Queue<Point>();
            this.field = field;
        }

        public void Begin(Point position)
        {
            MusicPlayer.Light();

            visited = new bool[field.GetLength(0), field.GetLength(1)];
            count = 0;

            queue.Clear();
            queue.Enqueue(position);
        }

        public void Step()
        {
            while (queue.Count > 0 && count++ < StepSize)
            {
                var position = queue.Dequeue();

                foreach (var neighbour in GetNeighbours(position))
                {
                    var num = (int)neighbour.X;
                    var num2 = (int)neighbour.Y;

                    var cell = field[num, num2];

                    if (!visited[num, num2])
                    {
                        queue.Enqueue(neighbour);
                        visited[num, num2] = true;

                        cell.Opacity = Game.TouchOpacity;
                    }
                }
            }

            count = 0;
        }

        private IEnumerable<Point> GetNeighbours(Point position)
        {
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    if (position.X + (double)x >= 0.0
                        && position.X + (double)x < (double)field.GetLength(0)
                        && position.Y + (double)y >= 0.0
                        && position.Y + (double)y < (double)field.GetLength(1)
                        && (x != 0 || y != 0))
                    {
                        yield return position.Offset(x, y);
                    }
                }
            }
        }
    }
}

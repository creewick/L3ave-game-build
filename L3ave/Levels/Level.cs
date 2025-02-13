using System;
using System.Collections.Generic;

using L3ave.Models;

namespace L3ave.Levels
{
    public class Level
    {
        public List<Event> Events;

        public Point Position;

        public Cell[,] Field;

        public int LightLeft;

        public int Width => Field.GetLength(0);

        public int Height => Field.GetLength(1);

        public Level(Cell[,] field, Point position, int lightCount, List<Event> events = null)
        {
            Field = field;
            Position = position;
            Events = events ?? new List<Event>();
            LightLeft = lightCount;
        }
    }
}

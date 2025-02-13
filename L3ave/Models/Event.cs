using System;

using L3ave.GameLogic;

namespace L3ave.Models
{
    public class Event
    {
        public Rect Area;

        public Action<Game> Act;

        public Event(Rect area, Action<Game> action)
        {
            Area = area;
            Act = action;
        }
    }
}

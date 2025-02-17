using System;

using L3ave.Model;

namespace L3ave.Entity
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

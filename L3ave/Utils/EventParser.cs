using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using L3ave.Models;
using L3ave.GameLogic;

namespace L3ave.Utils
{
    public static class EventParser
    {
        private static Regex EventModel = new Regex("(\\S+) (\\S+) (\\S+) (\\S+) (.*)");

        public static Event Parse(string line)
        {
            var groups = EventModel.Match(line).Groups;

            var rectangle = new Rect(ExtractPoint(groups[1].Value), ExtractPoint(groups[2].Value));
            var triggers = ExtractTriggers(groups[3].Value.Split([';']));
            var value = groups[5].Value;

            return groups[4].Value switch
            {
                "text" => ParseMessage(rectangle, triggers, value.Replace("\\n", "\n")), 
                "playertext" => ParseText(rectangle, triggers, value.Replace("\\n", "\n")), 
                "level" => ParseLevel(rectangle, triggers, value), 
                "map" => ParseMap(rectangle, triggers, value), 
                "trigger" => ParseTrigger(rectangle, triggers, value), 
                "exit" => ParseExit(rectangle, triggers, value), 
                _ => throw new Exception(line), 
            };
        }

        private static List<TriggerPair> ExtractTriggers(string[] data)
        {
            var list = new List<TriggerPair>();

            foreach (var text in data)
            {
                var flag = text[0] != '!';
                var name = text.Substring((!flag) ? 1 : 0);

                list.Add(new TriggerPair(name, flag));
            }

            return list;
        }

        private static Point ExtractPoint(string data, char splitter = ',')
        {
            var array = data.Split([splitter]);

            return new Point(int.Parse(array[0]), int.Parse(array[1]));
        }

        private static Event ParseExit(Rect rectangle, List<TriggerPair> triggers, string data)
        {
            return new Event(rectangle, delegate(Game game)
            {
                if (game.Triggers.CheckAll(triggers))
                {
                    game.Exited = true;
                }
            });
        }

        private static Event ParseMessage(Rect rectangle, List<TriggerPair> triggers, string data)
        {
            return new Event(rectangle, delegate(Game game)
            {
                if (game.Triggers.CheckAll(triggers))
                {
                    game.Message = data;
                }
            });
        }

        private static Event ParseText(Rect rectangle, List<TriggerPair> triggers, string data)
        {
            return new Event(rectangle, delegate(Game game)
            {
                if (game.Triggers.CheckAll(triggers))
                {
                    game.Text = data;
                }
            });
        }

        private static Event ParseLevel(Rect rectangle, List<TriggerPair> triggers, string data)
        {
            var array = data.Split(['=']);
            var point = (array.Length > 1) ? ExtractPoint(array[1]) : null;

            var levelNumber = int.Parse(array[0]);

            return new Event(rectangle, delegate(Game game)
            {
                if (game.Triggers.CheckAll(triggers))
                {
                    game.ChangeLevel(levelNumber, point);
                }
            });
        }

        private static Event ParseMap(Rect rectangle, List<TriggerPair> triggers, string data)
        {
            var cellsToChange = new List<Tuple<int, int, CellType>>();
            var array = data.Split([';']);

            foreach (var text in array)
            {
                var array2 = text.Split(['=']);
                var array3 = array2[0].Split([',']);

                var item = EnumParser.Parse<CellType>(array2[1]);

                cellsToChange.Add(Tuple.Create(int.Parse(array3[0]), int.Parse(array3[1]), item));
            }

            return new Event(rectangle, delegate(Game game)
            {
                if (game.Triggers.CheckAll(triggers))
                {
                    foreach (var item2 in cellsToChange)
                    {
                        game.Level.Field[item2.Item1, item2.Item2] = new Cell(item2.Item3);
                    }
                }
            });
        }

        private static Event ParseTrigger(Rect rectangle, List<TriggerPair> triggers, string data)
        {
            var array = data.Split(['=']);

            var name = array[0];
            var value = bool.Parse(array[1]);

            return new Event(rectangle, delegate(Game game)
            {
                if (game.Triggers.CheckAll(triggers))
                {
                    game.Triggers[name, true] = value;
                }
            });
        }
    }
}

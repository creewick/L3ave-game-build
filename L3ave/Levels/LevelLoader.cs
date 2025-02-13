using System;
using System.Collections.Generic;
using System.Reflection;

using L3ave.Utils;
using L3ave.Models;

namespace L3ave.Levels
{
    public static class LevelLoader
    {
        private static char PlayerChar = 'X';

        private static Dictionary<char, CellType> CellTypes = new Dictionary<char, CellType>
        {
            {
                'W',
                CellType.Wall
            },
            {
                '#',
                CellType.Danger
            },
            {
                '-',
                CellType.Empty
            }
        };

        public static Level LoadFromFile(string filename)
        {
            var manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);

            using (var streamReader = new StreamReader(manifestResourceStream)) {
                var content = streamReader.ReadToEnd();

                return Parse(content);
            }
        }

        public static Level Parse(string content)
        {
            content = content.Replace("\r\n", "\n");

            var array = content.Split(['\n']);
            var array2 = array[0].Split([' ']);

            Point position = null;

            var lightCount = int.Parse(array2[2]);

            var array3 = new Cell[int.Parse(array2[0]), int.Parse(array2[1])];

            for (var i = 1; i < array3.GetLength(1) + 1; i++)
            {
                for (var j = 0; j < array3.GetLength(0); j++)
                {
                    if (array[i][j] == PlayerChar)
                    {
                        position = new Point(j, i);
                        array3[j, i - 1] = new Cell(CellType.Empty);
                    }
                    else
                    {
                        array3[j, i - 1] = new Cell(CellTypes[array[i][j]]);
                    }
                }
            }

            var array4 = new string[array.Length - array3.GetLength(1) - 1];

            Array.Copy(array, array3.GetLength(1) + 1, array4, 0, array4.Length);

            return new Level(array3, position, lightCount, ParseEvents(array4));
        }

        private static List<Event> ParseEvents(string[] lines)
        {
            var list = new List<Event>();

            foreach (string line in lines)
            {
                list.Add(EventParser.Parse(line));
            }

            return list;
        }
    }
}

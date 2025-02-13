namespace L3ave.Models
{
    public enum CellType
    {
        Empty,
        Wall,
        Danger
    }

    public class Cell
    {
        public CellType Type;

        public float Opacity;

        public Cell(CellType type, float opacity = 0f)
        {
            Type = type;
            Opacity = opacity;
        }
    }
}

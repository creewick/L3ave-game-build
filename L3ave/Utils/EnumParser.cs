namespace L3ave.Utils
{
    public static class EnumParser
    {
        public static T Parse<T>(string name)
        {
            return (T)Enum.Parse(typeof(T), name, ignoreCase: true);
        }
    }
}

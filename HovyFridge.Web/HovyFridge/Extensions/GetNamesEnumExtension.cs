namespace HovyFridge.Extensions
{
    public static class GetNamesEnumExtension
    {
        public static string? GetName<T>(this Enum value) where T : Enum
        {
            return Enum.GetName(typeof(T), value);
        }

        public static string[] GetNames<T>(this Enum value) where T : Enum
        {
            return Enum.GetNames(typeof(T));
        }
    }
}
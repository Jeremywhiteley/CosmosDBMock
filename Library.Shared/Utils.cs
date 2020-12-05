namespace Library.Shared
{
    public static class Utils
    {
        public static string GetValue<T>(T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, default).ToString();
        }

        public static void SetValue<T>(T item, string propertyName, string value)
        {
            item.GetType().GetProperty(propertyName).SetValue(item, value, default);
        }

        public static void SetValue<T>(T item, string propertyName, int value)
        {
            item.GetType().GetProperty(propertyName).SetValue(item, value, default);
        }
    }
}

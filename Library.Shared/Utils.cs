using System.Globalization;

namespace Library.Shared
{
    public static class Utils
    {
        // for string
        public static string GetValue<T>(T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, default)?.ToString();
        }
        // for string
        public static void SetValue<T>(T item, string propertyName, string value)
        {
            item.GetType().GetProperty(propertyName).SetValue(item, value, default);
        }

        // util as the default partition
        static string _country;
        public static string COUNTRYID {
            get {
                if (_country == null) {
                    _country = new RegionInfo(CultureInfo.CurrentCulture.LCID).Name;
                }
                return _country;
            }
            // NOTE. About Partitions
            // https://azure.microsoft.com/en-us/resources/videos/azure-documentdb-elastic-scale-partitioning/
        }

        public const string DEFAULT_PARTITION = "COUNTRYID";
    }
}

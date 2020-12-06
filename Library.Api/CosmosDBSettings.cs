// ******************************
// Axis Project
// @__harveyt__
// ******************************
namespace LibraryApi
{
    /// <summary>
    /// Clound account data
    /// </summary>
    public class CosmosDBSettings
    {
        public string EndPoint { get; set; }
        public string Key { get; set; }
        public string DatabaseId { get; set; }
        // default
        public string PartitionName { get; set; }
    }
}


namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// The data format of a batch geocode file.
    /// </summary>
    public enum BatchFileFormat
    {
        /// <summary>
        /// Comma seperated (comma delimited) file format.
        /// </summary>
        CSV,

        /// <summary>
        /// Pipe (|) delimited file format.
        /// </summary>
        PIPE,

        /// <summary>
        /// Tab delimited file format.
        /// </summary>
        TAB,

        /// <summary>
        /// XML file that matches the schema required for batch geocoding files in the Bing Spatial Data Services.
        /// </summary>
        XML
    }
}

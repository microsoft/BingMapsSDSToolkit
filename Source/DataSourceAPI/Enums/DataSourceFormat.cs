
namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// The data format of a data source. Note data sources should follow the documented file schema.
    /// </summary>
    public enum DataSourceFormat
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
        /// XML file that matches the schema required for Bing Spatial Data Services data sources.
        /// </summary>
        XML,

        /// <summary>
        /// A file in KML or KMZ format.
        /// </summary>
        KML,

        /// <summary>
        /// A file in the ESRI Shapefile format.
        /// </summary>
        SHP
    }
}

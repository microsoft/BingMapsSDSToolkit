using BingMapsSDSToolkit.DataSourceAPI;
using BingMapsSDSToolkit.GeocodeDataflowAPI;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// Useful utilities for working with file extensions.
    /// </summary>
    public static class FileExtensionUtilities
    {
        /// <summary>
        /// Give a data source format, this method provides a default and file extension filter string. 
        /// </summary>
        /// <param name="format">The data source file format to get the extensions for.</param>
        /// <param name="defaultExt">Default file extension.</param>
        /// <param name="filters">A file extension filter string</param>
        public static void GetFileExtensions(DataSourceFormat format, out string defaultExt, out string filters)
        {
            switch (format)
            {
                case DataSourceFormat.CSV:
                    defaultExt = ".csv";
                    filters = "CSV (*.csv)|*.csv|Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.TAB:
                case DataSourceFormat.PIPE:
                    defaultExt = ".txt";
                    filters = "Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.SHP:
                    defaultExt = ".zip";
                    filters = "Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.KML:
                    defaultExt = ".kml";
                    filters = "Kml (*.kml)|*.kml|Kmz (*.kmz)|*.kmz|Xml (.xml)|*.xml|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.XML:
                default:
                    defaultExt = ".xml";
                    filters = "Xml (*.xml)|*.xml|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
            }
        }

        /// <summary>
        /// Give a batch geocoding file format, this method provides a default and file extension filter string. 
        /// </summary>
        /// <param name="format">The batch geocoding file file format to get the extensions for.</param>
        /// <param name="defaultExt">Default file extension.</param>
        /// <param name="filters">A file extension filter string</param>
        public static void GetFileExtensions(BatchFileFormat format, out string defaultExt, out string filters)
        {
            switch (format)
            {
                case BatchFileFormat.CSV:
                    defaultExt = ".csv";
                    filters = "CSV (*.csv)|*.csv|Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case BatchFileFormat.TAB:
                case BatchFileFormat.PIPE:
                    defaultExt = ".txt";
                    filters = "Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case BatchFileFormat.XML:
                default:
                    defaultExt = ".xml";
                    filters = "Xml (.xml)|*.xml|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
            }
        }
    }
}

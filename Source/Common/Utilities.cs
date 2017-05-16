using BingMapsSDSToolkit.DataSourceAPI;
using BingMapsSDSToolkit.GeocodeDataFlowAPI;

namespace BingMapsSDSToolkit
{
    public static class Utilities
    {
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

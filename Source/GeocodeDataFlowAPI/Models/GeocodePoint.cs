using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// An object that stores the coordinate information.
    /// </summary>
    public class GeocodePoint : GeoDataLocation
    {
        /// <summary>
        /// The method used to calculate the coordinate.
        /// </summary>
        [XmlAttribute]
        public string CalculationMethod { get; set; }

        /// <summary>
        /// The Entity Type of the location result.
        /// </summary>
        [XmlAttribute]
        public string Type { get; set; }

        /// <summary>
        /// The recommended usage based on the calculation type.
        /// </summary>
        [XmlAttribute]
        public string UsageTypes { get; set; }
    }
}

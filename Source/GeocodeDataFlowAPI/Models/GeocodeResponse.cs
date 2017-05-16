using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    public class GeocodeResponse
    {
        /// <summary>
        /// The geocoded address result.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// An array of coordinates for the resulting locaiton.
        /// </summary>
        [XmlElement]
        public GeocodePoint[] GeocodePoint { get; set; }

        /// <summary>
        /// An array of the parsed query properties.
        /// </summary>
        [XmlElement]
        public QueryParseValue[] QueryParseValue { get; set; }

        /// <summary>
        /// A bounding box for viewing the location.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// A coordinate for the geocoded location.
        /// </summary>
        public GeoDataLocation Point { get; set; }

        /// <summary>
        /// The name of the geocoded location.
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// The geocode entity type of the geocoded location.
        /// </summary>
        [XmlAttribute]
        public string EntityType { get; set; }

        /// <summary>
        /// The confidence of the geococded results.
        /// </summary>
        [XmlAttribute]
        public string Confidence { get; set; }

        /// <summary>
        /// A comma seperated string of match codes. Use the MatchCodeTypes.ParseMatchCodes method to parse into an array of strings.
        /// </summary>
        [XmlAttribute]
        public string MatchCodes { get; set; }
    }
}

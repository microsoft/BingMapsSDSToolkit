using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// Request information for reverse geocoding a location cooridinate.
    /// </summary>
    public class ReverseGeocodeRequest : BaseRequest
    {
        /// <summary>
        /// The location coordinate to reverse geocode.
        /// </summary>
        public GeoDataLocation Location { get; set; }

        /// <summary>
        /// A list of Geocode Entity Types to return. This parameter only returns a geocoded address if 
        /// the entity type for that address is one of the entity types you specified.
        /// </summary>
        [XmlAttribute]
        public string IncludeEntityTypes { get; set; }
    }
}

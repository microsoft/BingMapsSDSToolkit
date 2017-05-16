using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// A parsed query value.
    /// </summary>
    public class QueryParseValue
    {
        /// <summary>
        /// The property name of the parsed query. See QueryParsePropertyTypes for possible values.
        /// </summary>
        [XmlAttribute]
        public string Property { get; set; }

        /// <summary>
        /// The value of parsed property.
        /// </summary>
        [XmlAttribute]
        public string Value { get; set; }
    }
}

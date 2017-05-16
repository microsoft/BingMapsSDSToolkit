using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// Request information for geocoding a location.
    /// </summary>
    public class GeocodeRequest : BaseRequest
    {
        #region Constructor

        /// <summary>
        /// Request information for geocoding a location.
        /// </summary>
        public GeocodeRequest()
        {
        }

        /// <summary>
        /// Request information for geocoding a location.
        /// </summary>
        /// <param name="query">A query string that contains address information to geocode. Can be used instead of an address.</param>
        public GeocodeRequest(string query)
        {
            this.Query = query;
        }

        #endregion

        /// <summary>
        /// The address to geocode.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// A boolean value that specifies whether to return parsing information.
        /// </summary>
        [XmlAttribute]
        public bool IncludeQueryParse { get; set; }

        /// <summary>
        /// A boolean indicating if the information on how the query propery was parsed should be returned.
        /// </summary>
        [XmlIgnore]
        public bool IncludeQueryParseSpecified { get; set; }

        /// <summary>
        /// A query string that contains address information to geocode. Can be used instead of an address.
        /// </summary>
        [XmlAttribute]
        public string Query { get; set; }
    }
}

using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// An eneity in the the geocode feed to be geocoded or reverse geocoded.
    /// </summary>
    public class GeocodeEntity
    {
        #region Constructor

        /// <summary>
        /// An eneity in the the geocode feed to be geocoded or reverse geocoded.
        /// </summary>
        public GeocodeEntity()
        {
        }

        /// <summary>
        /// An eneity in the the geocode feed to be geocoded or reverse geocoded.
        /// </summary>
        /// <param name="query">A query string that contains address information to geocode.Can be used instead of an address.</param>
        public GeocodeEntity(string query)
        {
            this.GeocodeRequest = new GeocodeRequest(query);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A string that contains a unique ID to assign to the entity.
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        /// The reverse geocode request being made.
        /// </summary>
        public ReverseGeocodeRequest ReverseGeocodeRequest { get; set; }

        /// <summary>
        /// The geocode request being made.
        /// </summary>
        public GeocodeRequest GeocodeRequest { get; set; }

        /// <summary>
        /// The results of the geocoding/reverse geocoding process. Do not set this property.
        /// </summary>
        [XmlElement]
        public GeocodeResponse[] GeocodeResponse { get; set; }

        /// <summary>
        /// A string that provides information about the success of the operation.
        /// Do not set this property.
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Information about an error that occurred during the geocode dataflow job. 
        /// This value is provided only for data that was not processed successfully.
        /// Do not set this property.
        /// </summary>
        public string FaultReason { get; set; }

        /// <summary>
        /// An id used to trace the request through the Bing servers. Do not set this property.
        /// </summary>
        public string TraceId { get; set; }

        #endregion
    }
}

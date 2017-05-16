using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// An object that contains the response from the GeoData API.
    /// </summary>
    [DataContract]
    public class GeoDataResponse
    {
        /// <summary>
        /// The result set from the GeoData API.
        /// </summary>
        [DataMember(Name = "d", EmitDefaultValue = false)]
        public GeoDataResultSet ResultSet { get; set; }
    }
}

using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// A result set from the GeoData API.
    /// </summary>
    [DataContract]
    public class GeoDataResultSet
    {
        /// <summary>
        /// A copyright string.
        /// </summary>
        [DataMember(Name = "Copyright", EmitDefaultValue = false)]
        public string Copyright { get; set; }

        /// <summary>
        /// An array of GeoData boundary results. 
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public GeoDataResult[] Results { get; set; }
    }
}

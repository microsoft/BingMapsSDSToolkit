using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// An object that contains the response from the Geodata API.
    /// </summary>
    [DataContract]
    public class GeodataResponse
    {
        /// <summary>
        /// The result set from the Geodata API.
        /// </summary>
        [DataMember(Name = "d", EmitDefaultValue = false)]
        public GeodataResultSet ResultSet { get; set; }
    }
}

using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// A result set from the Geodata API.
    /// </summary>
    [DataContract]
    public class GeodataResultSet
    {
        /// <summary>
        /// A copyright string.
        /// </summary>
        [DataMember(Name = "Copyright", EmitDefaultValue = false)]
        public string Copyright { get; set; }

        /// <summary>
        /// An array of Geodata boundary results. 
        /// </summary>
        [DataMember(Name = "results", EmitDefaultValue = false)]
        public GeodataResult[] Results { get; set; }
    }
}

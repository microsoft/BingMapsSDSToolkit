using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// Copyright information.
    /// </summary>
    [DataContract]
    public class Copyright
    {
        /// <summary>
        /// The copyright URL for the GeoData service. 
        /// </summary>
        [DataMember(Name = "CopyrightURL", EmitDefaultValue = false)]
        public string CopyrightURL { get; set; }

        /// <summary>
        /// A collection of CopyrightSource objects that give information about the sources of the polygon data that is returned. 
        /// </summary>
        [DataMember(Name = "Sources", EmitDefaultValue = false)]
        public CopyrightSource[] Sources { get; set; }
    }
}

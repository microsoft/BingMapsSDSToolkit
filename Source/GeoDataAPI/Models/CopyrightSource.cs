using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// Copyright source information.
    /// </summary>
    [DataContract]
    public class CopyrightSource
    {
        /// <summary>
        /// An ID identifying the data provider that supplied the data. 
        /// </summary>
        [DataMember(Name = "SourceID", EmitDefaultValue = false)]
        public string SourceID { get; set; }

        /// <summary>
        /// The name of the data provider represented by this Source element.
        /// </summary>
        [DataMember(Name = "SourceName", EmitDefaultValue = false)]
        public string SourceName { get; set; }

        /// <summary>
        /// The copyright string for the source.
        /// </summary>
        [DataMember(Name = "Copyright", EmitDefaultValue = false)]
        public string Copyright { get; set; }
    }
}

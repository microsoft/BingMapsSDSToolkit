using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeoDataAPI
{
    /// <summary>
    /// An object that contains information about the boundary, such as the name and culture.
    /// </summary>
    [DataContract]
    public class Name
    {
        /// <summary>
        /// The name of boundary. 
        /// </summary>
        /// <example>United States</example>
        [DataMember(Name = "EntityName", EmitDefaultValue = false)]
        public string EntityName { get; set; }

        /// <summary>
        /// The culture of the region.
        /// </summary>
        [DataMember(Name = "Culture", EmitDefaultValue = false)]
        public string Culture { get; set; }

        /// <summary>
        /// An ID identifying the data provider that supplied the data. 
        /// </summary>
        [DataMember(Name = "SourceID", EmitDefaultValue = false)]
        public string SourceID { get; set; }
    }

}

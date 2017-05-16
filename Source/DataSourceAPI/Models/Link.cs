using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// A data source job link.
    /// </summary>
    [DataContract]
    public class Link
    {
        /// <summary>
        /// The role of the link.
        /// </summary>
        [DataMember(Name = "role", EmitDefaultValue = false)]
        public string Role { get; set; }

        /// <summary>
        /// The name of the link.
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The URL of the link.
        /// </summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string URL { get; set; }
    }
}

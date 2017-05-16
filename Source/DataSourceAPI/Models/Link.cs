using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    [DataContract]
    public class Link
    {
        [DataMember(Name = "role", EmitDefaultValue = false)]
        public string Role { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string URL { get; set; }
    }
}

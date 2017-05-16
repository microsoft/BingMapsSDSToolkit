using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// An object that contains metadata about a boundary. Not all fields will always be populated.
    /// </summary>
    [DataContract]
    public class Metadata
    {
        /// <summary>
        /// The approximate total surface area (in square kilometers) covered by all the polygons that comprise this entity.
        /// </summary>
        [DataMember(Name = "AreaSqKm", EmitDefaultValue = false)]
        public string AreaSqKm { get; set; }

        /// <summary>
        /// An area on the Earth that provides the best map view for this entity. This area is defined as a 
        /// bounding box which represents an area using a set of latitude and longitude values: South Latitude, 
        /// East Longitude, North Latitude, East Longitude. 
        /// </summary>
        [DataMember(Name = "BestMapViewBox", EmitDefaultValue = false)]
        public string BestMapViewBox { get; set; }

        /// <summary>
        /// The culture associated with this entity.
        /// </summary>
        [DataMember(Name = "OfficialCulture", EmitDefaultValue = false)]
        public string OfficialCulture { get; set; }

        /// <summary>
        /// The regional culture associated with this entity.
        /// </summary>
        [DataMember(Name = "RegionalCulture", EmitDefaultValue = false)]
        public string RegionalCulture { get; set; }

        /// <summary>
        /// The approximate population within this entity. 
        /// </summary>
        /// <example>PopClass20000to99999</example>
        [DataMember(Name = "PopulationClass", EmitDefaultValue = false)]
        public string PopulationClass { get; set; }
    }
}

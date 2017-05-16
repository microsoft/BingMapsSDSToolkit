using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// A collection of DataflowJob's
    /// </summary>
    [DataContract]
    public class DataflowResultSet
    {
        /// <summary>
        /// An estimate of the total number of resources in the ResourceSet.
        /// </summary>
        [DataMember(Name = "estimatedTotal", EmitDefaultValue = false)]
        public long EstimatedTotal { get; set; }

        /// <summary>
        /// A collection of one or more DataflowJob resources.
        /// </summary>
        [DataMember(Name = "resources", EmitDefaultValue = false)]
        public DataServiceJob[] Resources { get; set; }
    }
}

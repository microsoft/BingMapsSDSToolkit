using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// A response object from the Bing Spatial Data Services when making job requests.
    /// </summary>
    [DataContract]
    public class DataflowResponse
    {
        /// <summary>
        /// A copyright notice.
        /// </summary>
        [DataMember(Name = "copyright", EmitDefaultValue = false)]
        public string Copyright { get; set; }

        /// <summary>
        /// A URL that references a brand image to support contractual branding requirements.
        /// </summary>
        [DataMember(Name = "brandLogoUri", EmitDefaultValue = false)]
        public string BrandLogoUri { get; set; }

        /// <summary>
        /// The HTTP Status code for the request.
        /// </summary>
        [DataMember(Name = "statusCode", EmitDefaultValue = false)]
        public int StatusCode { get; set; }

        /// <summary>
        /// A description of the HTTP status code.
        /// </summary>
        [DataMember(Name = "statusDescription", EmitDefaultValue = false)]
        public string StatusDescription { get; set; }

        /// <summary>
        /// A status code that offers additional information about authentication success or failure.
        /// </summary>
        [DataMember(Name = "authenticationResultCode", EmitDefaultValue = false)]
        public string AuthenticationResultCode { get; set; }

        /// <summary>
        /// A collection of error descriptions. 
        /// </summary>
        [DataMember(Name = "errorDetails", EmitDefaultValue = false)]
        public string[] ErrorDetails { get; set; }
        
        /// <summary>
        /// A unique identifier for the request.
        /// </summary>
        [DataMember(Name = "traceId", EmitDefaultValue = false)]
        public string TraceId { get; set; }

        /// <summary>
        /// A collection of DataflowResourceSet objects. A DataflowResourceSet is a container of DataflowJob's returned by the request.
        /// </summary>
        [DataMember(Name = "resourceSets", EmitDefaultValue = false)]
        public DataflowResultSet[] ResourceSets { get; set; }
    }
}

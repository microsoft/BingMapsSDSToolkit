using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object that contains the information of a DataServiceJob.
    /// </summary>
    [KnownType(typeof(DataflowJob))]
    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    public class DataServiceJob
    {
        /// <summary>
        /// Type name of the data service or flow job.
        /// </summary>
        [DataMember(Name = "__type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// A unique string that identifies the dataflow job. There are no requirements for the string format.
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The date and time that the dataflow job is completed. If the Status field is set to Pending, the CompletedDate field is not shown or is empty.
        /// </summary>
        [DataMember(Name = "completedDate", EmitDefaultValue = false)]
        public string CompletedDate { get; set; }

        /// <summary>
        /// The date and time that the dataflow job was created.
        /// </summary>
        [DataMember(Name = "createdDate", EmitDefaultValue = false)]
        public string CreatedDate { get; set; }

        /// <summary>
        /// A user-defined description of the dataflow job. If a description is not specified when the workflow is created, this field is not included or the value is null.
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The status of the dataflow job. Possible values are 'Pending', 'Aborted', or 'Completed'.
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        /// <summary>
        /// URLs that is defined by its role and name attributes. 
        /// “role”:”self”: Use to check the status of your job. This URL is provided in the response when you create a load data source job.
        /// “role”:”self” and “name”:“datasource”: Use to query and update a data source. This URL appears in the response to a job status request when the load data source job completes.
        /// “role”:”output” and “name”:”failed”: Use to access the error log for the job. This URL appears in the response only when the job status is set to Aborted.
        /// </summary>
        [DataMember(Name = "links", EmitDefaultValue = false)]
        public Link[] Links { get; set; }

        /// <summary>
        /// A string containing an error message if an error occurs while processing the job.
        /// </summary>
        [DataMember(Name = "errorMessage", EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }
    }
}

using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object that represents a single data flow job.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    public class DataflowJob : DataServiceJob
    {
        #region Constructor

        /// <summary>
        /// An object that represents a single data flow job.
        /// </summary>
        public DataflowJob()
        {
        }

        /// <summary>
        /// An object that represents a single data flow job.
        /// </summary>
        /// <param name="job">A data service job to convert into a DataflowJob.</param>
        public DataflowJob(DataServiceJob job)
        {
            this.Id = job.Id;
            this.Links = job.Links;
            this.ErrorMessage = job.ErrorMessage;
            this.CompletedDate = job.CompletedDate;
            this.CreatedDate = job.CreatedDate;
            this.Description = job.Description;
            this.Status = job.Status;
            this.Type = job.Type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The number of entities that did not process successfully because of an error.
        /// </summary>
        [DataMember(Name = "failedEntityCount", EmitDefaultValue = false)]
        public int FailedEntityCount { get; set; }

        /// <summary>
        /// The number of entities that were processed. This number included entities that were processed successfully and those that failed. If the field is set to 0, the number of processed entries is not known.
        /// </summary>
        [DataMember(Name = "processedEntityCount", EmitDefaultValue = false)]
        public int ProcessedEntityCount { get; set; }

        /// <summary>
        /// The total number of entities that were uploaded.
        /// </summary>
        [DataMember(Name = "totalEntityCount", EmitDefaultValue = false)]
        public int TotalEntityCount { get; set; }

        #endregion
    }
}

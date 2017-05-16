
namespace BingMapsSDSToolkit
{
    /// <summary>
    /// A summary of status information returned in the response when you check job status. 
    /// </summary>
    internal class DownloadDetails
    {
        public string JobStatus { get; set; }

        public string SucceededUrl { get; set; }

        public string FailedUrl { get; set; }
    }
}

/*
 * Copyright(c) 2017 Microsoft Corporation. All rights reserved. 
 * 
 * This code is licensed under the MIT License (MIT). 
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal 
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
 * of the Software, and to permit persons to whom the Software is furnished to do 
 * so, subject to the following conditions: 
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE. 
*/

using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// A tool for doing batch geocoding and reverse geocoding using the Bing Spatial Data Services.
    /// </summary>
    public class BatchGeocodeManager
    {
        #region Private Properties

        /// <summary>
        /// Interval used to check the status of the batch job in ms.
        /// </summary>
        private int _statusUpdateInterval = 15000;

        #endregion

        #region Constructor

        /// <summary>
        /// A tool for doing batch geocoding and reverse geocoding using the Bing Spatial Data Services.
        /// </summary>
        public BatchGeocodeManager() { }

        /// <summary>
        /// A tool for doing batch geocoding and reverse geocoding using the Bing Spatial Data Services.
        /// </summary>
        /// <param name="statusUpdateInterval">The update interval in ms that should be used when checking the status of a job. Default 15000ms (15 sec.)</param>
        public BatchGeocodeManager(int statusUpdateInterval)
        {
            _statusUpdateInterval = statusUpdateInterval;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// An event that provides update messages during the batch geocode process.
        /// </summary>
        public Action<string> StatusChanged;

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to geocode a set of data.
        /// </summary>
        /// <param name="dataFeed">GeocodeFeed which contains the data to batch geocode/reverse geocode.</param>
        /// <param name="bingMapsKey">Bing Maps key to use for accessing service.</param>
        /// <returns>The results of the batch geocoding process.</returns>
        public async Task<BatchGeocoderResults> Geocode(GeocodeFeed dataFeed, string bingMapsKey)
        {
            BatchGeocoderResults results;

            try
            {
                ReportStatus("Creating batch geocode job.");

                string dataflowJobLocation = await CreateJob(dataFeed, bingMapsKey);
                
                ReportStatus("Job created and being processed.");

                //Continue to check the dataflow job status until the job has completed
                var statusDetails = new DownloadDetails();

                do
                {
                    statusDetails = await CheckStatus(dataflowJobLocation, bingMapsKey);
                    
                    if (statusDetails.JobStatus == "Aborted")
                    {
                        ReportStatus("Batch geocode job aborted.");
                        return new BatchGeocoderResults()
                        {
                            Error = "Batch geocode job was aborted due to an error."
                        };
                    }

                    if (statusDetails.JobStatus.Equals("Pending"))
                    {
                        await Task.Delay(_statusUpdateInterval);
                    }
                }
                while (statusDetails.JobStatus.Equals("Pending"));

                ReportStatus("Batch geocode job completed. Downloading results.");
                results = await DownloadResults(statusDetails, bingMapsKey);

                ReportStatus("Batch geocode results downloaded.");
            }
            catch (Exception ex)
            {
                results = new BatchGeocoderResults()
                {
                    Error = ex.Message
                };
            }

            return results;
        }

        #endregion

        #region Private Methods

        private void ReportStatus(string msg)
        {
            if (StatusChanged != null)
            {
                StatusChanged(msg);
            }
        }

        /// <summary>
        /// Creates a geocode dataflow job and uploads spatial data to process.
        /// </summary>
        /// <param name="dataFeed">The address data to geocode.</param>
        /// <param name="bingMapsKey">The Bing Maps Key to use for this job. The same key is used to get job status and download results.</param>
        /// <returns>A URL that defines the location of the geocode dataflow job that was created.</returns>
        private async Task<string> CreateJob(GeocodeFeed dataFeed, string bingMapsKey)
        {
            var tcs = new TaskCompletionSource<string>();

            //Build the HTTP URI that will upload and create the geocode dataflow job
            Uri createJobUri = new Uri("https://spatial.virtualearth.net/REST/v1/dataflows/geocode?input=xml&clientApi=" + InternalSettings.ClientApi + "&key=" + bingMapsKey);

            //Include the data to geocode in the HTTP request
            var request = HttpWebRequest.Create(createJobUri);

            // The HTTP method must be 'POST'.
            request.Method = "POST";
            //request.ContentType = "application/xml";
            request.ContentType = "application/octet-stream";

            using (var requestStream = await request.GetRequestStreamAsync())
            {
                using (var zipStream = new GZipStream(requestStream, CompressionMode.Compress))
                {
                    await dataFeed.WriteAsync(zipStream);
                }
            }

            request.BeginGetResponse((a) =>
            {
                var r = (HttpWebRequest)a.AsyncState;               

                try{
                    using (var response = (HttpWebResponse)r.EndGetResponse(a))
                    {
                        // If the job was created successfully, the status code should be
                        // 201 (Created) and the 'Location' header should contain a URL
                        // that defines the location of the new dataflow job. You use this 
                        // URL with the Bing Maps Key to query the status of your job.
                        if (response.StatusCode != HttpStatusCode.Created)
                        {
                            tcs.SetException(new Exception("An HTTP error status code was encountered when creating the geocode job."));
                        }

                        string dataflowJobLocation = response.Headers["Location"];
                        if (String.IsNullOrEmpty(dataflowJobLocation))
                        {
                            tcs.SetException(new Exception("The 'Location' header is missing from the HTTP response when creating a goecode job."));
                        }

                        tcs.SetResult(dataflowJobLocation);
                    }
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }, request);

            return await tcs.Task;
        }

        /// <summary>
        /// Checks the status of a dataflow job and defines the URLs to use to download results when the job is completed.
        /// </summary>
        /// <param name="dataflowJobLocation">The URL to use to check status for a job.</param>
        /// <param name="bingMapsKey">The Bing Maps Key for this job. The same key is used to create the job and download results.</param>
        /// <returns>
        /// A DownloadDetails object that contains the status of the geocode dataflow job (Completed, Pending, Aborted). 
        /// When the status is set to Completed, DownloadDetails also contains the links to download the results
        /// </returns>
        private async Task<DownloadDetails> CheckStatus(string dataflowJobLocation, string bingMapsKey)
        {
            //Build the HTTP Request to get job status
            var uriBuilder = new UriBuilder(dataflowJobLocation + @"?key=" + bingMapsKey + "&output=xml&clientApi=" + InternalSettings.ClientApi);
            var request = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
            request.Method = "GET";
                       
            var statusDetails = new DownloadDetails()
            {
                JobStatus = "Pending"
            };

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("An HTTP error status code was encountered when checking job status.");
                }

                using (var receiveStream = response.GetResponseStream())
                {
                    var reader = XmlReader.Create(receiveStream);
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            if (reader.Name.Equals("Status"))
                            {
                                //return job status
                                statusDetails.JobStatus = reader.ReadElementContentAsString();
                                break;
                            }
                            else if (reader.Name.Equals("Link"))
                            {
                                //Set the URL location values for retrieving 
                                // successful and failed job results
                                reader.MoveToFirstAttribute();
                                if (reader.Value.Equals("output"))
                                {
                                    reader.MoveToNextAttribute();
                                    if (reader.Value.Equals("succeeded"))
                                    {
                                        reader.MoveToContent();
                                        statusDetails.SucceededUrl = reader.ReadElementContentAsString();
                                    }
                                    else if (reader.Value.Equals("failed"))
                                    {
                                        reader.MoveToContent();
                                        statusDetails.FailedUrl = reader.ReadElementContentAsString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return statusDetails;
        }

        /// <summary>
        /// Downloads job results to files names Success.txt (successfully geocoded results) and Failed.txt (info about spatial data that was not geocoded successfully).
        /// </summary>
        /// <param name="statusDetails">Inclues job status and the URLs to use to download all geocoded results.</param>
        /// <param name="bingMapsKey">The Bing Maps Key for this job. The same key is used to create the job and get job status. </param>
        private async Task<BatchGeocoderResults> DownloadResults(DownloadDetails statusDetails, string bingMapsKey)
        {
            var results = new BatchGeocoderResults();

            //Write the results for data that was geocoded successfully to a file named Success.xml
            if (statusDetails.SucceededUrl != null && !statusDetails.SucceededUrl.Equals(String.Empty))
            {
                //Create a request to download successfully geocoded data. You must add the Bing Maps Key to the 
                //download location URL provided in the response to the job status request.
                var successUriBuilder = new UriBuilder(statusDetails.SucceededUrl + "?clientApi=SDSToolkit&key=" + bingMapsKey);

                var successfulRequest = (HttpWebRequest)WebRequest.Create(successUriBuilder.Uri);
                successfulRequest.Method = "GET";

                using (var response = (HttpWebResponse)await successfulRequest.GetResponseAsync())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("An HTTP error status code was encountered when downloading results.");
                    }

                    using (var receiveStream = response.GetResponseStream())
                    {
                        results.Succeeded = await GeocodeFeed.ReadAsync(receiveStream);
                    }
                }
            }

            //If some spatial data could not be geocoded, write the error information to a file called Failed.xml
            if (statusDetails.FailedUrl != null && !statusDetails.FailedUrl.Equals(String.Empty))
            {
                var failedRequest = (HttpWebRequest)WebRequest.Create(new Uri(statusDetails.FailedUrl + "?clientApi=SDSToolkit&key=" + bingMapsKey));
                failedRequest.Method = "GET";

                using (var response = (HttpWebResponse)await failedRequest.GetResponseAsync())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("An HTTP error status code was encountered when downloading results.");
                    }

                    using (Stream receiveStream = response.GetResponseStream())
                    {
                        results.Failed = await GeocodeFeed.ReadAsync(receiveStream);
                    }
                }
            }

            return results;
        }

        #endregion
    }
}
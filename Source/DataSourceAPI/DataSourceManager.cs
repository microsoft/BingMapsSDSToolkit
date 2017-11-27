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

using BingMapsSDSToolkit.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// A tool for managing data sources and data flow jobs in the Bing Saptial Data Services.
    /// </summary>
    public class DataSourceManager
    {
        #region Private Properties

        /// <summary>
        /// Interval used to check the status of the batch job in ms.
        /// </summary>
        private int _statusUpdateInterval = 15000;

        #endregion

        #region Constructor

        /// <summary>
        /// A tool for managing data sources and data flow jobs in the Bing Saptial Data Services.
        /// </summary>
        public DataSourceManager() { }

        /// <summary>
        /// A tool for managing data sources and data flow jobs in the Bing Saptial Data Services.
        /// </summary>
        /// <param name="statusUpdateInterval">The update interval in ms that should be used when checking the status of a job. Default 15000ms (15 sec.)</param>
        public DataSourceManager(int statusUpdateInterval)
        {
            _statusUpdateInterval = statusUpdateInterval;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// An event that provides update messages during the upload process.
        /// </summary>
        public Action<string> UploadStatusChanged;

        #endregion

        #region Public Methods

        #region Get Job List

        /// <summary>
        /// Gets all the recent Bing Spatial Data Services data flow jobs for a Bing Maps account.
        /// </summary>
        /// <param name="bingMapsKey">A Bing Maps key for the account in which you want to get all jobs for.</param>
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of all data flow jobs.</returns>
        public async Task<List<DataflowJob>> GetJobList(string bingMapsKey)
        {
            if (string.IsNullOrWhiteSpace(bingMapsKey))
            {               
                throw new Exception("Invalid Bing Maps Key.");
            }

            string request = "http://spatial.virtualearth.net/REST/v1/dataflows/listjobs?key=" + bingMapsKey;
            return await DownloadDataflowJobs(request);
        }

        #endregion

        #region Get Public Data Sources

        /// <summary>
        /// Gets a list of all the data sources in the Bing Spatial Data Services that are marked as public.
        /// </summary>
        /// <param name="bingMapsKey">A bing maps key to authenicate the service request.</param>
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of information for all public data sources.</returns>
        public async Task<List<DataSourceDetails>> GetAllPublicDataSources(string bingMapsKey)
        {
            if (string.IsNullOrWhiteSpace(bingMapsKey))
            {
                throw new Exception("Invalid Bing Maps Key.");
            }

            string request = "http://spatial.virtualearth.net/REST/v1/data/$GetPublicDataSourceList?$format=atom&key=" + bingMapsKey;

            return await DownloadDataSourceInfo(request);
        }

        #endregion

        #region Get Data Source Info

        /// <summary>
        /// Gets details for all data sources in a Bing Maps account. Such as the date and time that each data source was last updated, 
        /// the data source and entity type names of the data sources, and if they are public or not.
        /// </summary>
        /// <param name="bingMapsKey">A Bing Maps key from the Bing Maps account you want to get the data source details for.</param>
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of data source details.</returns>
        public async Task<List<DataSourceDetails>> GetDetails(string bingMapsKey)
        {
            return await GetDetails(bingMapsKey, null, null, false);
        }

        /// <summary>
        /// Gets details for a specific data sources in a Bing Maps account. 
        /// </summary>
        /// <param name="bingMapsKey">A Bing Maps key from the Bing Maps account you want to get the data source details for.</param>
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>             
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of data source details.</returns>
        public async Task<List<DataSourceDetails>> GetDetails(string bingMapsKey, string accessId, string dataSourceName)
        {
            return await GetDetails(bingMapsKey, accessId, dataSourceName, false);
        }

        /// <summary>
        /// Gets details for a specific data sources in a Bing Maps account. 
        /// If the access id and data source name is specified details for a specific data sources in a Bing Maps account. 
        /// Otherwise details for all data sources in a Bing Maps account are return. 
        /// </summary>
        /// <param name="bingMapsKey">A Bing Maps key from the Bing Maps account you want to get the data source details for.</param>
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>        
        /// <param name="showAllVersions">A boolean value indicating if information for all versions of data sources should be returned.</param>
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of data source details.</returns>
        public async Task<List<DataSourceDetails>> GetDetails(string bingMapsKey, string accessId, string dataSourceName, bool showAllVersions)
        {
            string request = "http://spatial.virtualearth.net/REST/v1/data";

            if (!string.IsNullOrWhiteSpace(accessId) && !string.IsNullOrWhiteSpace(dataSourceName))
            {
                request += "/" + accessId + "/" + dataSourceName;
            }

            request += "?$format=atom";

            if (showAllVersions)
            {
                request += "&showAllVersions=true";
            }

            if (!string.IsNullOrWhiteSpace(bingMapsKey))
            {
                request += "&key=" + bingMapsKey;
            }
            else
            {
                throw new Exception("Invalid Bing Maps Key.");
            }

            return await DownloadDataSourceInfo(request);
        }

        /// <summary>
        /// Gets details for a specific data sources in a Bing Maps account. 
        /// Requires Access ID, Data Source Name and Master or Query Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of data source details.</returns>
        public async Task<List<DataSourceDetails>> GetDetails(BasicDataSourceInfo info)
        {
            return await GetDetails(info, false);
        }

        /// <summary>
        /// Gets details for a specific data sources in a Bing Maps account. 
        /// Requires Access ID, Data Source Name and Master or Query Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <param name="showAllVersions">A boolean value indicating if information for all versions of data sources should be returned.</param>
        /// <exception cref="System.Exception">Thrown when a Bing Maps key is not specified.</exception>
        /// <returns>A list of data source details.</returns>
        public async Task<List<DataSourceDetails>> GetDetails(BasicDataSourceInfo info, bool showAllVersions)
        {
            string key = (string.IsNullOrEmpty(info.MasterKey)) ? info.QueryKey : info.MasterKey;
            return await GetDetails(key, info.AccessId, info.DataSourceName, showAllVersions);
        }

        #endregion

        #region Publish Staged Data Source

        /// <summary>
        /// Publishes a staged data source to production.
        /// Requires Access ID, Data Source Name and Master Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <returns>The final data flow job when the publishing process completes or is aborted.</returns>
        public async Task<DataServiceJob> PublishStagedDataSource(BasicDataSourceInfo info)
        {
            return await PublishStagedDataSource(info.AccessId, info.DataSourceName, info.MasterKey);
        }

        /// <summary>
        /// Publishes a staged data source to production.
        /// </summary>
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>        
        /// <param name="masterKey">A Bing Maps key that has full control of the data source.</param>
        /// <returns>The final data flow job when the publishing process completes or is aborted.</returns>
        public async Task<DataServiceJob> PublishStagedDataSource(string accessId, string dataSourceName, string masterKey)
        {
            DataflowJob result = new DataflowJob();

            try
            {
                ValidateProperties(accessId, dataSourceName, masterKey);

                string request = string.Format("https://spatial.virtualearth.net/REST/v1/data/{0}/{1}/$commit?output=json&key={2}&clientApi={3}", accessId, dataSourceName, masterKey, InternalSettings.ClientApi);

                var jobs = await DownloadDataServiceJobs(request);

                if (jobs != null && jobs.Count > 0 &&
                    string.Compare(jobs[0].Description, "DataSourcePublishFromStaged", StringComparison.OrdinalIgnoreCase) == 0 &&
                    jobs[0].Links != null && jobs[0].Links.Length > 0)
                {
                    string statusUrl = string.Empty;
                    foreach (var l in jobs[0].Links)
                    {
                        if (string.Compare(l.Role, "self", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            statusUrl = l.Url;
                            break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(statusUrl))
                    {
                        result = await MonitorStatus(new Uri(statusUrl));
                    }
                }                
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Status = "Aborted";
            }

            return result;
        }

        #endregion

        #region Upload Data Source

        /// <summary>
        /// Uploads a data source to the Bing Spatial Data Services. Skips rows that have no location information.
        /// </summary>
        /// <param name="dataSource">The data source that is to be uploaded.</param>
        /// <param name="loadOperation">The type of operation to perform when uploading a data source.</param>
        /// <returns>The final data flow job when the upload process completes or is aborted.</returns>
        public async Task<DataflowJob> Upload(DataSource dataSource, LoadOperation loadOperation)
        {
            return await Upload(dataSource, loadOperation, false, true);
        }

        /// <summary>
        /// Uploads a data source to the Bing Spatial Data Services. Skips rows that have no location information.
        /// </summary>
        /// <param name="dataSource">The data source that is to be uploaded.</param>
        /// <param name="loadOperation">The type of operation to perform when uploading a data source.</param>
        /// <param name="setPublic">A boolean value indicating if the data source should be made public or not.</param>
        /// <returns>The final data flow job when the upload process completes or is aborted.</returns>
        public async Task<DataflowJob> Upload(DataSource dataSource, LoadOperation loadOperation, bool setPublic)
        {
            return await Upload(dataSource, loadOperation, setPublic, true);
        }

        /// <summary>
        /// Uploads a data source to the Bing Spatial Data Services. 
        /// </summary>
        /// <param name="dataSource">The data source that is to be uploaded.</param>
        /// <param name="loadOperation">The type of operation to perform when uploading a data source.</param>
        /// <param name="setPublic">A boolean value indicating if the data source should be made public or not.</param>
        /// <param name="skipEmptyLocations">A boolean value indicating if rows that don't have locaiton information (latitude, longitude, or Geography column) should be uploaded or not. If all rows do not have location information an error will occur and the upload aborted.</param>
        /// <returns>The final data flow job when the upload process completes or is aborted.</returns>
        public async Task<DataflowJob> Upload(DataSource dataSource, LoadOperation loadOperation, bool setPublic, bool skipEmptyLocations)
        {
            var result = new DataflowJob();

            try
            {
                if (string.IsNullOrWhiteSpace(dataSource.Info.MasterKey))
                {
                    throw new Exception("A valid Bing Maps key must be specified as a master key for the data source.");
                }

                var validation = await dataSource.Validate();
                if (validation.Errors != null && validation.Errors.Count > 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Data source failed validation process.");
                    foreach (var e in validation.Errors)
                    {
                        sb.AppendLine(e);
                    }

                    throw new Exception(sb.ToString());
                }

                if (!validation.AllRowsHaveLocationInfo && !skipEmptyLocations)
                {
                    throw new Exception("Not all rows of the data source contain location information.");
                }
                
                string request = string.Format("https://spatial.virtualearth.net/REST/v1/Dataflows/LoadDataSource?loadOperation={0}&dataSourceName={1}&setPublic={2}&input=xml&output=json&key={3}&clientApi={4}",
                    loadOperation,
                    dataSource.Info.DataSourceName,
                    (setPublic) ? 1 : 0,
                    dataSource.Info.MasterKey, InternalSettings.ClientApi);

                if (!string.IsNullOrWhiteSpace(dataSource.Info.QueryKey))
                {
                    request += "&queryKey=" + dataSource.Info.QueryKey;
                }

                if(!string.IsNullOrWhiteSpace(dataSource.Info.AccessId) && (loadOperation == LoadOperation.Incremental || loadOperation == LoadOperation.IncrementalStaging)){
                     request += "&accessId=" + dataSource.Info.AccessId;
                }

                if (!string.IsNullOrWhiteSpace(dataSource.Info.Description))
                {
                    request += "&description=" + Uri.EscapeDataString(dataSource.Info.Description);
                }

                ReportUploadStatus("Creating upload job.");
                var dataflowJobLocation = await CreateUploadJob(request, dataSource, skipEmptyLocations);
                var statusUrl = new Uri(dataflowJobLocation + "?output=json&key=" + dataSource.Info.MasterKey);

                ReportUploadStatus("Upload job created. Monitoring status.");
                result = await MonitorStatus(statusUrl);
                ReportUploadStatus("Upload job " + result.Status);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Status = "Aborted";
                ReportUploadStatus("Upload aborted.\r\n" + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Uploads KML and SHP files streams as a data source.
        /// </summary>
        /// <param name="dataSourceStream">A file stream for a data source that is in KML or SHP file format.</param>
        /// <param name="format">Data Source file Format.</param>
        /// <param name="loadOperation">The type of operation to perform when uploading a data source.</param>
        /// <param name="info">Information about the datasource.</param>
        /// <param name="setPublic">A boolean value indicating if the data source should be made public or not.</param>
        /// <returns>The final data flow job when the upload process completes or is aborted.</returns>
        public async Task<DataflowJob> Upload(Stream dataSourceStream, DataSourceFormat format, LoadOperation loadOperation, BasicDataSourceInfo info, bool setPublic)
        {
            var result = new DataflowJob();

            try
            {
                if (string.IsNullOrWhiteSpace(info.MasterKey))
                {
                    throw new Exception("A valid Bing Maps key must be specified as a master key for the data source.");
                }

                if (format != DataSourceFormat.KML && format != DataSourceFormat.SHP)
                {
                    var dataSource = new DataSource(info);                    

                    if (await dataSource.ReadAsync(dataSourceStream, format))
                    {
                        return await Upload(dataSource, loadOperation, setPublic);
                    }
                    else
                    {
                        throw new Exception("Unable to read data source file.");
                    }                 
                }

                //Handle KML and SHP files.          

                string request = string.Format("https://spatial.virtualearth.net/REST/v1/Dataflows/LoadDataSource?loadOperation={0}&dataSourceName={1}&setPublic={2}&input={3}&output=json&key={4}&clientApi={5}",
                    loadOperation,
                    info.DataSourceName,
                    (setPublic) ? 1 : 0,
                    (format == DataSourceFormat.KML) ? "kml" : "shp",
                    info.MasterKey, InternalSettings.ClientApi);

                if (!string.IsNullOrWhiteSpace(info.QueryKey))
                {
                    request += "&queryKey=" + info.QueryKey;
                }

                if (!string.IsNullOrWhiteSpace(info.Description))
                {
                    request += "&description=" + Uri.EscapeDataString(info.Description);
                }

                ReportUploadStatus("Creating upload job.");
                var dataflowJobLocation = await CreateUploadJob(request, dataSourceStream, format);
                var statusUrl = new Uri(dataflowJobLocation + "?output=json&key=" + info.MasterKey);

                ReportUploadStatus("Upload job created. Monitoring status.");
                result = await MonitorStatus(statusUrl);
                ReportUploadStatus("Upload job " + result.Status);
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Status = "Aborted";
                ReportUploadStatus("Upload aborted.\r\n" + ex.Message);
            }

            return result;
        }

        #endregion

        #region Download Data Source

        /// <summary>
        /// Downloads a data source.
        /// Requires Access ID, Data Source Name and Master Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <exception cref="System.Exception">Thrown when invalid data is specified or the download process is aborted.</exception>
        /// <returns>The downloaded data source.</returns>
        public async Task<DataSource> Download(BasicDataSourceInfo info)
        {
            return await Download(info.AccessId, info.DataSourceName, info.MasterKey);
        }

        /// <summary>
        /// Downloads a data source. 
        /// </summary>
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>        
        /// <param name="masterKey">A Bing Maps key that has full control of the data source.</param>
        /// <exception cref="System.Exception">Thrown when invalid data is specified or the download process is aborted.</exception>
        /// <returns>The downloaded data source.</returns>
        public async Task<DataSource> Download(string accessId, string dataSourceName, string masterKey)
        {
            ValidateProperties(accessId, dataSourceName, masterKey);

            var dataflowJobLocation = await CreateDownloadJob(accessId, dataSourceName, masterKey);

            var statusUrl = new Uri(dataflowJobLocation + "?output=json&key=" + masterKey);

            var job = await MonitorStatus(statusUrl);

            var dataSource = new DataSource();

            if (job.Status.Equals("Completed"))
            {
                string downloadUrl = null;
                foreach (Link l in job.Links)
                {
                    if (l.Role.Equals("output") && l.Name != null && l.Name.Equals("succeeded"))
                    {
                        downloadUrl = l.Url;
                        break;
                    }
                }

                using (var xmlStream = await ServiceHelper.GetStreamAsync(new Uri(downloadUrl + "?key=" + masterKey)))
                {                    
                    await dataSource.ReadAsync(xmlStream, DataSourceFormat.XML);
                    dataSource.Info.AccessId = accessId;
                }
            }
            else if (job.Status.Equals("Aborted"))
            {
                throw new Exception("Download Aborted.");
            }

            return dataSource;  
        }

        #endregion

        #region Set Public Setting

        /// <summary>
        /// Sets the data source public setting as either public or non-public.
        /// Requires Access ID, Data Source Name and Master Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <param name="makePublic">A boolean indicating if the data source should be made public or not.</param>
        /// <returns>A data flow job which contains the status of the job setting the public flag of a data source.</returns>
        public async Task<DataServiceJob> SetPublicSetting(BasicDataSourceInfo info, bool makePublic)
        {
            return await SetPublicSetting(info.AccessId, info.DataSourceName, info.MasterKey, makePublic);
        }

        /// <summary>
        /// Sets the data source public setting as either public or non-public.
        /// </summary>        
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>        
        /// <param name="masterKey">A Bing Maps key that has full control of the data source.</param>
        /// <param name="makePublic">A boolean indicating if the data source should be made public or not.</param>
        /// <returns>A data flow job which contains the status of the job setting the public flag of a data source.</returns>
        public async Task<DataServiceJob> SetPublicSetting(string accessId, string dataSourceName, string masterKey, bool makePublic)
        {
            var result = new DataServiceJob();
            try
            {
                ValidateProperties(accessId, dataSourceName, masterKey);

                string request = string.Format("http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/$updatedatasource?setPublic={2}&o=json&key={3}", accessId, dataSourceName, (makePublic) ? 1 : 0, masterKey);

                var jobs = await DownloadDataServiceJobs(request);
                if (jobs != null && jobs.Count > 0)
                {
                    result = jobs[0];
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Status = "Aborted";
            }

            return result;
        }

        #endregion

        #region Rollback Data source.

        /// <summary>
        /// Rolls back a data source to an earlier version.
        /// </summary>
        /// <param name="jobId">The job ID from a previous data source upload. To find the job ID for the data source version you want to restore, query for information about a data source using the showAllVersions parameter.</param>
        /// <param name="dataSourceName">Name of a data source.</param>
        /// <param name="masterKey">A Bing Maps key that has full control of the data source.</param>
        /// <returns>A data flow job which contains the status of the rollback job.</returns>
        public async Task<DataflowJob> RollbackDataSource(string jobId, string dataSourceName, string masterKey)
        {
            DataflowJob result = new DataflowJob();
            try
            {
                if (string.IsNullOrWhiteSpace(jobId))
                {
                    throw new Exception("Job id not specified.");
                }

                if (string.IsNullOrWhiteSpace(dataSourceName))
                {
                    throw new Exception("Data source name not specified.");
                }

                if (string.IsNullOrWhiteSpace(masterKey))
                {
                    throw new Exception("Master key not specified.");
                }

                var request = string.Format("https://spatial.virtualearth.net/REST/v1/Dataflows/DataSourceRollback/{0}/{1}?output=json&key={2}&clientApi={3}", jobId, dataSourceName, masterKey, InternalSettings.ClientApi);

                var jobs = await DownloadDataflowJobs(request);

                if (jobs != null && jobs.Count > 0 &&
                    string.Compare(jobs[0].Description, "DataSourceRollback", StringComparison.OrdinalIgnoreCase) == 0 &&
                    jobs[0].Links != null && jobs[0].Links.Length > 0)
                {
                    string statusUrl = string.Empty;
                    foreach (var l in jobs[0].Links)
                    {
                        if (string.Compare(l.Role, "self", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            statusUrl = l.Url;
                            break;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(statusUrl))
                    {
                        result = await MonitorStatus(new Uri(statusUrl));
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Status = "Aborted";
            }

            return result;
        }

        #endregion

        #region Delete Data Source

        /// <summary>
        /// Makes a request to delete a data source.
        /// Requires Access ID, Data Source Name and Master Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <exception cref="System.Exception">Thrown if invalid parameters are specified or there is an error making the request.</exception>
        /// <returns>A boolean value indicating if the request to delete a data source was successfully made or not.</returns>
        public Task<bool> DeleteDataSource(BasicDataSourceInfo info)
        {
            return DeleteDataSource(info.AccessId, info.DataSourceName, info.MasterKey);
        }

        /// <summary>
        /// Makes a request to delete a data source.
        /// Requires Access ID, Data Source Name and Master Key.
        /// </summary>
        /// <param name="info">Basic data source information.</param>
        /// <param name="isStaging">A boolean indicating if the staging data source should be deleted or not.</param>
        /// <exception cref="System.Exception">Thrown if invalid parameters are specified or there is an error making the request.</exception>
        /// <returns>A boolean value indicating if the request to delete a data source was successfully made or not.</returns>
        public Task<bool> DeleteDataSource(BasicDataSourceInfo info, bool isStaging)
        {
            return DeleteDataSource(info.AccessId, info.DataSourceName, info.MasterKey, isStaging);
        }

        /// <summary>
        /// Makes a request to delete a data source.
        /// </summary>
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>        
        /// <param name="masterKey">A Bing Maps key that has full control of the data source.</param>
        /// <exception cref="System.Exception">Thrown if invalid parameters are specified or there is an error making the request.</exception>
        /// <returns>A boolean value indicating if the request to delete a data source was successfully made or not.</returns>
        public Task<bool> DeleteDataSource(string accessId, string dataSourceName, string masterKey)
        {
            return DeleteDataSource(accessId, dataSourceName, masterKey, false);
        }

        /// <summary>
        /// Makes a request to delete a data source.
        /// </summary>
        /// <param name="accessId">A unique id used by the Bing Spatial Data Services to identify a data source.</param>
        /// <param name="dataSourceName">The name of a data source.</param>        
        /// <param name="masterKey">A Bing Maps key that has full control of the data source.</param>
        /// <param name="isStaging">A boolean indicating if the staging data source should be deleted or not.</param>
        /// <exception cref="System.Exception">Thrown if invalid parameters are specified or there is an error making the request.</exception>
        /// <returns>A boolean value indicating if the request to delete a data source was successfully made or not.</returns>
        public Task<bool> DeleteDataSource(string accessId, string dataSourceName, string masterKey, bool isStaging)
        {
            var tcs = new TaskCompletionSource<bool>();
            ValidateProperties(accessId, dataSourceName, masterKey);

            string requestUrl = string.Format("https://spatial.virtualearth.net/REST/v1/data/{0}/{1}?isStaging={2}&key={3}&clientApi={4}", accessId, dataSourceName, (isStaging) ? 1 : 0, masterKey, InternalSettings.ClientApi);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
            request.Method = "DELETE";

            request.BeginGetResponse((a) =>
            {
                try
                {
                    var r = (HttpWebRequest)a.AsyncState;
                    HttpWebResponse response = (HttpWebResponse)r.EndGetResponse(a);
                    tcs.SetResult(false);
                }
                catch
                {
                    tcs.SetResult(false);
                }
            }, request);

            return tcs.Task;
        }

        #endregion

        #endregion

        #region Private Methods

        private void ReportUploadStatus(string msg)
        {
            if (UploadStatusChanged != null)
            {
                UploadStatusChanged(msg);
            }
        }

        private void ValidateProperties(string accessId, string dataSourceName, string masterKey)
        {
            if (string.IsNullOrWhiteSpace(accessId))
            {
                throw new Exception("Access id not specified.");
            }

            if (string.IsNullOrWhiteSpace(dataSourceName))
            {
                throw new Exception("Data source name not specified.");
            }

            if (string.IsNullOrWhiteSpace(masterKey))
            {
                throw new Exception("Master key not specified.");
            }
        }

        private async Task<DataflowJob> MonitorStatus(Uri statusUrl)
        {
            DataflowResponse statusDetails;
            DataflowJob job = null;
            var  ser = new DataContractJsonSerializer(typeof(DataflowResponse));
            int failedMoinitorRequests = 0;

            do
            {
                try
                {
                    using (var s = await ServiceHelper.GetStreamAsync(statusUrl))
                    {
                        statusDetails = ser.ReadObject(s) as DataflowResponse;

                        if (statusDetails != null
                            && statusDetails.ResourceSets != null
                            && statusDetails.ResourceSets.Length > 0
                            && statusDetails.ResourceSets[0] != null
                            && statusDetails.ResourceSets[0].Resources != null
                            && statusDetails.ResourceSets[0].Resources.Length > 0
                            && statusDetails.ResourceSets[0].Resources[0] != null)
                        {
                            job = statusDetails.ResourceSets[0].Resources[0] as DataflowJob;
                            if (job.Status == "Aborted")
                            {
                                var sb = new StringBuilder();
                                sb.AppendLine("Job was aborted due to an error.");
                                if (statusDetails.ErrorDetails != null)
                                {
                                    foreach (var e in statusDetails.ErrorDetails)
                                    {
                                        sb.AppendLine(e);
                                    }
                                }

                                job.ErrorMessage = sb.ToString();
                            }
                        }

                        if (job == null || job.Status.Equals("Pending"))
                        {
                            await Task.Delay(_statusUpdateInterval);
                        }
                    }
                }
                catch
                {
                    failedMoinitorRequests++;

                    //If it failed to get the status more than 3 times then stop monitoring.
                    if (failedMoinitorRequests > 3)
                    {
                        job.Status = "Unknown - Lost connection.";
                        break;
                    }
                }
            }
            while (job != null && job.Status.Equals("Pending"));

            return job;
        }

        private async Task<List<DataflowJob>> DownloadDataflowJobs(string request)
        {
            using (var response = await ServiceHelper.GetStreamAsync(new Uri(request)))
            {
                var ser = new DataContractJsonSerializer(typeof(DataflowResponse));
                var r = ser.ReadObject(response) as DataflowResponse;

                if (r != null && r.ErrorDetails != null)
                {
                    var sb = new StringBuilder();
                    if (r.ErrorDetails != null)
                    {
                        foreach (var e in r.ErrorDetails)
                        {
                            sb.AppendLine(e);
                        }
                    }
                    throw new Exception(sb.ToString());
                }

                if (r != null &&
                    r.ResourceSets != null &&
                    r.ResourceSets.Length > 0 &&
                    r.ResourceSets[0].Resources != null &&
                    r.ResourceSets[0].Resources.Length > 0)
                {
                    var jobs = new List<DataflowJob>();

                    foreach (var res in r.ResourceSets[0].Resources)
                    {
                        if (res is DataflowJob)
                        {
                            jobs.Add(res as DataflowJob);
                        }
                        else
                        {
                            jobs.Add(new DataflowJob(res)); 
                        }
                    }

                    return jobs;
                }
            }

            return null;
        }

        private async Task<List<DataServiceJob>> DownloadDataServiceJobs(string request)
        {
            using (var response = await ServiceHelper.GetStreamAsync(new Uri(request)))
            {
                var ser = new DataContractJsonSerializer(typeof(DataflowResponse));
                var r = ser.ReadObject(response) as DataflowResponse;

                if (r != null && r.ErrorDetails != null)
                {
                    var sb = new StringBuilder();
                    if (r.ErrorDetails != null)
                    {
                        foreach (var e in r.ErrorDetails)
                        {
                            sb.AppendLine(e);
                        }
                    }
                    throw new Exception(sb.ToString());
                }

                if (r != null &&
                    r.ResourceSets != null &&
                    r.ResourceSets.Length > 0 &&
                    r.ResourceSets[0].Resources != null &&
                    r.ResourceSets[0].Resources.Length > 0)
                {
                    return new List<DataServiceJob>(r.ResourceSets[0].Resources);
                }
            }

            return null;
        }

        private async Task<List<DataSourceDetails>> DownloadDataSourceInfo(string requestUrl)
        {
            var dataSources = new List<DataSourceDetails>();

            using (var stream = await ServiceHelper.GetStreamAsync(new Uri(requestUrl)))
            {
                XDocument xmlDoc = XDocument.Load(stream);
                DateTime dt;
                bool b;
                XAttribute attr;

                foreach (XElement element in xmlDoc.Descendants(XmlNamespaces.App + "workspace"))
                {
                    DataSourceDetails ds = new DataSourceDetails();

                    if (element.HasAttributes)
                    {
                        attr = element.Attribute(XmlNamespaces.BingSpatialDataServices + "updated");
                        if (attr != null && DateTime.TryParse(attr.Value, out dt))
                        {
                            ds.Updated = dt;
                        }

                        attr = element.Attribute(XmlNamespaces.BingSpatialDataServices + "uploaded");
                        if (attr != null && DateTime.TryParse(attr.Value, out dt))
                        {
                            ds.Updated = dt;
                        }

                        attr = element.Attribute(XmlNamespaces.BingSpatialDataServices + "isPublic");
                        if (attr != null && bool.TryParse(attr.Value, out b))
                        {
                            ds.IsPublic = b;
                        }

                        attr = element.Attribute(XmlNamespaces.BingSpatialDataServices + "isActive");
                        if (attr != null && bool.TryParse(attr.Value, out b))
                        {
                            ds.IsActive = b;
                        }

                        attr = element.Attribute(XmlNamespaces.BingSpatialDataServices + "isStagingDataSource");
                        if (attr != null && bool.TryParse(attr.Value, out b))
                        {
                            ds.IsStaging = b;
                        }
                    }

                    ds.DataSourceName = element.Element(XmlNamespaces.Atom + "title").Value;

                    var jobId =  element.Element(XmlNamespaces.Atom + "jobId");
                    if(jobId != null){
                        ds.JobId =jobId.Value;
                    }

                    var disclaimer = element.Element(XmlNamespaces.Atom + "disclaimer");
                    if (disclaimer != null)
                    {
                        ds.Disclaimer = disclaimer.Value;
                    }

                    var description = element.Element(XmlNamespaces.Atom + "description");
                    if (description != null)
                    {
                        ds.Description = description.Value;
                    }

                    XElement xCollection = element.Element(XmlNamespaces.App + "collection");

                    if (xCollection != null)
                    {
                        if (xCollection.HasAttributes)
                        {
                            ds.QueryUrl = xCollection.Attribute(XmlNamespaces.App + "href").Value;
                        }

                        ds.EntityTypeName = xCollection.Element(XmlNamespaces.Atom + "title").Value;
                    }

                    dataSources.Add(ds);
                }
            }

            return dataSources;
        }

        private Task<string> CreateDownloadJob(string accessId, string dataSourceName, string masterKey)
        {
            var tcs = new TaskCompletionSource<string>();

            //Build the HTTP URI that will upload and create the geocode dataflow job
            string url = string.Format("https://spatial.virtualearth.net/REST/v1/Dataflows/DataSourceDownload/{0}/{1}?output=json&key={2}&clientApi={3}",
                accessId,
                dataSourceName,
                Uri.EscapeUriString(masterKey), InternalSettings.ClientApi);

            Uri createJobUri = new Uri(url);

            //Include the data to geocode in the HTTP request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(createJobUri);
            request.Method = "GET";

            request.BeginGetResponse((a) =>
            {
                try
                {
                    var r = (HttpWebRequest)a.AsyncState;
                    HttpWebResponse response = (HttpWebResponse)r.EndGetResponse(a);

                    string dataflowJobLocation = string.Empty;
                    foreach (var hKey in response.Headers.AllKeys)
                    {
                        if (string.Compare(hKey, "Location", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            dataflowJobLocation = response.Headers[hKey];
                            break;
                        }
                    }

                    tcs.SetResult(dataflowJobLocation);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }, request);

            return tcs.Task;
        }

        private Task<string> CreateUploadJob(string createJobUri, DataSource dataSource, bool skipEmptyLocations)
        {
            var tcs = new TaskCompletionSource<string>();

            //Include the data to geocode in the HTTP request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(createJobUri);

            // The HTTP method must be 'POST'.
            request.Method = "POST";
            //request.ContentType = "application/xml";
            request.ContentType = "application/octet-stream";
            request.ContinueTimeout = 1800000;

            request.BeginGetRequestStream(async (a) =>
            {
                var r = (HttpWebRequest)a.AsyncState;

                using (var postStream = request.EndGetRequestStream(a))
                {
                    //await dataSource.WriteAsync(postStream, DataSourceFormat.XML, skipEmptyLocations);

                    using (var zipStream = new GZipStream(postStream, CompressionMode.Compress))
                    {
                        await dataSource.WriteAsync(zipStream, DataSourceFormat.XML, skipEmptyLocations);
                    }
                }
                
                request.BeginGetResponse((a2) =>
                {
                    try
                    {
                        var r2 = (HttpWebRequest)a2.AsyncState;
                        var response = (HttpWebResponse)r2.EndGetResponse(a2);

                        string dataflowJobLocation = string.Empty;
                        foreach (var hKey in response.Headers.AllKeys)
                        {
                            if (string.Compare(hKey, "Location", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                dataflowJobLocation = response.Headers[hKey];
                                break;
                            }
                        }

                        tcs.SetResult(dataflowJobLocation);
                    }
                    catch (WebException ex)
                    {
                        if (ex.Response != null)
                        {
                            var ser = new DataContractJsonSerializer(typeof(DataflowResponse));
                            var res = ser.ReadObject(ex.Response.GetResponseStream()) as DataflowResponse;

                            if (res.ErrorDetails != null && res.ErrorDetails.Length > 0)
                            {
                                tcs.SetException(new Exception(string.Join("\r\n", res.ErrorDetails)));
                                return;
                            }
                        }

                        tcs.SetException(ex);
                    }
                }, request);
            }, request);            

            return tcs.Task;
        }

        private Task<string> CreateUploadJob(string createJobUri, Stream dataSourceStream, DataSourceFormat format)
        {
            var tcs = new TaskCompletionSource<string>();

            //Include the data to geocode in the HTTP request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(createJobUri);

            // The HTTP method must be 'POST'.
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            request.ContinueTimeout = 1800000;

            request.BeginGetRequestStream((a) =>
            {
                var r = (HttpWebRequest)a.AsyncState;

                using (var postStream = request.EndGetRequestStream(a))
                {
                    if (format == DataSourceFormat.SHP || XmlUtilities.IsStreamCompressed(dataSourceStream))
                    {
                        dataSourceStream.CopyTo(postStream);
                    }
                    else
                    {
                        using (var zipStream = new GZipStream(postStream, CompressionMode.Compress))
                        {
                            dataSourceStream.CopyTo(zipStream);
                        }
                    }
                }

                request.BeginGetResponse((a2) =>
                {
                    try
                    {
                        var r2 = (HttpWebRequest)a2.AsyncState;
                        var response = (HttpWebResponse)r2.EndGetResponse(a2);

                        string dataflowJobLocation = string.Empty;
                        foreach (var hKey in response.Headers.AllKeys)
                        {
                            if (string.Compare(hKey, "Location", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                dataflowJobLocation = response.Headers[hKey];
                                break;
                            }
                        }
                        
                        tcs.SetResult(dataflowJobLocation);
                    }
                    catch (WebException ex)
                    {
                        if (ex.Response != null)
                        {
                            var ser = new DataContractJsonSerializer(typeof(DataflowResponse));
                            var res = ser.ReadObject(ex.Response.GetResponseStream()) as DataflowResponse;

                            if (res.ErrorDetails != null && res.ErrorDetails.Length > 0)
                            {
                                tcs.SetException(new Exception(string.Join("\r\n", res.ErrorDetails)));
                                return;
                            }
                        }
                        
                        tcs.SetException(ex);
                    }
                }, request);
            }, request);

            return tcs.Task;
        }
        
        #endregion
    }
}

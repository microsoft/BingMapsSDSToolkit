
using System;
using System.Text.RegularExpressions;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// Basic information about a data source
    /// </summary>
    public class BasicDataSourceInfo 
    {
        #region Private Properties

        private string accessId, name, entityType, queryUrl;

        #endregion

        #region Public Properties

        /// <summary>
        /// Name fo the Data Source.
        /// </summary>
        public string DataSourceName
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !IsValidName(value))
                {
                    throw new ArgumentException("Invalid data source name specified.");
                }

                name = value;
                CreateQueryUrl();
            }
        }

        /// <summary>
        /// Name of the Entity type of the data source.
        /// </summary>
        public string EntityTypeName
        {
            get { return entityType; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !IsValidName(value))
                {
                    throw new ArgumentException("Invalid entity type name specified.");
                }

                entityType = value;
                CreateQueryUrl();
            }
        }

        /// <summary>
        /// The access ID for the data source.
        /// </summary>
        public string AccessId
        {
            get { return accessId; }
            set
            {
                accessId = value;
                CreateQueryUrl();
            }
        }

        /// <summary>
        /// The query URL for accessing the data source.
        /// </summary>
        public string QueryURL
        {
            get
            {
                return queryUrl;
            }
            set
            {
                queryUrl = value;
                ParseQueryUrl();
            }
        }

        /// <summary>
        /// The master Bing Maps key for editting a data source.
        /// </summary>
        public string MasterKey { get; set; }

        /// <summary>
        /// The query Bing Maps key for querying a data source.
        /// </summary>
        public string QueryKey { get; set; }

        /// <summary>
        /// A description of the data source data.
        /// </summary>
        public string Description { get; set; }

        #endregion
        
        #region Private Methods

        private void CreateQueryUrl()
        {
            if (!string.IsNullOrWhiteSpace(DataSourceName) && !string.IsNullOrWhiteSpace(AccessId) && !string.IsNullOrWhiteSpace(EntityTypeName))
            {
                queryUrl = string.Format("https://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}", AccessId, DataSourceName, EntityTypeName);
            }
        }

        private void ParseQueryUrl()
        {
            if (!string.IsNullOrWhiteSpace(queryUrl))
            {
                int minIdx = queryUrl.IndexOf("/data/") + 6;
                int maxIdx = minIdx;

                if (minIdx > -1 && queryUrl.Length > minIdx)
                {                    
                    maxIdx = queryUrl.IndexOf('/', minIdx);
                    accessId = queryUrl.Substring(minIdx, maxIdx - minIdx);

                    minIdx = maxIdx + 1;
                }

                if (minIdx > -1 && queryUrl.Length > minIdx)
                {
                    maxIdx = queryUrl.IndexOf('/', minIdx);
                    name = queryUrl.Substring(minIdx, maxIdx - minIdx);

                    minIdx = maxIdx + 1;
                }

                if(minIdx > -1 && queryUrl.Length > minIdx){
                    maxIdx = queryUrl.IndexOf('/', minIdx);

                    if (maxIdx > minIdx)
                    {
                        entityType = queryUrl.Substring(minIdx, maxIdx - minIdx);
                    }
                    else
                    {
                        entityType = queryUrl.Substring(minIdx);
                    }
                }
            }
        }

        //A data source name can have up to 50 characters and can contain alphanumeric characters and any of the following:
        // ~ ` ! $ ^ _ = { }       
        private bool IsValidName(string name)
        {
            var nameRegex = new Regex("^[-a-zA-Z0-9~`!$^_={}]+$");
            return (name.Length <= 50 && nameRegex.IsMatch(name));
        }

        #endregion
    }
}

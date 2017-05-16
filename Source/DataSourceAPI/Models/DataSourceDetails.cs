using System;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object that contains a bunch of information about a data source.
    /// </summary>
    public class DataSourceDetails : BasicDataSourceInfo
    {
        #region Public Properties

        /// <summary>
        /// The Job Id from when the data source was uploaded.
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// A disclaimer that indicates if the data source is owned or managed by Microsoft or not.
        /// </summary>
        public string Disclaimer { get; set; }

        /// <summary>
        /// The date and time the data source was uploaded or last updated.
        /// </summary>
        public DateTime Updated { get; set; }
        
        /// <summary>
        /// A boolean indicating if the data source is public or not.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// A boolean indicating if the data source is in the staging environment or not.
        /// </summary>
        public bool IsStaging { get; set; }

        /// <summary>
        /// A boolean indicating if the data source is active or not. 
        /// Non-active data sources are earlier versions of the data source.
        /// </summary>
        public bool IsActive { get; set; }
        
        #endregion

        #region Public Methods

        public override int GetHashCode()
        {
            int hash = 0;

            if (!string.IsNullOrWhiteSpace(AccessId))
            {
                hash ^= AccessId.GetHashCode();
            }

            if (!string.IsNullOrWhiteSpace(DataSourceName))
            {
                hash ^= DataSourceName.GetHashCode();
            }

            if (!string.IsNullOrWhiteSpace(EntityTypeName))
            {
                hash ^= EntityTypeName.GetHashCode();
            }

            if (!string.IsNullOrWhiteSpace(JobId))
            {
                hash ^= JobId.GetHashCode();
            }

            hash ^= IsActive.GetHashCode();
            hash ^= IsPublic.GetHashCode();
            hash ^= IsStaging.GetHashCode();

            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is DataSourceDetails)
            {
                return base.GetHashCode() == (obj as DataSourceDetails).GetHashCode();
            }

            return false;
        }

        public static bool operator ==(DataSourceDetails x, DataSourceDetails y)
        {
            return x.GetHashCode() == y.GetHashCode();
        }

        public static bool operator !=(DataSourceDetails x, DataSourceDetails y)
        {
            return !(x == y);
        }

        #endregion
    }
}

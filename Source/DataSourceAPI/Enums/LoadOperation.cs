
namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// The type of operation to perform when uploading a data source.
    /// </summary>
    public enum LoadOperation
    {
        /// <summary>
        /// Publishes entity data to a new data source or overwrites an existing data source. 
        /// </summary>
        Complete,

        /// <summary>
        /// Performs the same operation as "complete" except that the data source data is staged and not published. 
        /// Use the query URLs with the isStaged parameter set to 1 (true) to query the staged data source. 
        /// Staged data sources that are not published are deleted after 30 days.
        /// </summary>
        CompleteStaging,

        /// <summary>
        /// Edit, add and delete entities in an existing data source. If you want to delete entities, you must add a 
        /// property named __deleteEntity to the schema and set it to 1 or true for each entity that you want to remove.
        /// </summary>
        Incremental,

        /// <summary>
        /// Performs the same operation as "incremental" except that the data source update is staged and not published. 
        /// Use the query URLs with the isStaged parameter set to 1 or true to query the staged data source. 
        /// Staged data sources that are not published are deleted after 30 days.
        /// </summary>
        IncrementalStaging
    }
}

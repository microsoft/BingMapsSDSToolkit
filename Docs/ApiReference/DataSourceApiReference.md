# Data Source API Reference

**Namespace:** `BingMapsSDSToolkit.DataSourceAPI`

This API provides the ability to upload and manage data sources in the Bing Spatial Data Services.

[MSDN API Documentation](https://msdn.microsoft.com/en-us/library/gg585132.aspx)

## ColumnHeader Class

Data source column header information.

### Constructor

> `ColumnHeader(String name)` 

> `ColumnHeader(String name, Type type)` 

> `ColumnHeader(String name, Type type, Boolean isPrimaryKey)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| IsPrimaryKey | Boolean | A boolean indicating if the column is a primary key or not. |
| Name | String | The name of the column. |
| Type | String | The type of data in the column. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetEdmTypeName() | String | Gets the Edm type name of the column. |
| GetXmlTypeName() | String | Gets the XML type name of the column. |
| ToString() | String | A string representing a ColumnHeader in the format Name(EdmTypeName) or Name(EdmTypeName,primaryKey) |

## DataflowJob Class

An object that represents a single data flow job.

### Constructor

> `DataflowJob()` 

> `DataflowJob(DataServiceJob job)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| FailedEntityCount | Int32 | The number of entities that did not process successfully because of an error. |
| ProcessedEntityCount | Int32 | The number of entities that were processed. This number included entities that were processed successfully and those that failed. If the field is set to 0, the number of processed entries is not known. |
| TotalEntityCount | Int32 | The total number of entities that were uploaded. |

## DataflowResponse Class

A response object from the Bing Spatial Data Services when making job requests.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| AuthenticationResultCode | String | A status code that offers additional information about authentication success or failure. |
| BrandLogoUri | String | A URL that references a brand image to support contractual branding requirements. |
| Copyright | String | A copyright notice. |
| ErrorDetails | String\[\] | A collection of error descriptions. |
| ResourceSets | DataflowResultSet\[\] | A collection of DataflowResourceSet objects. A DataflowResourceSet is a container of DataflowJob's returned by the request. |
| StatusCode | Int32 | The HTTP Status code for the request. |
| StatusDescription | String | A description of the HTTP status code. |
| TraceId | String | A unique identifier for the request. |

## DataflowResultSet Class

A collection of DataflowJob's

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| EstimatedTotal | Int64 | An estimate of the total number of resources in the ResourceSet. |
| Resources | DataServiceJob\[\] | A collection of one or more DataflowJob resources. |

## DataServiceJob Class

An object that contains the information of a DataServiceJob.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| CompletedDate | String | The date and time that the dataflow job is completed. If the Status field is set to Pending, the CompletedDate field is not shown or is empty. |
| CreatedDate | String | The date and time that the dataflow job was created. |
| Description | String | A user-defined description of the dataflow job. If a description is not specified when the workflow is created, this field is not included or the value is null. |
| ErrorMessage | String | A string containing an error message if an error occurs while processing the job. |
| Id | String | A unique string that identifies the dataflow job. There are no requirements for the string format. |
| Links | Link\[\] | URLs that is defined by its role and name attributes. <br/>- "role":"self": Use to check the status of your job. This URL is provided in the response when you create a load data source job.<br/> - "role":"self" and "name":"datasource": Use to query and update a data source. This URL appears in the response to a job status request when the load data source job completes.<br/>- "role":"output" and "name":"failed": Use to access the error log for the job. This URL appears in the response only when the job status is set to Aborted. |
| Status | String | The status of the dataflow job. Possible values are 'Pending', 'Aborted', or 'Completed'. |
| Type | String | Type name of the data service or flow job. |

<a name="DataSource"></a>
## DataSource Class

A data source object used for storing a data set. Can be uploaded, downloaded and converted into various file formats.

### Constructor

> `DataSource()` 

> `DataSource(BasicDataSourceInfo info)` 

> `DataSource(String name, String entityTypeName)` 

> `DataSource(String accessId, String name, String entityTypeName)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| ColumnHeaders | List\<ColumnHeader\> | The column header information for the data source. |
| Info | BasicDataSourceInfo | Basic information about the data source. |
| Rows | List\<List\<object\>\> | The rows of data in the data source. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| Geocode(String bingMapsKey) | Task\<DataSourceGeocodeResults\> | Batch geocodes all the data in a data source and updates it accordingly. Only Geocodes rows that do not have Latitude, Longitude, or a Geography value specified. Optimizes batch geocoding by grouping rows with identical addesses together. |
| Geocode(String bingMapsKey, String culture) | Task\<DataSourceGeocodeResults\> | Batch geocodes all the data in a data source and updates it accordingly. Only Geocodes rows that do not have Latitude, Longitude, or a Geography value specified. Optimizes batch geocoding by grouping rows with identical addesses together. |
| GetColumnNameIndex(String name) | Int32 | Gets the column header index by column Name. Returns -1 if column does not exist. Search is case sensitive. |
| GetColumnNameIndex(String name, Boolean ignoreCase) | Int32 | Gets the column header index by column Name. Returns -1 if column does not exist. |
| GetColumnTypeIndex(Type type) | List\<int\> | Gets a list of column header indices by column type. |
| GetPrimaryKeyColumnIndex() | Int32 | Gets the index of the primary key column. If no primary key specified it will return -1. |
| GetRowByPrimaryKeyValue(String keyValue) | List\<object\> | Searches through all rows in the data source for a row that has the specified primary key value specified.  If no row found, or no primary key specified in data source, null will be returned. |
| ReadAsync(Stream stream, DataSourceFormat dataSourceFormat) | Task\<bool\> | Parses a stream of a Bing Maps data source file and populates the data source. |
| Validate() | Task\<ValidationResult\> | Scans a data source to verify that it is valid. If Latitude, Longitude or primary key columns do not exist, they will be added. |
| WriteAsync(Stream outputStream, DataSourceFormat dataSourceFormat) | Task\<bool\> | Writes the data source to a stream as a formatted Bing Maps data source. |
| WriteAsync(Stream outputStream, DataSourceFormat dataSourceFormat, Boolean skipEmptyLocations) | Task\<bool\> | Writes the data source to a stream as a formatted Bing Maps data source. |

### Events

| Name | Type | Description |
| ---- | ---- | ----------- |
| GeocodeStatusChanged | `(string x) => {}` | An event that provides update messages during the batch geocode process of the data source. |

## DataSourceDetails Class

An object that contains a bunch of information about a data source.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Disclaimer | String | A disclaimer that indicates if the data source is owned or managed by Microsoft or not. |
| IsActive | Boolean | A boolean indicating if the data source is active or not.  Non-active data sources are earlier versions of the data source. |
| IsPublic | Boolean | A boolean indicating if the data source is public or not. |
| IsStaging | Boolean | A boolean indicating if the data source is in the staging environment or not. |
| JobId | String | The Job Id from when the data source was uploaded. |
| Updated | DateTime | The date and time the data source was uploaded or last updated. |

## DataSourceFormat Enumerator

The data format of a data source. Note data sources should follow the documented file schema.

| Name | Description |
| ---- | ----------- |
| CSV | Comma seperated (comma delimited) file format. |
| KML | A file in KML or KMZ format. |
| PIPE | Pipe (\|) delimited file format. |
| SHP | A file in the ESRI Shapefile format. |
| TAB | Tab delimited file format. |
| XML | XML file that matches the schema required for Bing Spatial Data Services data sources. |

## DataSourceGeocodeResults Class

An object for storing the results of geocoding a data source.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Error | String | Error during batch geocoding process. |
| Failed | Int32 | Number of rows that failed to geocode. |
| FailedRows | List\<string\> | A list of all the primary keys, of the rows that faile to geocode. |
| Succeeded | Int32 | Number of rows that geocoded successfully. |

<a name="DataSourceManager"></a>
## DataSourceManager Class

A tool for managing data sources and data flow jobs in the Bing Saptial Data Services.

### Constructor

> `DataSourceManager()` 

> `DataSourceManager(int statusUpdateInterval)` 

* `statusUpdateInterval` - Interval used to check the status of the batch job in ms.

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| DeleteDataSource(BasicDataSourceInfo info) | Task\<bool\> | Makes a request to delete a data source. Requires Access ID, Data Source Name and Master Key. |
| DeleteDataSource(BasicDataSourceInfo info, Boolean isStaging) | Task\<bool\> | Makes a request to delete a data source. Requires Access ID, Data Source Name and Master Key. |
| DeleteDataSource(String accessId, String dataSourceName, String masterKey) | Task\<bool\> | Makes a request to delete a data source. |
| DeleteDataSource(String accessId, String dataSourceName, String masterKey, Boolean isStaging) | Task\<bool\> | Makes a request to delete a data source. |
| Download(BasicDataSourceInfo info) | Task\<DataSource\> | Downloads a data source. Requires Access ID, Data Source Name and Master Key. |
| Download(String accessId, String dataSourceName, String masterKey) | Task\<DataSource\>  | Downloads a data source. |
| GetAllPublicDataSources(String bingMapsKey) | Task\<List\<DataSourceDetails\>\> | Gets a list of all the data sources in the Bing Spatial Data Services that are marked as public. |
| GetDetails(BasicDataSourceInfo info) | Task\<List\<DataSourceDetails\>\> | Gets details for a specific data sources in a Bing Maps account.  Requires Access ID, Data Source Name and Master or Query Key. |
| GetDetails(BasicDataSourceInfo info, Boolean showAllVersions) | Task\<List\<DataSourceDetails\>\> | Gets details for a specific data sources in a Bing Maps account.  Requires Access ID, Data Source Name and Master or Query Key. |
| GetDetails(String bingMapsKey) | Task\<List\<DataSourceDetails\>\> | Gets details for all data sources in a Bing Maps account. Such as the date and time that each data source was last updated,  the data source and entity type names of the data sources, and if they are public or not. |
| GetDetails(String bingMapsKey, String accessId, String dataSourceName) | Task\<List\<DataSourceDetails\>\> | Gets details for a specific data sources in a Bing Maps account. |
| GetDetails(String bingMapsKey, String accessId, String dataSourceName, Boolean showAllVersions) | Task\<List\<DataSourceDetails\>\> | Gets details for a specific data sources in a Bing Maps account.  If the access id and data source name is specified details for a specific data sources in a Bing Maps account.  Otherwise details for all data sources in a Bing Maps account are return. |
| GetJobList(String bingMapsKey) | Task\<List\<DataflowJob\>\> | Gets all the recent Bing Spatial Data Services data flow jobs for a Bing Maps account. |
| PublishStagedDataSource(BasicDataSourceInfo info) | Task\<DataServiceJob\> | Publishes a staged data source to production. Requires Access ID, Data Source Name and Master Key. |
| PublishStagedDataSource(String accessId, String dataSourceName, String masterKey) | Task\<DataServiceJob\> | Publishes a staged data source to production. |
| RollbackDataSource(String jobId, String dataSourceName, String masterKey) | Task\<DataflowJob\> | Rolls back a data source to an earlier version. |
| SetPublicSetting(BasicDataSourceInfo info, Boolean makePublic) | Task\<DataServiceJob\> | Sets the data source public setting as either public or non-public. Requires Access ID, Data Source Name and Master Key. |
| SetPublicSetting(String accessId, String dataSourceName, String masterKey, Boolean makePublic) | Task\<DataServiceJob\> | Sets the data source public setting as either public or non-public. |
| Upload(DataSource dataSource, LoadOperation loadOperation) | Task\<DataflowJob\> | Uploads a data source to the Bing Spatial Data Services. Skips rows that have no location information. |
| Upload(DataSource dataSource, LoadOperation loadOperation, Boolean setPublic) | Task\<DataflowJob\> | Uploads a data source to the Bing Spatial Data Services. Skips rows that have no location information. |
| Upload(DataSource dataSource, LoadOperation loadOperation, Boolean setPublic, Boolean skipEmptyLocations) | Task\<DataflowJob\> | Uploads a data source to the Bing Spatial Data Services. |
| Upload(Stream dataSourceStream, DataSourceFormat format, LoadOperation loadOperation, BasicDataSourceInfo info, Boolean setPublic) | Task\<DataflowJob\> | Uploads KML and SHP files streams as a data source. |

### Events

| Name | Type | Description |
| ---- | ---- | ----------- |
| UploadStatusChanged | `(string x) => {}` | An event that provides update messages during the upload process. |

## Link Class

A data source job link.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Name | String | The name of the link. |
| Role | String | The role of the link. |
| URL | String | The URL of the link. |

## LoadOperation Enumerator

The type of operation to perform when uploading a data source.

| Name | Description |
| ---- | ----------- |
| Complete | Publishes entity data to a new data source or overwrites an existing data source. |
| CompleteStaging | Performs the same operation as "complete" except that the data source data is staged and not published.  Use the query URLs with the isStaged parameter set to 1 (true) to query the staged data source.  Staged data sources that are not published are deleted after 30 days. |
| Incremental | Edit, add and delete entities in an existing data source. If you want to delete entities, you must add a  property named __deleteEntity to the schema and set it to 1 or true for each entity that you want to remove. |
| IncrementalStaging | Performs the same operation as "incremental" except that the data source update is staged and not published.  Use the query URLs with the isStaged parameter set to 1 or true to query the staged data source.  Staged data sources that are not published are deleted after 30 days. |

## ValidationResult Class

An object used to store the results of validating a data source.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| AllRowsHaveLocationInfo | Boolean | A boolean value indicating if all rows contain valid location data, either a Latitude/Longitude pair, or a Geography object. |
| Errors | List\<string\> | A list of error messages. This should be empty for valid data sources. |
| Warnings | List\<string\> | A list of warning messages that may be an issue if uploading a data source. i.e. More than 200,000 rows. Or not Latitue/Longitude values specified. |

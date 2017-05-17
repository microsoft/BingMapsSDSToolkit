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

using BingMapsSDSToolkit.GeocodeDataflowAPI;
using BingMapsSDSToolkit.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// A data source object used for storing a data set. Can be uploaded, downloaded and converted into various file formats.
    /// </summary>
    public class DataSource
    {
        #region Private Properties

        private BasicDataSourceInfo _info = new BasicDataSourceInfo(){
            DataSourceName = "EntityDataSet",
            EntityTypeName = "Entity"
        };

        private string _defaultPrimaryKeyName = "EntityID";

        private const string _schemaText = "Bing Spatial Data Services, 1.0, {0}";
        private const string _columnHeaderPattern = @"^([a-zA-Z0-9_]+)\(([a-zA-Z0-9\.,]+)\)$";

        private const int _maxRows = 200000;
        private const int _maxGeomPoints = 100000;
        private const int _maxStringLength = 2560;

        //Includes lat/lon
        private const int _maxColumns = 352;

        private Regex coordinatePairRegex = new Regex("(-?[0-9]+.?[0-9]* -?[0-9]+.?[0-9]*)");

        private const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";
        private const string XmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";
        private const string MsDataNamespace = "urn:schemas-microsoft-com:xml-msdata";

        private XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
        {
            Encoding = System.Text.Encoding.UTF8,
            Indent = true,
            NamespaceHandling = NamespaceHandling.OmitDuplicates
        };

        private List<ColumnHeader> _columnHeaders;
        private List<List<object>> _rows;

        #endregion

        #region Constructor

        /// <summary>
        /// A data source object used for storing a data set. Can be uploaded, downloaded and converted into various file formats.
        /// </summary>
        public DataSource()
        {
            _columnHeaders = new List<ColumnHeader>();
            _rows = new List<List<object>>();
        }

        /// <summary>
        /// A data source object used for storing a data set. Can be uploaded, downloaded and converted into various file formats.
        /// </summary>
        /// <param name="name">The name of the data source.</param>
        /// <param name="entityTypeName">The entity type name of the data source.</param>
        public DataSource(string name, string entityTypeName)
        {
            _columnHeaders = new List<ColumnHeader>();
            _rows = new List<List<object>>();

            _info.DataSourceName = name;
            _info.EntityTypeName = entityTypeName;
        }

        /// <summary>
        /// A data source object used for storing a data set. Can be uploaded, downloaded and converted into various file formats.
        /// </summary>
        /// <param name="accessId">The accessId of the data source.</param>
        /// <param name="name">The name of the data source.</param>
        /// <param name="entityTypeName">The entity type name of the data source.</param>
        public DataSource(string accessId, string name, string entityTypeName)
        {
            _columnHeaders = new List<ColumnHeader>();
            _rows = new List<List<object>>();

            _info.AccessId = accessId;
            _info.DataSourceName = name;
            _info.EntityTypeName = entityTypeName;
        }

        /// <summary>
        /// A data source object used for storing a data set. Can be uploaded, downloaded and converted into various file formats.
        /// </summary>
        /// <param name="info">Basic information about the data source.</param>
        public DataSource(BasicDataSourceInfo info)
        {
            _columnHeaders = new List<ColumnHeader>();
            _rows = new List<List<object>>();
            _info = info;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Basic information about the data source.
        /// </summary>
        public BasicDataSourceInfo Info
        {
            get { return _info; }
        }

        /// <summary>
        /// The column header information for the data source.
        /// </summary>
        public List<ColumnHeader> ColumnHeaders
        {
            get { return _columnHeaders;  }
        }

        /// <summary>
        /// The rows of data in the data source.
        /// </summary>
        public List<List<object>> Rows
        {
            get { return _rows; }
        }

        /// <summary>
        /// An event that provides update messages during the batch geocode process of the data source.
        /// </summary>
        public Action<string> GeocodeStatusChanged;

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses a stream of a Bing Maps data source file and populates the data source.
        /// </summary>
        /// <param name="stream">The file stream of a Bing Maps data source file.</param>
        /// <param name="dataSourceFormat">The data source format.</param>
        /// <returns>A boolean value indicating if it was able to parse the file stream.</returns>
        public Task<bool> ReadAsync(Stream stream, DataSourceFormat dataSourceFormat)
        {
            return Task.Run<bool>(() =>
            {
                var ms = new MemoryStream();

                try
                {
                    stream.CopyTo(ms);
                    if (XmlUtilities.IsStreamCompressed(ms))
                    {
                        //Uncompress the first file in the zipped stream.
                        var zipArchive = new System.IO.Compression.ZipArchive(ms);
                        if (zipArchive.Entries != null && zipArchive.Entries.Count > 0)
                        {
                            stream = zipArchive.Entries[0].Open();

                            ms = new MemoryStream();
                            stream.CopyTo(ms);
                        }
                    }

                    ms.Position = 0;

                    if (dataSourceFormat == DataSourceFormat.XML)
                    {
                        var doc = XDocument.Load(ms);
                        ParseDataSource(doc);
                    }
                    else if (dataSourceFormat == DataSourceFormat.KML || dataSourceFormat == DataSourceFormat.SHP)
                    {
                        throw new Exception("Unsupported file format for reading.");
                    }
                    else
                    {
                        using (var reader = new StreamReader(ms))
                        {
                            ParseDataSource(reader, dataSourceFormat);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                ms.Dispose();

                return true;
            });
        }

        /// <summary>
        /// Writes the data source to a stream as a formatted Bing Maps data source.
        /// </summary>
        /// <param name="outputStream">The output stream to write the data source to.</param>
        /// <param name="dataSourceFormat">The data source format.</param>
        /// <returns>A boolean value indicating if it was able to write data source successfully.</returns>
        public Task<bool> WriteAsync(Stream outputStream, DataSourceFormat dataSourceFormat)
        {
            return WriteAsync(outputStream, dataSourceFormat, false);
        }

        /// <summary>
        /// Writes the data source to a stream as a formatted Bing Maps data source.
        /// </summary>
        /// <param name="outputStream">The output stream to write the data source to.</param>
        /// <param name="dataSourceFormat">The data source format.</param>
        /// <param name="skipEmptyLocations">A boolean value indicating if rows that don't have locaiton information (latitude, longitude, or geography column) should be written or not.</param>
        /// <returns>A boolean value indicating if it was able to write data source successfully.</returns>
        public Task<bool> WriteAsync(Stream outputStream, DataSourceFormat dataSourceFormat, bool skipEmptyLocations)
        {
            return Task.Run<bool>(() =>
            {
                try
                {
                    if (dataSourceFormat == DataSourceFormat.KML || dataSourceFormat == DataSourceFormat.SHP)
                    {
                        throw new Exception("File format not supported for writing");
                    }
                    else if (dataSourceFormat == DataSourceFormat.XML)
                    {
                        using (var writer = XmlWriter.Create(outputStream, xmlWriterSettings))
                        {
                            Write(writer, skipEmptyLocations);
                        }
                    }
                    else
                    {
                        WriteDelimitedDataSource(outputStream, dataSourceFormat, skipEmptyLocations);
                    }
                }
                catch
                {
                    return false;
                }

                return true;
            });
        }

        /// <summary>
        /// Scans a data source to verify that it is valid. If Latitude, Longitude or primary key columns do not exist, they will be added.
        /// </summary>
        /// <returns>An object containing errors, warnings, and information on if all rows contain location information.</returns>
        public Task<ValidationResult> Validate()
        {
            return Task.Run<ValidationResult>(() =>
            {
                var results = new ValidationResult();

                if (_columnHeaders == null)
                {
                    results.Errors.Add("Column header can not be null.");
                }

                if (string.IsNullOrWhiteSpace(_info.DataSourceName))
                {
                    results.Errors.Add("Data source name is not specified.");
                }

                if (string.IsNullOrWhiteSpace(_info.EntityTypeName))
                {
                    results.Errors.Add("Entity type name is not specified.");
                }

                bool primaryKeyAdded = false;
                var colIdx = GetPrimaryKeyColumnIndex();
                if (colIdx == -1)
                {
                    colIdx = GetColumnNameIndex(_defaultPrimaryKeyName);
                    if (colIdx == -1)
                    {
                        _columnHeaders.Add(new ColumnHeader(_defaultPrimaryKeyName, typeof(string)));
                        results.Warnings.Add("Primary key column is not defined in column header. Column added.");
                        primaryKeyAdded = true;
                    }
                    else
                    {
                        results.Errors.Add("Primary key column is not defined in column header.");
                    }
                }

                bool latColAdded = false;
                colIdx = GetColumnNameIndex("Latitude");
                if (colIdx == -1)
                {
                    results.Warnings.Add("'Latitude' column is not defined in column header. Column added.");
                    _columnHeaders.Add(new ColumnHeader("Latitude", typeof(double)));
                    latColAdded = true;
                }

                bool lonColAdded = false;
                colIdx = GetColumnNameIndex("Longitude");
                if (colIdx == -1)
                {
                    results.Warnings.Add("'Longitude' column is not defined in column header. Column added.");
                    _columnHeaders.Add(new ColumnHeader("Longitude", typeof(double)));
                    lonColAdded = true;
                }

                if (_columnHeaders != null)
                {
                    var counts = _columnHeaders.GroupBy(x => x.Name).Select(a => new { Name = a.Key, Count = a.Count() });

                    foreach (var c in counts)
                    {
                        if (c.Count > 1)
                        {
                            results.Errors.Add("Column '" + c.Name + "' is defined more than once in the column header.");
                        }
                    }

                    if (_columnHeaders.Count > _maxColumns)
                    {
                        results.Errors.Add("Data source contains more than " + string.Format("{0}", _maxColumns) + " columns.");
                    }
                }

                if (_rows == null || _rows.Count == 0)
                {
                    results.Warnings.Add("Data source contains no rows.");
                }

                if (_rows != null && _rows.Count > 0)
                {
                    if (_rows.Count > _maxRows)
                    {
                        results.Warnings.Add("Data source contains more than " + string.Format("{0}", _maxRows) + " rows.");
                    }

                    if (_columnHeaders != null)
                    {
                        int numCols, headerNumCols = _columnHeaders.Count;
                        Type type;
                        List<object> row;
                        string colName;

                        for (int r = 0; r < _rows.Count; r++)
                        {
                            if (primaryKeyAdded)
                            {
                                _rows[r].Add(r.ToString());
                            }

                            if (latColAdded)
                            {
                                _rows[r].Add(null);
                            }

                            if (lonColAdded)
                            {
                                _rows[r].Add(null);
                            }

                            numCols = _rows[r].Count;
                            if (numCols != headerNumCols)
                            {
                                results.Errors.Add("Row: " + r + " - does not contain the same number of columns as the header.");
                            }
                            else
                            {
                                row = _rows[r];

                                bool hasLat = false, hasLong = false, hasGeom = false;

                                for (int c = 0; c < numCols; c++)
                                {
                                    colName = _columnHeaders[c].Name;
                                    type = _columnHeaders[c].Type;

                                    //Check if string values are within the limits
                                    if (type == typeof(string) || type == typeof(String))
                                    {
                                        if (!string.IsNullOrEmpty(row[c] as string) && (row[c] as string).Length > _maxStringLength)
                                        {
                                            results.Errors.Add("Row: " + r + " Column: '" + colName + "' - String value exceeds limit of " + _maxStringLength + " characters.");
                                        }
                                    }
                                    else if (type == typeof(Geography))
                                    {
                                        //Check the # of coordinates in geography
                                        if (row[c] == null || string.IsNullOrEmpty(row[c].ToString()))
                                        {
                                            results.Warnings.Add("Row: " + r + " Column: '" + colName + "' - No value specified");
                                        }
                                        else
                                        {
                                            var numPoints = coordinatePairRegex.Matches(row[c].ToString()).Count;
                                            if (numPoints > _maxGeomPoints)
                                            {
                                                results.Errors.Add("Row: " + r + " Column: '" + colName + "' - Number of points in Geography exceeds limit of " + _maxGeomPoints + " coordinates.");
                                            }
                                            else if (numPoints >= 1)
                                            {
                                                hasGeom = true;
                                            }
                                        }
                                    }
                                    else if (string.Compare(colName, "Latitude", StringComparison.OrdinalIgnoreCase) == 0 && type == typeof(double))
                                    {
                                        if (row[c] == null)
                                        {
                                            results.Warnings.Add("Row: " + r + " Column: '" + colName + "' - No value specified");
                                        }
                                        else if ((double)row[c] > 90 || (double)row[c] < -90)
                                        {
                                            results.Errors.Add("Row: " + r + " Column: '" + colName + "' - Value is outside the range of -90 and 90.");
                                        }
                                        else
                                        {
                                            hasLat = true;
                                        }
                                    }
                                    else if (string.Compare(colName, "Longitude", StringComparison.OrdinalIgnoreCase) == 0 && type == typeof(double))
                                    {
                                        if (row[c] == null)
                                        {
                                            results.Warnings.Add("Row: " + r + " Column: '" + colName + "' - No value specified");
                                        }
                                        else if ((double)row[c] > 180 || (double)row[c] < -180)
                                        {
                                            results.Errors.Add("Row: " + r + " Column: '" + colName + "' - Value is outside the range of -180 and 180.");
                                        }
                                        else
                                        {
                                            hasLong = true;
                                        }
                                    }
                                }

                                results.AllRowsHaveLocationInfo &= ((hasLat && hasLong) || hasGeom);
                            }
                        }
                    }
                }

                return results;
            });
        }

        #region Data source search methods

        /// <summary>
        /// Gets the column header index by column Name. Returns -1 if column does not exist. Search is case sensitive.
        /// </summary>
        /// <param name="name">Name of column to get index for.</param>
        /// <returns>Index of the named column, or -1 if column name not found.</returns>
        public int GetColumnNameIndex(string name)
        {
            return GetColumnNameIndex(name, false);
        }

        /// <summary>
        /// Gets the column header index by column Name. Returns -1 if column does not exist.
        /// </summary>
        /// <param name="name">Name of column to get index for.</param>
        /// <param name="ignoreCase">A boolean indicating if the search should be case sensitive or not.</param>
        /// <returns>Index of the named column, or -1 if column name not found.</returns>
        public int GetColumnNameIndex(string name, bool ignoreCase)
        {
            int idx = -1;

            if (_columnHeaders != null)
            {
                if (ignoreCase)
                {
                    for (var i = 0; i < _columnHeaders.Count; i++)
                    {
                        if (string.Compare(_columnHeaders[i].Name, name, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            idx = i;
                            break;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < _columnHeaders.Count; i++)
                    {
                        if (string.Compare(_columnHeaders[i].Name, name, StringComparison.Ordinal) == 0)
                        {
                            idx = i;
                            break;
                        }
                    }
                }
            }

            return idx;
        }

        /// <summary>
        /// Gets a list of column header indices by column type. 
        /// </summary>
        /// <param name="type">The column type to search for.</param>
        /// <returns>A list of column indices the have the specified type.</returns>
        public List<int> GetColumnTypeIndex(Type type)
        {
            var idx = new List<int>();

            if (_columnHeaders != null)
            {
                for (var i = 0; i < _columnHeaders.Count; i++)
                {
                    if (_columnHeaders[i].Type == type)
                    {
                        idx.Add(i);
                    }
                }
            }

            return idx;
        }

        /// <summary>
        /// Gets the index of the primary key column. If no primary key specified it will return -1.
        /// </summary>
        /// <returns>The index of the primary key column, or -1.</returns>
        public int GetPrimaryKeyColumnIndex()
        {
            int idx = -1;

            if (_columnHeaders != null)
            {
                for (var i = 0; i < _columnHeaders.Count; i++)
                {
                    if (_columnHeaders[i].IsPrimaryKey)
                    {
                        idx = i;
                        break;
                    }
                }
            }

            return idx;
        }

        /// <summary>
        /// Searches through all rows in the data source for a row that has the specified primary key value specified. 
        /// If no row found, or no primary key specified in data source, null will be returned.
        /// </summary>
        /// <param name="keyValue">The primary key value for the row you want to retrieve.</param>
        /// <returns>An object array for a row, or null if no primary key specified in data source or row is not found.</returns>
        public List<object> GetRowByPrimaryKeyValue(string keyValue)
        {
            var keyIdx = GetPrimaryKeyColumnIndex();
            if (keyIdx == -1 || string.IsNullOrWhiteSpace(keyValue))
            {
                return null;
            }

            return (from r in Rows
                    where string.Compare((string)r[keyIdx], keyValue, StringComparison.OrdinalIgnoreCase) == 0
                    select r).FirstOrDefault();
        }

        #endregion

        /// <summary>
        /// Batch geocodes all the data in a data source and updates it accordingly.
        /// Only Geocodes rows that do not have Latitude, Longitude, or a Geography value specified.
        /// Optimizes batch geocoding by grouping rows with identical addesses together.
        /// </summary>    
        /// <param name="bingMapsKey">A Bing Maps key to use to geocode data.</param>
        /// <returns>The results of the batch geocoding process.</returns>
        public async Task<DataSourceGeocodeResults> Geocode(string bingMapsKey)
        {
            return await Geocode(bingMapsKey, "en-US");
        }

        /// <summary>
        /// Batch geocodes all the data in a data source and updates it accordingly.
        /// Only Geocodes rows that do not have Latitude, Longitude, or a Geography value specified.
        /// Optimizes batch geocoding by grouping rows with identical addesses together.
        /// </summary>   
        /// <param name="bingMapsKey">A Bing Maps key to use to geocode data.</param>
        /// <param name="culture">The culture parameter to use when goeocoding. Default: en-US</param>
        /// <returns>The results of the batch geocoding process.</returns>
        public async Task<DataSourceGeocodeResults> Geocode(string bingMapsKey, string culture)
        {
            DataSourceGeocodeResults results = new DataSourceGeocodeResults();
            try
            {
                if (string.IsNullOrWhiteSpace(bingMapsKey))
                {
                    ReportGeocodeStatus("Geocode process aborted.");
                    results.Error = "Invalid Bing Maps key specified.";
                    return results;
                }

                if (string.IsNullOrWhiteSpace(culture))
                {
                    culture = "en-US";
                }

                //Grab the column indices that contain address related information. Fall back on to alternative naming conventions.
                int addressIdx = GetFirstColumnNameIndex(new string[] { "AddressLine", "Street", "Road", "Address", "StreetAddress" });
                int cityIdx = GetFirstColumnNameIndex(new string[] { "Locality", "City", "Town", "Village", "Municipality", "Township" });
                int postalCodeIdx = GetFirstColumnNameIndex(new string[] { "PostalCode", "ZipCode", "PostCode" });
                int stateIdx = GetFirstColumnNameIndex(new string[] { "AdminDistrict", "Province", "State", "County", "Canton" });
                int countryIdx = GetFirstColumnNameIndex(new string[] { "CountryRegion", "Country" });

                if (addressIdx == -1 && cityIdx == -1 && postalCodeIdx == -1 && stateIdx == -1 && countryIdx == -1)
                {
                    ReportGeocodeStatus("Geocode process aborted.");
                    results.Error = "Data source does not contain any columns that contain address information.";
                    return results;
                }

                bool primaryKeyAdded = false;
                var keyIdx = GetPrimaryKeyColumnIndex();
                if (keyIdx == -1)
                {
                    keyIdx = GetColumnNameIndex(_defaultPrimaryKeyName);
                    if (keyIdx == -1)
                    {
                        ReportGeocodeStatus("Primary key column is not defined in column header. Column added.");
                        _columnHeaders.Add(new ColumnHeader(_defaultPrimaryKeyName, typeof(string)));
                        primaryKeyAdded = true;
                    }
                    else
                    {
                        ReportGeocodeStatus("Geocode process aborted.");
                        results.Error = "Primary key column is not defined in column header.";
                        return results;
                    }                    
                }

                var latIdx = GetColumnNameIndex("Latitude");
                if (latIdx == -1)
                {
                    ReportGeocodeStatus("'Latitude' column is not defined in column header. Column added.");
                    _columnHeaders.Add(new ColumnHeader("Latitude", typeof(double)));
                    latIdx = GetColumnNameIndex("Latitude");
                }

                var lonIdx = GetColumnNameIndex("Longitude");
                if (lonIdx == -1)
                {
                    ReportGeocodeStatus("'Longitude' column is not defined in column header. Column added.");
                    _columnHeaders.Add(new ColumnHeader("Longitude", typeof(double)));
                    lonIdx = GetColumnNameIndex("Longitude");
                }

                var deleteIdx = GetColumnNameIndex("__deleteEntity");

                var geomIdices = GetColumnTypeIndex(typeof(Geography));
                int geomIdx = -1;
                if (geomIdices.Count > 0)
                {
                    geomIdx = geomIdices[0];
                }

                var geocodeFeed = new GeocodeFeed();

                ReportGeocodeStatus("Creating geocode feed out of data source.");

                //Create a table for cross referencing row indices with identical addresses.
                var rowIdxTable = new Dictionary<string, List<int>>();
                int locationsToGeocode = 0;
                bool skipRecord;

                for (int i = 0; i < _rows.Count; i++)
                {
                    if (primaryKeyAdded)
                    {
                        _rows[i].Add(i.ToString());
                    }

                    var row = _rows[i];

                    skipRecord = false;

                    //Skip geocoding of records marked to be deleted.
                    if (deleteIdx != -1)
                    {
                        var deleteInfo = (string)row[deleteIdx];

                        if(string.Compare(deleteInfo, "true", StringComparison.OrdinalIgnoreCase) == 0 ||
                           string.Compare(deleteInfo, "1", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            skipRecord = true;
                        }
                    }

                    //Only geocode locations that don't already have lat or lon values, or are not marked to be deleted, or when a Geography value is specified.
                    if ((geomIdx == -1 || row[geomIdx] == null) && (row[latIdx] == null || row[lonIdx] == null) && !skipRecord)
                    {
                        locationsToGeocode++;
                        var address = new Address()
                        {
                            AddressLine = (addressIdx != -1) ? (string)row[addressIdx] : string.Empty,
                            AdminDistrict = (stateIdx != -1) ? (string)row[stateIdx] : string.Empty,
                            CountryRegion = (countryIdx != -1) ? (string)row[countryIdx] : string.Empty,
                            Locality = (cityIdx != -1) ? (string)row[cityIdx] : string.Empty,
                            PostalCode = (postalCodeIdx != -1) ? (string)row[postalCodeIdx] : string.Empty
                        };

                        var existingId = geocodeFeed.ContainsAddress(address);

                        if (string.IsNullOrEmpty(existingId))
                        {
                            geocodeFeed.Entities.Add(new GeocodeEntity()
                            {
                                Id = i.ToString(),
                                GeocodeRequest = new GeocodeRequest()
                                {
                                    Address = address,
                                    Culture = culture
                                }
                            });
                            rowIdxTable.Add(i.ToString(), new List<int>() { i });
                        }
                        else if (rowIdxTable.ContainsKey(existingId))
                        {
                            rowIdxTable[existingId].Add(i);
                        }
                        else
                        {
                            rowIdxTable.Add(i.ToString(), new List<int>() { i });
                        }
                    }
                }

                if (locationsToGeocode > 0)
                {
                    var geocodeManager = new BatchGeocodeManager();

                    if (GeocodeStatusChanged != null)
                    {
                        geocodeManager.StatusChanged += (msg) =>
                        {
                            ReportGeocodeStatus(msg);
                        };
                    }

                    var geocodeResults = await geocodeManager.Geocode(geocodeFeed, bingMapsKey);

                    results.Error = geocodeResults.Error;

                    //Need to merge the goeocde results back into the data source.
                    if (geocodeResults.Succeeded != null &&
                        geocodeResults.Succeeded.Entities != null)
                    {
                        ReportGeocodeStatus("Merging successful geocode results into data source.");

                        foreach (var s in geocodeResults.Succeeded.Entities)
                        {
                            if (rowIdxTable.ContainsKey(s.Id) &&
                                s.GeocodeResponse != null &&
                                s.GeocodeResponse.Length > 0 &&
                                s.GeocodeResponse[0].Point != null)
                            {
                                //Update all rows that have the same geocode request.
                                foreach (var idx in rowIdxTable[s.Id])
                                {
                                    _rows[idx][latIdx] = s.GeocodeResponse[0].Point.Latitude;
                                    _rows[idx][lonIdx] = s.GeocodeResponse[0].Point.Longitude;
                                    results.Succeeded++;
                                }
                            }
                        }
                    }

                    if (geocodeResults.Failed != null &&
                        geocodeResults.Failed.Entities != null)
                    {
                        foreach (var s in geocodeResults.Failed.Entities)
                        {
                            if (rowIdxTable.ContainsKey(s.Id))
                            {
                                //Get all the primary keys of the failed rows.
                                foreach (var idx in rowIdxTable[s.Id])
                                {
                                    results.FailedRows.Add((string)_rows[idx][keyIdx]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    results.Succeeded = _rows.Count;
                }

                ReportGeocodeStatus("Geocode process for data source completed.");
            }
            catch (Exception ex)
            {
                results.Error = ex.Message;
                ReportGeocodeStatus("An error has occured. Batch geocode process aborted.");
            }

            return results;
        }

        #endregion

        #region Private Methods

        #region Read Methods

        private void ParseDataSource(XDocument doc)
        {
            _columnHeaders = new List<ColumnHeader>();
            _rows = new List<List<object>>();

            var schema = doc.Root.FirstNode as XElement;

            if (schema != null)
            {
                var idAttr = XmlUtilities.GetAttribute(schema, "id");
                if (idAttr != null)
                {
                    _info.DataSourceName = idAttr.Value;
                }

                var dsSchema = (schema.FirstNode as XElement);

                var entitySchema = ((dsSchema.FirstNode as XElement).FirstNode as XElement).FirstNode;
                var entityNameAttr = XmlUtilities.GetAttribute(entitySchema as XElement, "name");

                if (entityNameAttr != null)
                {
                    _info.EntityTypeName = entityNameAttr.Value;
                }

                var sequenceSchema = ((entitySchema as XElement).FirstNode as XElement).FirstNode as XElement;
                var sequenceElms = sequenceSchema.Elements();

                var primaryKeySchema = (dsSchema.LastNode as XElement).LastNode;

                string primaryKeyName = null;
                var primaryKeyAttr = XmlUtilities.GetAttribute(primaryKeySchema as XElement, "xpath");
                if (primaryKeyAttr != null)
                {
                    primaryKeyName = primaryKeyAttr.Value;
                }

                foreach (var e in sequenceElms)
                {
                    var elm = e as XElement;
                    var name = XmlUtilities.GetAttribute(elm, "name");
                    var xmlType = XmlUtilities.GetAttribute(elm, "type");

                    if (name != null && xmlType != null)
                    {
                        _columnHeaders.Add(new ColumnHeader(name.Value, ParseXmlPropertyType(xmlType.Value), (!string.IsNullOrEmpty(primaryKeyName) && string.Compare(name.Value, primaryKeyName, StringComparison.OrdinalIgnoreCase) == 0)));
                    }
                }

                var n = schema.NextNode;
                while (n != null)
                {
                    if (n is XElement)
                    {
                        var row = ParseRow(n as XElement);

                        if (row != null)
                        {
                            _rows.Add(row);
                        }
                    }

                    n = n.NextNode;
                }
            }
        }

        private List<object> ParseRow(XElement node)
        {
            XElement n = node.FirstNode as XElement;
            var row = new object[_columnHeaders.Count];
            string nodeName;
            int colIdx;

            while (n != null)
            {
                nodeName = n.Name.LocalName;
                colIdx = GetColumnNameIndex(nodeName);

                if (colIdx != -1)
                {
                    var type = _columnHeaders[colIdx].Type;

                    if (type == typeof(string))
                    {
                        row[colIdx] = XmlUtilities.GetString(n);
                    }
                    else if (type == typeof(long))
                    {
                        row[colIdx] = XmlUtilities.GetInt64(n, 0);
                    }
                    else if (type == typeof(double))
                    {
                        row[colIdx] = XmlUtilities.GetDouble(n, double.NaN);
                    }
                    else if (type == typeof(bool))
                    {
                        row[colIdx] = XmlUtilities.GetBoolean(n, false);
                    }
                    else if (type == typeof(DateTime))
                    {
                        var d = XmlUtilities.GetDateTime(n);
                        if (d.HasValue)
                        {
                            row[colIdx] = d.Value;
                        }
                        else
                        {
                            row[colIdx] = null;
                        }
                    }
                    else if (type == typeof(Geography))
                    {
                        row[colIdx] = new Geography()
                        {
                            WellKnownText = XmlUtilities.GetString(n)
                        };
                    }
                }

                n = n.NextNode as XElement;
            }

            return new List<object>(row);
        }

        private void ParseDataSource(TextReader textReader, DataSourceFormat dataSourceType)
        {
            char delimiter;

            switch (dataSourceType)
            {
                case DataSourceFormat.PIPE:
                    delimiter = '|';
                    break;
                case DataSourceFormat.TAB:
                    delimiter = '\t';
                    break;
                case DataSourceFormat.CSV:
                default:
                    delimiter = ',';
                    break;
            }
                        
            _rows = new List<List<object>>();
            List<string> colNames = null;
            
            using (var reader = new DelimitedFileReader(textReader, delimiter))
            {
                var row = reader.GetNextRow();

                if (row != null && row.Count > 0)
                {
                    //Parse Schema Version info.
                    if (row[0].StartsWith("Bing Spatial Data Services", StringComparison.OrdinalIgnoreCase))
                    {
                        string entityName = string.Empty;

                        if (row[0].Contains(","))
                        {
                            var r = row[0].Split(new char[] { ',' });
                            if (r.Length >= 3)
                            {
                                entityName = r[2].Trim();
                            }
                        }
                        else if (row.Count >= 3 && !string.IsNullOrWhiteSpace(row[3]))
                        {
                            entityName = row[2].Trim();
                        }
                                                
                        if (!string.IsNullOrWhiteSpace(entityName))
                        {
                            _info.EntityTypeName = entityName;
                        }

                        row = reader.GetNextRow();
                    }

                    //Parse header row                    
                    colNames = ParseColumnHeader(row);
                    row = reader.GetNextRow();
                }

                if (_columnHeaders != null && _columnHeaders.Count > 0)
                {
                    while (row != null && row.Count > 0)
                    {
                        var parsedRow = ParseRow(row);

                        if (parsedRow != null)
                        {
                            _rows.Add(parsedRow);
                        }

                        row = reader.GetNextRow();
                    }
                }
            }
        }

        private List<string>ParseColumnHeader(List<string> row)
        {
            _columnHeaders = new List<ColumnHeader>();
            var colNames = new List<String>();

            int len = row.Count;
            string name, typeName;
            bool isPrimaryKey = false;
            Type type;

            var headerRegex = new Regex(_columnHeaderPattern);

            for (int i = 0; i < len; i++)
            {
                isPrimaryKey = false;
                name = row[i];
                typeName = string.Empty;

                if (i < len - 1 && row[i + 1].StartsWith("primaryKey)", StringComparison.OrdinalIgnoreCase))
                {
                    name = name.Substring(0, name.IndexOf('('));

                    //Parse out type name
                    typeName = row[i].Substring(row[i].IndexOf('(') + 1);

                    if (typeName.Contains(","))
                    {
                        typeName = typeName.Replace(",", "");
                    }

                    isPrimaryKey = true;
                    i++;
                }
                else if (headerRegex.IsMatch(name))
                {
                    Match m = headerRegex.Match(name);

                    //Expect column header to look like:
                    //OData: EntityID(Edm.String,primarykey) or Longitude(Edm.Double)
                    if (m.Success && m.Groups.Count >= 3)
                    {
                        name = m.Groups[1].Value;
                        typeName = m.Groups[2].Value;

                        if (typeName.IndexOf(",") > -1)
                        {
                            typeName = typeName.Substring(0, typeName.IndexOf(","));
                        }

                        if (m.Groups[2].Value.IndexOf("primaryKey", StringComparison.OrdinalIgnoreCase) > -1)
                        {
                            isPrimaryKey = true;
                        }
                    }
                }

                //Validate Data Type
                switch (typeName.ToLowerInvariant())
                {
                    case "edm.int64":
                    case "int":
                    case "long":
                        type = typeof(long);
                        break;
                    case "edm.double":
                    case "float":
                    case "double":
                        type = typeof(double);
                        break;
                    case "edm.boolean":
                    case "bool":
                    case "boolean":
                        type = typeof(bool);
                        break;
                    case "edm.datetime":
                    case "date":
                    case "datetime":
                        type = typeof(DateTime);
                        break;
                    case "edm.geography":
                        type = typeof(Geography);
                        break;
                    case "edm.string":
                    case "varchar":     //Multimap
                    case "text":        //Multimap
                    default:
                        type = typeof(string);
                        break;
                }

                _columnHeaders.Add(new ColumnHeader(name, type, isPrimaryKey));
                colNames.Add(name);
            }

            return colNames;
        }

        private List<object> ParseRow(List<string> row)
        {
            var parsedRow = new object[_columnHeaders.Count];
            string nodeName;
            Type type;

            double tempDouble;
            long tempLong;
            bool tempBool;
            DateTime tempDate;

            for (int i = 0; i < _columnHeaders.Count; i++)
            {
                if (i >= row.Count)
                {
                    parsedRow[i] = null;
                }
                else
                {
                    nodeName = _columnHeaders[i].Name;
                    type = _columnHeaders[i].Type;

                    if (type == typeof(string))
                    {
                        parsedRow[i] = row[i];
                    }
                    else if (type == typeof(long))
                    {
                        if (!long.TryParse(row[i], out tempLong))
                        {
                            tempLong = 0;
                        }

                        parsedRow[i] = tempLong;
                    }
                    else if (type == typeof(double))
                    {
                        if (double.TryParse(row[i], out tempDouble))
                        {
                            parsedRow[i] = tempDouble;
                        }
                    }
                    else if (type == typeof(bool))
                    {
                        if (!bool.TryParse(row[i], out tempBool))
                        {
                            tempBool = false;
                        }

                        parsedRow[i] = tempBool;
                    }
                    else if (type == typeof(DateTime))
                    {
                        if (!DateTime.TryParse(row[i], out tempDate))
                        {
                            parsedRow[i] = tempDate;
                        }

                        parsedRow[i] = null;
                    }
                    else if (type == typeof(Geography))
                    {
                        parsedRow[i] = new Geography()
                        {
                            WellKnownText = row[i]
                        };
                    }
                    else
                    {
                        parsedRow[i] = null;
                    }
                }
            }

            return new List<object>(parsedRow);
        }

        #endregion

        #region Write Methods

        private void Write(XmlWriter xmlWriter, bool skipEmptyLocations)
        {
            //<?xml version="1.0" encoding="UTF-8"?>
            //<DataSourceNameData>
            //  <xs:schema id="DataSourceName" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
            //    <xs:element name="DataSourceName" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
            //      <xs:complexType>
            //        <xs:choice minOccurs="0" maxOccurs="unbounded">
            //          <xs:element name="EntityTypeName">
            //            <xs:complexType>
            //              <xs:sequence>
            //                <xs:element name="AddressLine" type="xs:string" minOccurs="0" />
            //                <xs:element name="Locality" type="xs:string" minOccurs="0" />
            //                <xs:element name="AdminDistrict" type="xs:string" minOccurs="0" />
            //                <xs:element name="CountryRegion" type="xs:string" minOccurs="0" />
            //                <xs:element name="PostalCode" type="xs:string" minOccurs="0" />
            //                <xs:element name="Name" type="xs:string" minOccurs="0" />
            //                <xs:element name="EntityID" type="xs:string" />
            //                <xs:element name="Longitude" type="xs:double" minOccurs="0" />
            //                <xs:element name="Latitude" type="xs:double" minOccurs="0" />
            //                <xs:element name="StoreLayout" type="xs:anyType" minOccurs="0" />
            //              </xs:sequence>
            //            </xs:complexType>
            //          </xs:element>
            //        </xs:choice>
            //      </xs:complexType>
            //      <xs:unique name="Constraint1" msdata:PrimaryKey="true">
            //        <xs:selector xpath=".//EntityTypeName" />
            //        <xs:field xpath="EntityID" />
            //      </xs:unique>
            //    </xs:element>
            //  </xs:schema>
            //  <EntityTypeName>
            //    <AddressLine>Løven</AddressLine>
            //    <Locality>Aalborg</Locality>
            //    <AdminDistrict>Nordjyllands Amt</AdminDistrict>
            //    <CountryRegion>Danmark</CountryRegion>
            //    <PostalCode>9200</PostalCode>
            //    <Name>Fourth Coffee Store #22067</Name>
            //    <EntityID>-22067</EntityID>
            //    <Longitude>9.87443416667</Longitude>
            //    <Latitude>57.00376611111</Latitude>
            //    <StoreLayout>POLYGON((9.86445 57.13876,9.89266 57.13876, 9.89266 56.94234,9.86445 56.94234,9.86445 57.13876))</StoreLayout>
            //  </EntityTypeName>
            //</DataSourceNameData>

            //Open document
            xmlWriter.WriteStartDocument(true);

            //Write root tag -> Data Source Name.
            xmlWriter.WriteStartElement(_info.DataSourceName + "Data");

            //Write schema info
            WriteXmlSchema(xmlWriter);

            if (_rows != null)
            {
                var primaryKeyIdx = GetPrimaryKeyColumnIndex();
                var latIdx = GetColumnNameIndex("Latitude");
                var lonIdx = GetColumnNameIndex("Longitude");
                var geomIdices = GetColumnTypeIndex(typeof(Geography));
                var geomIdx = (geomIdices != null && geomIdices.Count > 0)? geomIdices[0] : -1;
                var deleteIdx = GetColumnNameIndex("__deleteEntity");

                if(primaryKeyIdx > -1){    
                    foreach (var row in _rows)
                    {
                        WriteXmlRow(row, xmlWriter, primaryKeyIdx, latIdx, lonIdx, geomIdx, deleteIdx, skipEmptyLocations);
                    }
                }
            }

            //Close document
            xmlWriter.WriteEndDocument();
        }

        private void WriteXmlSchema(XmlWriter xmlWriter)
        {
            //  <xs:schema id="DataSourceName" xmlns="" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
            //    <xs:element name="DataSourceName" msdata:IsDataSet="true" msdata:UseCurrentLocale="true">
            //      <xs:complexType>
            //        <xs:choice minOccurs="0" maxOccurs="unbounded">
            //          <xs:element name="EntityTypeName">
            //            <xs:complexType>
            //              <xs:sequence>
            //                <xs:element name="AddressLine" type="xs:string" minOccurs="0" />
            //                <xs:element name="Locality" type="xs:string" minOccurs="0" />
            //                <xs:element name="AdminDistrict" type="xs:string" minOccurs="0" />
            //                <xs:element name="CountryRegion" type="xs:string" minOccurs="0" />
            //                <xs:element name="PostalCode" type="xs:string" minOccurs="0" />
            //                <xs:element name="Name" type="xs:string" minOccurs="0" />
            //                <xs:element name="PrimaryKeyName" type="xs:string" />
            //                <xs:element name="Longitude" type="xs:double" minOccurs="0" />
            //                <xs:element name="Latitude" type="xs:double" minOccurs="0" />
            //                <xs:element name="StoreLayout" type="xs:anyType" minOccurs="0" />
            //              </xs:sequence>
            //            </xs:complexType>
            //          </xs:element>
            //        </xs:choice>
            //      </xs:complexType>
            //      <xs:unique name="Constraint1" msdata:PrimaryKey="true">
            //        <xs:selector xpath=".//EntityTypeName" />
            //        <xs:field xpath="PrimaryKeyName" />
            //      </xs:unique>
            //    </xs:element>
            //  </xs:schema>

            //Write start of Schema 
            xmlWriter.WriteStartElement("xs", "schema", XmlSchemaNamespace);

            xmlWriter.WriteAttributeString("id", _info.DataSourceName);
            xmlWriter.WriteAttributeString("xmlns", "xs", XmlnsNamespace, XmlSchemaNamespace);
            xmlWriter.WriteAttributeString("xmlns", "msdata", XmlnsNamespace, MsDataNamespace);

            //Write start of data source element
            xmlWriter.WriteStartElement("element", XmlSchemaNamespace);

            xmlWriter.WriteAttributeString("name", _info.DataSourceName);
            xmlWriter.WriteAttributeString("IsDataSet", MsDataNamespace, "true");
            xmlWriter.WriteAttributeString("UseCurrentLocale", MsDataNamespace, "true");

            xmlWriter.WriteStartElement("complexType", XmlSchemaNamespace);

            xmlWriter.WriteStartElement("choice", XmlSchemaNamespace);
            xmlWriter.WriteAttributeString("minOccurs", "0");
            xmlWriter.WriteAttributeString("maxOccurs", "unbounded");

            //Write start of EntityTypeName element
            xmlWriter.WriteStartElement("element", XmlSchemaNamespace);

            xmlWriter.WriteAttributeString("name", _info.EntityTypeName);

            xmlWriter.WriteStartElement("complexType", XmlSchemaNamespace);

            xmlWriter.WriteStartElement("sequence", XmlSchemaNamespace);

            if (_columnHeaders != null)
            {
                foreach (var col in _columnHeaders)
                {
                    xmlWriter.WriteStartElement("element", XmlSchemaNamespace);
                    xmlWriter.WriteAttributeString("name", col.Name);
                    xmlWriter.WriteAttributeString("type", col.GetXmlTypeName());
                    xmlWriter.WriteAttributeString("minOccurs", "0");

                    if (col.Type == typeof(DateTime))
                    {
                        xmlWriter.WriteAttributeString("DateTimeMode", MsDataNamespace, "Utc");
                    }

                    xmlWriter.WriteEndElement();
                }
            }

            //Close sequence
            xmlWriter.WriteEndElement();

            //Close end of ComplexType
            xmlWriter.WriteEndElement();

            //Close end of EntityTypeName element
            xmlWriter.WriteEndElement();

            //Close end of choice
            xmlWriter.WriteEndElement();

            //Close end of ComplexType
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("unique", XmlSchemaNamespace);
            xmlWriter.WriteAttributeString("name", "Constraint1");
            xmlWriter.WriteAttributeString("PrimaryKey", MsDataNamespace, "true");

            xmlWriter.WriteStartElement("selector", XmlSchemaNamespace);
            xmlWriter.WriteAttributeString("xpath", ".//" + _info.EntityTypeName);
            xmlWriter.WriteEndElement();

            var keyIdx = GetPrimaryKeyColumnIndex();
            if (keyIdx > -1)
            {
                xmlWriter.WriteStartElement("field", XmlSchemaNamespace);
                xmlWriter.WriteAttributeString("xpath", _columnHeaders[keyIdx].Name);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();

            //Close end of data source element
            xmlWriter.WriteEndElement();

            //Close end of Schema
            xmlWriter.WriteEndElement();
        }

        private void WriteXmlRow(List<object> row, XmlWriter xmlWriter, int primaryKeyIdx, int latIdx, int lonIdx, int geomIdx, int deleteIdx, bool skipEmptyLocations)
        {
            if (row.Count >= _columnHeaders.Count)
            {
                string tempString;

                bool addDeleteRow = false;

                //Ensure rows marked to be deleted are added regardless if they have location information or not.
                if (deleteIdx != -1)
                {
                    var deleteInfo = (string)row[deleteIdx];

                    if (string.Compare(deleteInfo, "true", StringComparison.OrdinalIgnoreCase) == 0 ||
                       string.Compare(deleteInfo, "1", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        addDeleteRow = true;
                    }
                }
                               
                //Verify that row has location data if required.
                if (addDeleteRow || !skipEmptyLocations || (
                    latIdx != -1 && row[latIdx] != null &&
                    lonIdx != -1 && row[lonIdx] != null) ||
                    (geomIdx != -1 && row[geomIdx] != null && !string.IsNullOrWhiteSpace(row[geomIdx].ToString())))
                {
                    xmlWriter.WriteStartElement(_info.EntityTypeName);

                    for (int i = 0; i < _columnHeaders.Count; i++)
                    {
                        if (row[i] != null)
                        {
                            tempString = string.Empty;

                            if (_columnHeaders[i].Type == typeof(DateTime))
                            {
                                tempString = ((DateTime)row[i]).ToUniversalTime().ToString("O");
                            }
                            else
                            {
                                tempString = row[i].ToString();
                            }

                            if (!string.IsNullOrWhiteSpace(tempString))
                            {
                                xmlWriter.WriteElementString(_columnHeaders[i].Name, tempString);
                            }
                        }
                    }

                    xmlWriter.WriteEndElement();
                }
            }
        }

        private void WriteDelimitedDataSource(Stream outputStream, DataSourceFormat dataSourceType, bool skipEmptyLocations)
        {
            char delimiter;

            switch (dataSourceType)
            {
                case DataSourceFormat.PIPE:
                    delimiter = '|';
                    break;
                case DataSourceFormat.TAB:
                    delimiter = '\t';
                    break;
                case DataSourceFormat.CSV:
                default:
                    delimiter = ',';
                    break;
            }

            using (var writer = new DelimitedFileWriter(outputStream, _columnHeaders.Count, delimiter))
            {
                //Add Bing Schema Information
                writer.WriteLine(string.Format(_schemaText, _info.EntityTypeName));

                var headerCells = new List<string>();

                //Generate Header Information
                foreach (var col in _columnHeaders)
                {
                    headerCells.Add(col.ToString());
                }

                //Write Header Information
                writer.WriteRow(headerCells, false);

                if (_rows != null)
                {
                    var latIdx = GetColumnNameIndex("Latitude");
                    var lonIdx = GetColumnNameIndex("Longitude");
                    var geomIdices = GetColumnTypeIndex(typeof(Geography));                    
                    var geomIdx = (geomIdices != null && geomIdices.Count > 0) ? geomIdices[0] : -1;
                    var deleteIdx = GetColumnNameIndex("__deleteEntity");

                    bool addDeleteRow;

                    //Write the rows
                    foreach (var row in _rows)
                    {
                        addDeleteRow = false;

                        //Ensure rows marked to be deleted are added regardless if they have location information or not.
                        if (deleteIdx != -1)
                        {
                            var deleteInfo = (string)row[deleteIdx];

                            if (string.Compare(deleteInfo, "true", StringComparison.OrdinalIgnoreCase) == 0 ||
                               string.Compare(deleteInfo, "1", StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                addDeleteRow = true;
                            }
                        }
                               
                        //Verify that row has location data if required.
                        if (addDeleteRow || !skipEmptyLocations || (
                            latIdx != -1 && row[latIdx] != null &&
                            lonIdx != -1 && row[lonIdx] != null &&
                            (geomIdx == -1 || (row[geomIdx] != null && !string.IsNullOrWhiteSpace((string)row[geomIdx])))))
                        {
                            writer.WriteRow(row, true);
                        }
                    
                    }
                }
            }
        }

        #endregion

        #region Helper Methods

        private void ReportGeocodeStatus(string msg)
        {
            if (GeocodeStatusChanged != null)
            {
                GeocodeStatusChanged(msg);
            }
        }

        private Type ParseXmlPropertyType(string type)
        {
            switch (type.ToLowerInvariant())
            {
                case "xs:long":
                    return typeof(long);
                case "xs:boolean":
                    return typeof(bool);
                case "xs:double":
                    return typeof(double);
                case "xs:datetime":
                    return typeof(DateTime);
                case "xs:anytype":
                    return typeof(Geography);
                case "xs:string":
                default:
                    return typeof(string);

            }
        }

        /// <summary>
        /// Takes a list of names and finds the index of the first name that appears in the data source. This is not case insensitive.
        /// </summary>
        /// <param name="names">Array of names to look for.</param>
        /// <returns>The index of the column or -1.</returns>
        private int GetFirstColumnNameIndex(string[] names)
        {
            int idx = -1;

            if (_columnHeaders != null)
            {
                for (var n = 0; n < names.Length; n++)
                {
                    for (var i = 0; i < _columnHeaders.Count; i++)
                    {
                        if (string.Compare(_columnHeaders[i].Name, names[n], StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            idx = i;
                            break;
                        }
                    }

                    if (idx > -1)
                    {
                        break;
                    }
                }
            }

            return idx;
        }

        #endregion

        #endregion
    }
}

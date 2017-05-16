using BingMapsSDSToolkit.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BingMapsSDSToolkit.GeocodeDataFlowAPI
{
    /// <summary>
    /// An object that contains the data that is to be geocoded, or has been geocoded.
    /// </summary>
    [XmlSerializerFormat]
    [XmlRoot]
    public class GeocodeFeed
    {
        #region Private Properties

        private string _version = "2.0";
        private static DataContractJsonSerializer queryParseSerializer = new DataContractJsonSerializer(typeof(QueryParseValue[]));
        private static DataContractJsonSerializer geocodePointSerializer = new DataContractJsonSerializer(typeof(GeocodePoint[]));

        #endregion

        #region Constructor

        /// <summary>
        /// An object that contains the data that is to be geocoded, or has been geocoded.
        /// </summary>
        public GeocodeFeed()
        {
            Entities = new List<GeocodeEntity>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// A list of all entities in the geocode feed.
        /// </summary>
        [XmlElement("GeocodeEntity")]
        public List<GeocodeEntity> Entities { get; set; }

        /// <summary>
        /// The Geocode DataFlow API version to use. This always returns version 2.
        /// </summary>
        [XmlAttribute]
        public string Version
        {
            get { return _version; }
            set
            {
                //Do nothing with value, version should always be 2.0
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deserializes a GeocodeFeed from an XML stream.
        /// </summary>
        /// <param name="stream">GeocodeFeed XML feed stream.</param>
        /// <returns>A GeocodeFeed that has been parsed from a stream.</returns>
        public static Task<GeocodeFeed> ReadAsync(Stream stream)
        {
            return Task.Run<GeocodeFeed>(() =>
            {
                GeocodeFeed feed = null;

                var ms = new MemoryStream();

                try
                {
                    stream.CopyTo(ms);
                    if (BingMapsSDSToolkit.Internal.XmlUtilities.IsStreamCompressed(ms))
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

                    var serializer = new XmlSerializer(typeof(GeocodeFeed), "http://schemas.microsoft.com/search/local/2010/5/geocode");
                    feed = (GeocodeFeed)serializer.Deserialize(ms);
                }
                catch { }

                ms.Dispose();
                return feed;
            });
        }

        public static Task<GeocodeFeed> ReadAsync(Stream stream, BatchFileFormat fileFormat)
        {
            if (fileFormat == BatchFileFormat.XML)
            {
                return ReadAsync(stream);
            }
            else
            {
                return Task.Run<GeocodeFeed>(() =>
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

                        using (var reader = new StreamReader(ms))
                        {
                            return ParseDelimitedFile(reader, fileFormat);
                        }
                    }
                    catch { }

                    ms.Dispose();
                    return null;
                });
            }
        }

        /// <summary>
        /// Serializes a GeocodeFeed into an XML stream. 
        /// </summary>
        /// <param name="outputStream">Stream to output XML to.</param>
        /// <returns>An async Task that writes the geocode feed to a stream.</returns>
        public Task WriteAsync(Stream outputStream)
        {
            return Task.Run(() =>
            {
                var xmlWriterSettings = new XmlWriterSettings()
                {
                    Encoding = System.Text.Encoding.UTF8,
                    Indent = true,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates
                };

                var ns = new XmlSerializerNamespaces();
                ns.Add("", "http://schemas.microsoft.com/search/local/2010/5/geocode");

                using (var writer = XmlWriter.Create(outputStream, xmlWriterSettings))
                {
                    var serializer = new XmlSerializer(typeof(GeocodeFeed), "http://schemas.microsoft.com/search/local/2010/5/geocode");
                    serializer.Serialize(writer, this, ns);
                }
            });
        }

        /// <summary>
        /// Searches for the first entity that has the specified id value.
        /// </summary>
        /// <param name="id">Id of entity to retrieve.</param>
        /// <returns>An entity with the specified id or null.</returns>
        public GeocodeEntity GetEntityById(string id)
        {
            return (from e in Entities
                    where string.Compare(e.Id, id, StringComparison.Ordinal) == 0
                    select e).FirstOrDefault();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks to see if the geocode request contains and address. If it does, then it returns the string ID, otherwise it returns null.
        /// </summary>
        /// <param name="address">Address to search for.</param>
        /// <returns>The ID of the matching geocode entity, or null.</returns>
        internal string ContainsAddress(Address address)
        {            
            var entity = (from e in Entities
                     where e.GeocodeRequest != null && e.GeocodeRequest.Address != null && e.GeocodeRequest.Address.Equals(address)
                     select e).FirstOrDefault();

            return (entity != null) ? entity.Id : null;
        }

        private static GeocodeFeed ParseDelimitedFile(StreamReader textReader, BatchFileFormat format)
        {
            char delimiter;

            switch (format)
            {
                case BatchFileFormat.PIPE:
                    delimiter = '|';
                    break;
                case BatchFileFormat.TAB:
                    delimiter = '\t';
                    break;
                case BatchFileFormat.CSV:
                default:
                    delimiter = ',';
                    break;
            }
            
            var feed = new GeocodeFeed();
            double schemaVersion = 1.0;

            using (var reader = new DelimitedFileReader(textReader, delimiter))
            {
                var row = reader.GetNextRow();

                if (row != null && row.Count > 0)
                {
                    //Parse Schema Version info.
                    if (row[0].StartsWith("Bing Spatial Data Services", StringComparison.OrdinalIgnoreCase) && row.Count >= 2)
                    {
                        double.TryParse(row[1], out schemaVersion);
                        row = reader.GetNextRow();
                    }

                    //Skip header
                    if (string.Compare(row[0], "Id", StringComparison.OrdinalIgnoreCase) == 0 ||
                        string.Compare(row[0], "GeocodeEntity/@Id", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        row = reader.GetNextRow();
                    }

                    //Parse rows
                    while (row != null && row.Count > 0)
                    {
                        var parsedRow = ParseRow(row, schemaVersion);

                        if (parsedRow != null)
                        {
                            feed.Entities.Add(parsedRow);
                        }

                        row = reader.GetNextRow();
                    }
                }
            }

            return feed;
        }

        private static GeocodeEntity ParseRow(List<string> row, double schemaVersion){

            var entity = new GeocodeEntity()
            {
                Id = row[0]
            };

            int min = Math.Min(12, row.Count);

            var request = new GeocodeRequest()
            {
                Address = new Address()
            };

            var response = new GeocodeResponse()
            {
                Address = new Address(),
                BoundingBox = new BoundingBox()
            };
            
            var reverseRequest = new ReverseGeocodeRequest()
            {
                Location = new GeoDataLocation(0,0)
            };

            int reqCnt = 0, resCnt = 0, revResCnt = 0;
            double temp;
            var locs = new List<GeocodePoint>();

            for (int i = 1; i < min; i++)
            {
                if (!string.IsNullOrWhiteSpace(row[i]))
                {
                    switch (i)
                    {
                        case 1:
                            request.Culture = row[i];
                            reqCnt--;
                            break;
                        case 2:
                            request.Query = row[i];
                            break;
                        case 3:
                            request.Address.AddressLine = row[i];
                            break;
                        case 4:
                            request.Address.AdminDistrict = row[i];
                            break;
                        case 5:
                            request.Address.CountryRegion = row[i];
                            break;
                        case 6:
                            request.Address.AdminDistrict2 = row[i];
                            break;
                        case 7:
                            request.Address.FormattedAddress = row[i];
                            break;
                        case 8:
                            request.Address.Locality = row[i];
                            break;
                        case 9:
                            request.Address.PostalCode = row[i];
                            break;
                        case 10:
                            request.Address.PostalTown = row[i];
                            break;
                        case 11:
                            request.ConfidenceFilter = new ConfidenceFilter()
                            {
                                MinimumConfidence = row[i]
                            };
                            reqCnt--;
                            break;
                        default:
                            break;
                    }

                    reqCnt++;
                }
            }

            if (schemaVersion == 1)
            {
                for (int i = 12; i < row.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(row[i]))
                    {
                        switch (i)
                        {
                            case 12:
                                response.Address.AddressLine = row[i];
                                break;
                            case 13:
                                response.Address.AdminDistrict = row[i];
                                break;
                            case 14:
                                response.Address.CountryRegion = row[i];
                                break;
                            case 15:
                                response.Address.AdminDistrict2 = row[i];
                                break;
                            case 16:
                                response.Address.FormattedAddress = row[i];
                                break;
                            case 17:
                                response.Address.Locality = row[i];
                                break;
                            case 18:
                                response.Address.PostalCode = row[i];
                                break;
                            case 19:
                                response.Address.PostalTown = row[i];
                                break;
                            case 20:
                                if (double.TryParse(row[i], out temp))
                                {
                                    locs.Add(new GeocodePoint()
                                    {
                                        Latitude = temp,
                                        CalculationMethod = "Rooftop",
                                        UsageTypes = "Display"
                                    });
                                }
                                break;
                            case 21:
                                if (locs.Count > 0 && double.TryParse(row[i], out temp))
                                {
                                    locs[locs.Count - 1].Longitude = temp;
                                }
                                break;
                            case 22:
                                if (double.TryParse(row[i], out temp))
                                {
                                    locs.Add(new GeocodePoint()
                                    {
                                        Latitude = temp,
                                        CalculationMethod = "Interpolated",
                                        UsageTypes = "Route"
                                    });
                                }
                                break;
                            case 23:
                                if (locs.Count > 0 && double.TryParse(row[i], out temp))
                                {
                                    locs[locs.Count - 1].Longitude = temp;
                                }
                                break;
                            case 24:
                                response.Confidence = row[i];
                                break;
                            case 25:
                                response.Name = row[i];
                                break;
                            case 26:
                                response.EntityType = row[i];
                                break;
                            case 27:
                                entity.StatusCode = row[i];
                                break;
                            case 28:
                                entity.FaultReason = row[i];
                                break;
                            case 29:
                                if (double.TryParse(row[i], out temp))
                                {
                                    reverseRequest.Location.Latitude = temp;
                                    revResCnt++;
                                }
                                resCnt--;
                                break;
                            case 30:
                                if (double.TryParse(row[i], out temp))
                                {
                                    reverseRequest.Location.Longitude = temp;
                                    revResCnt++;
                                }
                                resCnt--;
                                break;
                            default:
                                break;
                        }

                        resCnt++;
                    }
                }
            }
            else
            {
                for (int i = 12; i < row.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(row[i]))
                    {
                        switch (i)
                        {
                            case 12:
                                reverseRequest.IncludeEntityTypes = row[i];
                                break;
                            case 13:
                                if (double.TryParse(row[i], out temp))
                                {
                                    reverseRequest.Location.Latitude = temp;
                                    revResCnt++;
                                }
                                resCnt--;
                                break;
                            case 14:
                                if (double.TryParse(row[i], out temp))
                                {
                                    reverseRequest.Location.Longitude = temp;
                                    revResCnt++;
                                }
                                resCnt--;
                                break;
                            case 15:
                                response.Address.AddressLine = row[i];
                                break;
                            case 16:
                                response.Address.AdminDistrict = row[i];
                                break;
                            case 17:
                                response.Address.CountryRegion = row[i];
                                break;
                            case 18:
                                response.Address.AdminDistrict2 = row[i];
                                break;
                            case 19:
                                response.Address.FormattedAddress = row[i];
                                break;
                            case 20:
                                response.Address.Locality = row[i];
                                break;
                            case 21:
                                response.Address.PostalCode = row[i];
                                break;
                            case 22:
                                response.Address.PostalTown = row[i];
                                break;
                            case 23:
                                response.Address.Neighborhood = row[i];
                                break;
                            case 24:
                                response.Address.Landmark = row[i];
                                break;
                            case 25:
                                response.Confidence = row[i];
                                break;
                            case 26:
                                response.Name = row[i];
                                break;
                            case 27:
                                response.EntityType = row[i];
                                break;
                            case 28:
                                response.MatchCodes = row[i];
                                break;
                            case 29:
                                if (double.TryParse(row[i], out temp))
                                {
                                    locs.Add(new GeocodePoint()
                                    {
                                        Latitude = temp
                                    });
                                }
                                break;
                            case 30:
                                if (locs.Count > 0 && double.TryParse(row[i], out temp))
                                {
                                    locs[locs.Count - 1].Longitude = temp;
                                }
                                break;
                            case 31:
                                if (double.TryParse(row[i], out temp))
                                {
                                    response.BoundingBox.SouthLatitude = temp;
                                }
                                break;
                            case 32:
                                if (double.TryParse(row[i], out temp))
                                {
                                    response.BoundingBox.WestLongitude = temp;
                                }
                                break;
                            case 33:
                                if (double.TryParse(row[i], out temp))
                                {
                                    response.BoundingBox.NorthLatitude = temp;
                                }
                                break;
                            case 34:
                                if (double.TryParse(row[i], out temp))
                                {
                                    response.BoundingBox.EastLongitude = temp;
                                }
                                break;
                            case 35:
                                response.QueryParseValue = ParseQueryParseValues(row[i]);
                                break;
                            case 36:
                                var p = ParseGeocodePoints(row[i]);

                                if (p != null)
                                {
                                    response.GeocodePoint = p.ToArray();
                                }
                                break;
                            case 37:
                                entity.StatusCode = row[i];
                                resCnt--;
                                break;
                            case 38:
                                entity.FaultReason = row[i];
                                resCnt--;
                                break;
                            case 39:
                                entity.TraceId = row[i];
                                resCnt--;
                                break;
                            default:
                                break;
                        }

                        resCnt++;
                    }
                }
            }

            if (reqCnt > 0)
            {
                entity.GeocodeRequest = request;
            }

            if(resCnt > 0)
            {
                if (locs.Count > 0)
                {
                    response.Point = locs[0];
                }

                if (schemaVersion == 1)
                {
                    response.GeocodePoint = locs.ToArray();
                }

                entity.GeocodeResponse = new GeocodeResponse[1];
                entity.GeocodeResponse[0] = response;
            }

            if (revResCnt == 2)
            {
                entity.ReverseGeocodeRequest = reverseRequest;
            }

            return entity;
        }

        private static QueryParseValue[] ParseQueryParseValues(string row)
        {
            var o = queryParseSerializer.ReadObject(new MemoryStream(System.Text.Encoding.Unicode.GetBytes(row)));

            if (o != null)
            {
                return (QueryParseValue[])o;
            }

            return null;
        }

        private static GeocodePoint[] ParseGeocodePoints(string row)
        {
            var o = geocodePointSerializer.ReadObject(new MemoryStream(System.Text.Encoding.Unicode.GetBytes(row)));

            if (o != null)
            {
                return (GeocodePoint[])o;
            }

            return null;
        }

        #endregion
    }
}

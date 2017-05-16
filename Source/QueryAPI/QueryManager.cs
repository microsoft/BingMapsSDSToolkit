using BingMapsSDSToolkit.Internal;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BingMapsSDSToolkit.QueryAPI
{
    public class QueryManager
    {
        #region Public Methods

        /// <summary>
        /// Processes a query request. 
        /// </summary>
        /// <param name="request">A request class that derives from the BaseQueryRequest class.</param>
        /// <returns>A query response.</returns>
        public async Task<QueryResponse> ProcessQuery(FindByPropertyRequest request)
        {
            return await MakeRequest(request);
        }

        #endregion

        #region Private Methods

        private async Task<QueryResponse> MakeRequest(FindByPropertyRequest request)
        {
            var result = new QueryResponse();

            try
            {
                string urlRequest = request.GetRequestUrl();
                using (var responseStream = await ServiceHelper.GetStreamAsync(new Uri(urlRequest)))
                {
                    XDocument xmlDoc = XDocument.Load(responseStream);
                    string name;

                    foreach (XElement element in xmlDoc.Descendants(XmlNamespaces.Atom + "entry"))
                    {
                        var r = new QueryResult(){
                            EntityUrl = element.Element(XmlNamespaces.Atom + "id").Value,
                            Location = new GeoDataLocation()
                        };

                        XElement content = element.Element(XmlNamespaces.Atom + "content");

                        if (content != null && content.FirstNode != null)
                        {
                            XElement properties = (XElement)content.FirstNode;//.Element(XmlNamespaces.m + "properties");

                            if (properties != null)
                            {                                
                                foreach (var prop in properties.Descendants())
                                {
                                    name = prop.Name.LocalName;

                                    switch (name.ToLowerInvariant())
                                    {
                                        case "latitude":
                                            r.Location.Latitude = XmlUtilities.GetDouble(prop, 0);
                                            break;
                                        case "longitude":
                                            r.Location.Longitude = XmlUtilities.GetDouble(prop, 0);
                                            break;
                                        case "__distance":
                                            r.Distance = SpatialTools.ConvertDistance(XmlUtilities.GetDouble(prop, 0), DistanceUnitType.KM, request.DistanceUnits);
                                            break;
                                        case "__IntersectedGeom":
                                            var wkt = XmlUtilities.GetString(prop);
                                            if (!string.IsNullOrEmpty(wkt))
                                            {
                                                r.IntersectedGeography = new Geography()
                                                {
                                                    WellKnownText = wkt
                                                };
                                            }
                                            break;
                                        default:
                                            if (!r.Properties.ContainsKey(name))
                                            {
                                                var nVal = ParseNodeValue(prop);
                                                r.Properties.Add(name, nVal);

                                                if (nVal is Geography)
                                                {
                                                    r.HasGeography = true;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        result.Results.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private object ParseNodeValue(XElement node)
        {
            if (node.HasAttributes)
            {
                var type = node.Attribute(XmlNamespaces.DataServicesMetadata + "type");
                switch (type.Value)
                {
                    case "Edm.Double":
                        return XmlUtilities.GetDouble(node, 0);
                    case "Edm.Int64":
                        return XmlUtilities.GetInt64(node, 0);
                    case "Edm.Boolean":
                        return XmlUtilities.GetBoolean(node, false);
                    case "Edm.DateTime":
                        return XmlUtilities.GetDateTime(node);
                    case "Edm.Geography":
                        var wkt = XmlUtilities.GetString(node);
                        if (!string.IsNullOrEmpty(wkt))
                        {
                            return new Geography()
                            {
                                WellKnownText = wkt
                            };
                        }

                        return null;
                    case "Edm.String":
                    default:
                        return XmlUtilities.GetString(node);
                }
            }

            return XmlUtilities.GetString(node);
        }

        #endregion
    }
}

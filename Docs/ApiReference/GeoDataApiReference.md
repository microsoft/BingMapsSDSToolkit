# Geodata API Reference

**Namespace:** `BingMapsSDSToolkit.GeodataAPI`

This API provides access to geographical boundary data such as a country/region, admin division, cities, and postal code.

[MSDN API Documentation](https://msdn.microsoft.com/en-us/library/dn306801.aspx)

The following classes and enumerators are used by the GeoData API.

* [BoundaryEntityType Enumerator](#boundaryentitytype-enumerator)
* [Copyright Class](#copyright-class)
* [CopyrightSource Class](#copyrightsource-class)
* [GeodataManager Class](#geodatamanager-class)
* [GeodataPolygon Class](#geodatapolygon-class)
* [GeodataResponse Class](#geodataresponse-class)
* [GeodataResult Class](#geodataresult-class)
* [GeodataResultSet Class](#geodataresultset-class)
* [GetBoundaryRequest Class](#getboundaryrequest-class)
* [Metadata Class](#metadata-class)
* [Name Class](#name-class)
* [PointCompression Class](#pointcompression-class)
* [Primitive Class](#primitive-class)

<a name="BoundaryEntityType"></a>
## BoundaryEntityType Enumerator

The supported boundary entity types. Note that not all all entity types are available in all regions.

| Name | Description |
| ---- | ----------- |
| AdminDivision1 | First administrative level within the country/region level, such as a state or a province. |
| AdminDivision2 | Second administrative level within the country/region level, such as a county. |
| CountryRegion | Country or region |
| Neighborhood | A section of a populated place that is typically well-known, but often with indistinct boundaries. |
| PopulatedPlace | A concentrated area of human settlement, such as a city, town or village. |
| Postcode1 | The smallest post code category, such as a zip code. |
| Postcode2 | The next largest post code category after Postcode1 that is created by aggregating Postcode1 areas. |
| Postcode3 | The next largest post code category after Postcode2 that is created by aggregating Postcode2 areas. |
| Postcode4 | The next largest post code category after Postcode3 that is created by aggregating Postcode3 areas. |

## Copyright Class

Copyright information.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| CopyrightURL | String | The copyright URL for the Geodata service. |
| Sources | CopyrightSource\[\] | A collection of CopyrightSource objects that give information about the sources of the polygon data that is returned. |

## CopyrightSource Class

Copyright source information.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Copyright | String | The copyright string for the source. |
| SourceID | String | An ID identifying the data provider that supplied the data. |
| SourceName | String | The name of the data provider represented by this Source element. |

<a name="GeodataManager"></a>
## GeodataManager Class

A static class for retrieving boundary data from the Geodata API.

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetBoundary(GetBoundaryRequest request, String bingMapsKey) | Task\<List\<GeodataResult\>\> | Gets the boundary for the specified request. |

## GeodataPolygon Class

An object storing the parsed bundary infromation as sets for rings for a polygon.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| ExteriorRing | List\<GeodataLocation\> | A list of coordinates that make up the exterior ring of a polygon. |
| InnerRings | List\<List\<GeodataLocation\>\> | A list of list of coordinates that make up all the inner rings of a polygon. |

## GeodataResponse Class

An object that contains the response from the Geodata API.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| ResultSet | GeodataResultSet | The result set from the Geodata API. |

## GeodataResult Class

A Geodata boundary result.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Copyright | Copyright | Copyright information for the boundary data. |
| EntityID | String | A unique ID number associated with this entity. |
| EntityMetadata | Metadata | A collection of metadata information associated with the entity. |
| Name | Name | Information about the name of the boundary location. |
| Primitives | Primitive\[\] | An array of objects that contain the polygon information for the boundary. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetBestMapViewBox(out GeodataLocation NorthWest, out GeodataLocation SouthEast) | void | Parses the BestMapViewBox property into bounding coordinates. If the BestMapViewBox property is not returned in the Metadata, the bounding coordinates will be calculated from the boundary coordinates. |
| GetPolygons() | List\<GeodataPolygon\> | Parses all the Primitives into Polygon objects. |

## GeodataResultSet Class

A result set from the Geodata API.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Copyright | String | A copyright string. |
| Results | GeodataResult\[\] | An array of Geodata boundary results. |

<a name="GetBoundaryRequest"></a>
## GetBoundaryRequest Class

An object the contains the request information for retrieving a boundary.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Address | String | An address string that is geocoded by the service to get latitude and longitude coordinates. Note: This call will result in two individual usage transactions: RESTLocations (for geocoding) and RESTSpatialDataService:Geodata. |
| Coordinate | GeodataLocation | The coordinate to use to find intersecting boundary. If set, this will override the Address property when doing a search. |
| Culture | String | Specifies the preferred language to use for any metadata text about the entity or polygons. Defualt: en |
| EntityType | BoundaryEntityType | The entity type to return. Default: CountryRegion. Note that not all entity types are available for each location. |
| GetAllPolygons | Boolean | Specifies whether the response should include all of the boundary polygons for the requested entity or just return a single polygon that represents the main outline of the entity. |
| GetEntityMetadata | Boolean | Specifies whether the response should include metadata about the entity, such as AreaSqKm and others. |
| LevelOfDetail | Int32 | The level of detail for the boundary polygons returned. An integer between 0 and 3, where 0 specifies the coarsest level of boundary detail and 3 specifies the best. Default: 0. |
| PreferCuratedPolygons | Boolean | Prefer curated boundary polygons. Curated polygons have been optimized for display purposes. These polygons are typically clipped to the land, and can fall somewhere between medium and high fidelity. |
| UserRegion | String | Specifies the user’s home country or region. Default: US |

## Metadata Class

An object that contains metadata about a boundary. Not all fields will always be populated.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| AreaSqKm | String | The approximate total surface area (in square kilometers) covered by all the polygons that comprise this entity. |
| BestMapViewBox | String | An area on the Earth that provides the best map view for this entity. This area is defined as a  bounding box which represents an area using a set of latitude and longitude values: South Latitude,  East Longitude, North Latitude, East Longitude. |
| OfficialCulture | String | The culture associated with this entity. |
| PopulationClass | String | The approximate population within this entity. |
| RegionalCulture | String | The regional culture associated with this entity. |

## Name Class

An object that contains information about the boundary, such as the name and culture.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Culture | String | The culture of the region. |
| EntityName | String | The name of boundary. |
| SourceID | String | An ID identifying the data provider that supplied the data. |

## PointCompression Class

This compression algorithm encodes/decodes a collections of locations into a string. This algorithm is used for generating a compressed collection of locations for use with the Bing Maps REST Elevation Service. This algorithm is also used for decoding the compressed coordinates returned by the Geodata API. These algorithms come from the following documentation:

* http://msdn.microsoft.com/en-us/library/jj158958.aspx
* http://msdn.microsoft.com/en-us/library/dn306801.aspx

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| Encode(List\<GeodataLocation\> points) | String | Compresses a list of coordinates into a string. Based on: http://msdn.microsoft.com/en-us/library/jj158958.aspx |
| TryDecode(String value, out List\<GeodataLocation\> parsedValue) | Boolean | Decodes a collection of coordinates from a compressed string. Based on: http://msdn.microsoft.com/en-us/library/dn306801.aspx |

## Primitive Class

An object that stores the information for a single polygon in the boundary.

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| NumPoints | String | The number of vertex points used to define the polygon. |
| PrimitiveID | String | A unique ID associated with this polygon primitive. |
| Shape | String | A comma-delimited sequence starting with the version number of the polygon set followed by a list of compressed polygon “rings” (closed paths represented by sequences of latitude and-longitude points) ordered as follows: <br/><br/>[version],[compressed polygon ring 1],[compressed polygon ring 2],...,[compressed polygon ring n]<br/><br/>See the Decompression Algorithm below for code to use to retrieve the polygon points from the compressed strings. |
| SourceID | String | An ID identifying the data provider that supplied the data. This ID number will reference one of the sources listed in the CopyrightSources collection attached to the parent entity . |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetPolygon() | GeodataPolygon | Parses the encoded Shape into a Polygon that consists of a list of coordinates that make up the exterior ring of the polygon and list of inner rings. |

# Query API Reference

**Namespace:** `BingMapsSDSToolkit.QueryAPI`

This API provides the ability to perform spatial queries against data sources hosted in the Bing Spatial Data Services.

[MSDN API Documentation](https://msdn.microsoft.com/en-us/library/gg585126.aspx)

## AvoidType Enumerator

Specifies the road types to minimize or avoid when the route is created for the driving travel mode.

| Name | Description |
| ---- | ----------- |
| Highways | Avoids the use of highways in the route. |
| MinimizeHighways | Minimizes (tries to avoid) the use of highways in the route. |
| MinimizeTolls | Minimizes (tries to avoid) the use of toll roads in the route. |
| Tolls | Avoids the use of toll roads in the route. |

## CompareOperator Enumerator

Compare Operators that can be used with filters.

| Name | Description |
| ---- | ----------- |
| And | Logical `and`<br/>**Note:** Not supported when combined with StartsWith or EndsWith wildcard searches. |
| Or | Logical `or`<br/>**Note:** Not supported when combined with StartsWith or EndsWith wildcard searches. |

<a name="FilterExpression"></a>
## FilterExpression Class

> Inherits from the IFilter interface.

An object that defines a filter expression.

### Constructor

> `FilterExpression()` 

> `FilterExpression(String propertyName, LogicalOperator logicalOperator, Object value)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Operator | LogicalOperator | The logical operator to use when comparing filtering against the filter value. |
| PropertyName | String | The name of the property to filter against. **Note:** You cannot filter on the latitude and longitude entity properties. |
| Value | Object | The value to compare against. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| ToString() | String | Converts the filter expression to string. |

<a name="FilterGroup"></a>
## FilterGroup Class

> Inherits from the IFilter interface.

A group of filter objects and the compare operator to use against them.

### Constructor

> `FilterGroup()` 

> `FilterGroup(IFilter\[\] filters, CompareOperator compareOperator)` 

> `FilterGroup(IFilter\[\] filters, CompareOperator compareOperator, Boolean not)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| CompareOperator | CompareOperator | The comparison operator to use with the filters. |
| Filters | IFilter\[\]  | An array of filters expressions. |
| Not | String | A boolean indicating if this filter should not make or not. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| ToString() | String | Converts the filter group to string. |

<a name="FindByPropertyRequest"></a>
## FindByPropertyRequest Class

> Inherits from the BasicDataSourceInfo class.

A query that searches by property. This is also the basis to all other queries.

### Constructor

> `FindByPropertyRequest()` 

> `FindByPropertyRequest(BasicDataSourceInfo info)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| DistanceUnits | DistanceUnitType | The distance units to use with the query. |
| Filter | IFilter | A filter that specifies conditional expressions for a list of properties and values. |
| InlineCount | Boolean | A boolean that specifies whether or not to return a count of the results in the response. Default: false. |
| IsStaging | Boolean | A boolean value indicating if the staging version of the data source should be accessed or not. |
| Orderby | String | A comma delimited list of data source properties to use to sort the results. (up to 3 properties can be specified). |
| Select | String | A string that contains a comma delimited list of data source properties to return. |
| Skip | Int32 | Specifies the number of results to skip. This lets you page through the results. |
| Top | Int32 | An integer value between 1 and 250 that represents the maximium results that can be returned. Default 25. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetRequestUrl() | String | Gets a URL to query the Bing Spatial Data Services. |

<a name="FindInBoundingBoxRequest"></a>
## FindInBoundingBoxRequest Class

> Inherist from the FindByProperty class.

A search request that looks for locations that are inside a bounding box.

### Constructor

> `FindInBoundingBoxRequest()` 

> `FindInBoundingBoxRequest(BasicDataSourceInfo info)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| East | Double | The eastern most longitude of the bounding box. Value between -180 and 180. |
| North | Double | The northern most latitude of the bounding box. Value between -90 and 90. |
| South | Double | The southern most latitude of the bounding box. Value between -90 and 90. |
| West | Double | The western most longitude of the bounding box. Value between -180 and 180. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetRequestUrl() | String | Retrieves the reuqest URL for the query. |

<a name="FindNearByRequest"></a>
## FindNearByRequest Class

> Inherist from the FindByProperty class.

A radial search request for finding results that are near a location.

### Constructor

> `FindNearByRequest()` 

> `FindNearByRequest(BasicDataSourceInfo info)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Address | String | A string address to perform a nearby search around. If the Center properties is specified it will override this value when doing the search. |
| Center | GeodataLocation | A centeral coordinate to perform the nearby search. Overrides the Address value if both are specified. |
| Distance | Double | The radial search distance. You must specify the distance in between 0.16 and 1000 kilometers. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetRequestUrl() | String | Retrieves the reuqest URL for the query. |

<a name="FindNearRouteRequest"></a>
## FindNearRouteRequest Class

> Inherist from the FindByProperty class.

A search query that looks for locations that are within 1 mile or 1.6 kilometers of a route.

### Constructor

> `FindNearRouteRequest()` 

> `FindNearRouteRequest(BasicDataSourceInfo info)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Avoid | AvoidType\[\] | Specifies the road types to minimize or avoid when the route is created for the driving travel mode. |
| DistanceBeforeFirstTurn | Int32 | Specifies the distance before the first turn is allowed in the route. This option only applies to the driving travel mode. |
| EndAddress | String | A string address to use as the end location of the route. If the EndLocation properties is specified it will override this value when doing the search. |
| EndLocation | GeodataLocation | A coordinate to use as the end location of the route. Overrides the EndAddress value if both are specified. |
| Heading | Int32 | Specifies the initial heading for the route. An integer value between 0 and 359 that represents degrees from north where north is 0 degrees and the heading is specified clockwise from north. For example, setting the heading of 270 degrees creates a route that initially heads west. |
| Optimize | RouteOptimizationType | Specifies what parameters to use to optimize the route on the map. Default: Time. |
| StartAddress | String | A string address to use as the starting location of the route. If the StartLocation properties is specified it will override this value when doing the search. |
| StartLocation | GeodataLocation | A coordinate to use as the starting location of the route. Overrides the StartAddress value if both are specified. |
| TravelMode | TravelModeType | The mode of travel for the route. Default: Driving. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetRequestUrl() | String | Retrieves the reuqest URL for the query. |

## IFilter Interface

A filter interface.

<a name="IntersectionSearchRequest"></a>
## IntersectionSearchRequest Class

> Inherist from the FindByProperty class.

A search query which looks to see if and locations intersect the specified Geography.

### Constructor

> `IntersectionSearchRequest()` 

> `IntersectionSearchRequest(BasicDataSourceInfo info)` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Geography | Geography | The Geography to do an intersection test against. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetRequestUrl() | String | Retrieves the reuqest URL for the query. |

<a name="LogicalOperator"></a>
## LogicalOperator Enumerator

The logical operators that can be used by a filter expression.

| Name | Description |
| ---- | ----------- |
| EndsWith | Finds all property values that end with a specified string value. Not supported with And or Or comparison operators. Is not supported for NavteqNA and NavteqEU data sources. |
| Eq | Equal |
| Ge | Greater than or equal |
| Gt | Greater than |
| IsIn | Finds all properties who's value is within a list of values. |
| Le | Less than or equal |
| Lt | Less than |
| Ne | Not equal |
| StartsWith | Finds all property values that start with a specified string value. Not supported with And or Or comparison operators. Is not supported for NavteqNA and NavteqEU data sources. |

<a name="QueryManager"></a>
## QueryManager Class

A static class for processing queries.

### Static Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| ProcessQuery(FindByPropertyRequest request) | Task\<QueryResponse\> | Processes a query request. |

## QueryResponse Class

An object that represents the response from a query.

### Constructor

> `QueryResponse()` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| ErrorMessage | String | A string containing an error message if an error occurs while processing the query. |
| Results | List\<QueryResult\> | An array of query results. |

## QueryResult Class

An object that represents a single entity result in the query.

### Constructor

> `QueryResult()` 

### Properties

| Name | Type | Description |
| ---- | ---- | ----------- |
| Distance | Double | The distance the result is from the search query. The distance value will be in the specified distance units or will default to KM. This value is only set if the request is of type FindNearby, FindInBoundingBox, or FindNearRoute. |
| EntityUrl | String | A url that points to this entity in the data source. |
| HasGeography | Boolean | Indicates if the result has a property of type Geography or not. |
| IntersectedGeography | Geography | The intersected section of the Geography |
| Location | GeodataLocation | The location coordinate of the result. |
| Properties | Dictionary\<string, object\> | A dictionary of additional properties this entity has. |

### Methods

| Name | Return Type | Description |
| ---- | ----------- | ----------- |
| GetGeography() | Geography | Gets first property of type geography for the result. |

## RouteOptimizationType Enumerator

Specifies what parameters to use to optimize the route on the map.

| Name | Description |
| ---- | ----------- |
| Distance | Optimizes route for shortest distance. |
| Time | Optimizes route for shortst travel time. |
| TimeWithTraffic | Optimizes route for shortst travel time with respect to current traffic conditions. |

## TravelModeType Enumerator

The mode of travel for the route.

| Name | Description |
| ---- | ----------- |
| Driving | Driving mode |
| Walking | Walking Mode |
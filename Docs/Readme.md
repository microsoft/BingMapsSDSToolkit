# Bing Maps Spatial Data Services Toolkit Documentation

The Bing Maps Spatial Data Services Toolkit provides a set of tools for easily integrating with the following API's;

* [Data Source API](#DataSourceAPI) - This API provides the ability to upload and manage data sources in the Bing Spatial Data Services.
* [Geocode Dataflow API](#GeocodeDataflowAPI) - This API provides the ability to batch forward or reverse geocode up to 200,000 entities in a single request.
* [Geodata API](#GeodataAPI) - This API provides access to geographical boundary data such as a country/region, admin division, cities, and postal code.
* [Query API](#QueryAPI) - This API provides the ability to perform spatial queries against data sources hosted in the Bing Spatial Data Services.

All of these API's have a manager class in the toolkit which is used to process requests to these services.

<a name="DataSourceAPI"></a>
## Data Source API

This API provides the ability to upload and manage data sources in the Bing Spatial Data Services. Documentation for the underlying service can be found [here](https://msdn.microsoft.com/en-us/library/gg585132.aspx).

**Main Classes**

* [DataSource Class](ApiReference/DataSourceApiReference.md#DataSource)
* [DataSourceManager Class](ApiReference/DataSourceApiReference.md#DataSourceManager)

### Example: Get all recent and current jobs

This example shows how to get all recent and current jobs for an account.

```csharp
//Create an instance of the data source manager.
var dataSourceManager = new DataSourceManager();

//Get all recent and current jobs for your account.
var jobs = await dataSourceManager.GetJobList("YourBingMapsKey");
```

### Example: Get all public data sources

This example shows how to a list of all public data sources available in the Bing Spatial Data Services.

```csharp
//Create an instance of the data source manager.
var dataSourceManager = new DataSourceManager();

//Get all public data sources.
var ds = await dataSourceManager.GetAllPublicDataSources("YourBingMapsKey");
```

### Example: Get all data sources in account

This example shows how to data sources linked to an account.

```csharp
//Create an instance of the data source manager.
var dataSourceManager = new DataSourceManager();

//Get all data sources in an account.
var ds = await dataSourceManager.GetDetails("YourBingMapsKey");
```

### Example: Get details of a single data source

This example shows how to access the details of a single data source.

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _datasourceApiDataSourceSettings = new BasicDataSourceInfo()
{
	MasterKey = "YourBingMapsKey",
	DataSourceName = "MyDataSource",
	EntityTypeName = "MyEntity",
	AccessId = "12fads35asg6a43"	
};

//Create an instance of the data source manager.
var dataSourceManager = new DataSourceManager();

//Get details of a data source.
var ds = await dataSourceManager.GetDetails(_datasourceApiDataSourceSettings);
```

### Example: Make a data source public

This example shows how to change the settings of a data source to make it publicly available.

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _datasourceApiDataSourceSettings = new BasicDataSourceInfo()
{
	MasterKey = "YourBingMapsKey",
	DataSourceName = "MyDataSource",
	EntityTypeName = "MyEntity",
	AccessId = "12fads35asg6a43"	
};

//Create an instance of the data source manager.
var dataSourceManager = new DataSourceManager();

//Set the data source public setting to true.
var job = await dataSourceManager.SetPublicSetting(_datasourceApiDataSourceSettings, true);
```

### Example: Read a data source file and geocode it

This example shows how to read a local data source file and geocode it.

```csharp
using (var fileStream = new FileStream("c:\\files\datasource.xml", FileMode.Open, FileAccess.Read))
{
	var ds = new DataSource();	
	if (await ds.ReadAsync(fileStream, DataSourceFormat.XML))
	{
		//Monitor the progress of the batch geocoding job.
		ds.GeocodeStatusChanged += (msg) =>
		{			
			Console.WriteLine(msg);
		};
		
		//Geocode the data source.
		var r = await ds.Geocode("YourBingMapsKey");
		
		//Do something with the geocoded data source.
	}
}
```

### Example: Validate a data source

This example shows how to validate a data source. This performs several local checks of a data source to ensure that it aligns with requirements of the Bing Spatial Data Services. This can save transactions being wasted on jobs that fail due to invalid data sources being uploaded. 

```csharp
using (var fileStream = new FileStream("c:\\files\datasource.xml", FileMode.Open, FileAccess.Read))
{
	var ds = new DataSource();	
	if (await ds.ReadAsync(fileStream, DataSourceFormat.XML))
	{
		//Validate the data source.
		var validation = await ds.Validate();
		
		//If there are errors, write them to the console.
		if (validation.Errors != null && validation.Errors.Count > 0)
		{
			foreach (var er in validation.Errors)
			{
				Console.WriteLine(er);
			}
		} else {
			//Do something with the validated data source.
		}
	}
}
```

### Example: Upload a data source

This example shows how to upload a data source and will remove any locations that it wasn't able to geocode.

```csharp
using (var fileStream = new FileStream("c:\\files\datasource.xml", FileMode.Open, FileAccess.Read))
{
	var ds = new DataSource();	
	if (await ds.ReadAsync(fileStream, DataSourceFormat.XML))
	{
		var dataSourceManager = new DataSourceManager();
		
		//Monitor the progress of the upload process.
		dataSourceManager.UploadStatusChanged += (msg) =>
		{
			Console.WriteLine(msg);
		};
		
		//Geocode the data source if you havent already.
		var r = await ds.Geocode("YourBingMapsKey");
		
		//Upload the data source.
		var dataflow = await dataSourceManager.Upload(ds, LoadOperation.Complete, false, true);
	}
}
```

### Example: Download a data source and save to a file

This example shows how to download a data source and save it as an XML file that uses the Bing Spatial Data Services XML data source file format schema.

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _datasourceApiDataSourceSettings = new BasicDataSourceInfo()
{
	MasterKey = "YourBingMapsKey",
	DataSourceName = "MyDataSource",
	AccessId = "12fads35asg6a43"	
};

using (var fileStream = new FileStream("c:\\files\datasource.xml", FileMode.Open, FileAccess.Write))
{
	//Create an instance of the data source manager.
	var dataSourceManager = new DataSourceManager();

	//Download a data sources.
	var ds = await dataSourceManager.Download(_datasourceApiDataSourceSettings);

	//Convert the data source to a XML string.
	await ds.WriteAsync(fileStream, DataSourceFormat.XML);
}
```

### Example: Delete a data source

This example shows how to delete a data source.

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _datasourceApiDataSourceSettings = new BasicDataSourceInfo()
{
	MasterKey = "YourBingMapsKey",
	DataSourceName = "MyDataSource",
	AccessId = "12fads35asg6a43"	
};

//Create an instance of the data source manager.
var dataSourceManager = new DataSourceManager();

//Make a request to delete a data source.
var successful = await dataSourceManager.DeleteDataSource(_datasourceApiDataSourceSettings);
```

<a name="GeocodeDataflowAPI"></a>
## Geocode Dataflow API

This API provides the ability to batch forward or reverse geocode up to 200,000 entities in a single request. Documentation for the underlying service can be found [here](https://msdn.microsoft.com/en-us/library/ff701733.aspx).

**Main Classes**

* [BatchFileFormat Enumerator](ApiReference/GeocodeDataflowApiReference.md#BatchFileFormat)
* [BatchGeocodeManager Class](ApiReference/GeocodeDataflowApiReference.md#BatchGeocodeManager)
* [GeocodeEntity Class](ApiReference/GeocodeDataflowApiReference.md#GeocodeEntity)
* [GeocodeFeed Class](ApiReference/GeocodeDataflowApiReference.md#GeocodeFeed)
* [GeocodeRequest Class](ApiReference/GeocodeDataflowApiReference.md#GeocodeRequest)
* [ReverseGeocodeRequest Class](ApiReference/GeocodeDataflowApiReference.md#ReverseGeocodeRequest)

### Example: Batch geocode local data

This example shows how to create a geocode feed from local data and batch geocode it.

```csharp
//Create a geocode feed to process.
var geocodeFeed = new GeocodeFeed()
{
	Entities = new List<GeocodeEntity>()
	{
		new GeocodeEntity("New York, NY"),
		new GeocodeEntity("Seattle, WA")
	}
};

var geocodeManager = new BatchGeocodeManager();

//Process the request.
var r = await geocodeManager.Geocode(geocodeFeed, "YourBingMapsKey");

//Do something with the response.
```

### Example: Batch geocode a file

This example shows how to read a local XML file that formatted as a geocode feed and batch geocode it.

```csharp
//Read a local file that in in the required geocode feed format.
using (var fileStream = new FileStream("c:\\files\geocodeFile.xml", FileMode.Open, FileAccess.Read))
{
	//Create a geocode feed from the file stream to process.
	var geocodeFeed = GeocodeFeed.ReadAsync(fileStream);

	var geocodeManager = new BatchGeocodeManager();

	//Process the request.
	var r = await geocodeManager.Geocode(geocodeFeed, "YourBingMapsKey");

	//Do something with the response.
}
```

<a name="GeodataAPI"></a>
## Geodata API

This API provides access to geographical boundary data such as a country/region, admin division, cities, and postal code. Documentation for the underlying service can be found [here](https://msdn.microsoft.com/en-us/library/dn306801.aspx).

**Main Classes**

* [BoundaryEntityType Enumerator](ApiReference/GeoDataApiReference.md#BoundaryEntityType)
* [GeodataManager Class](ApiReference/GeoDataApiReference.md#GeodataManager)
* [GetBoundaryRequest Class](ApiReference/GeoDataApiReference.md#GetBoundaryRequest)

### Example: Retrieve city boundary

This example shows how to retrive a city boundary that intersects with (47.599751, -122.334709) which is in "Seattle, WA".

```csharp
var request = new GetBoundaryRequest()
{               
	EntityType = BoundaryEntityType.PopulatedPlace,
	LevelOfDetail = 1,
	GetAllPolygons = true,
	GetEntityMetadata = true,
	Coordinate = new GeodataLocation(47.599751, -122.334709);
};

//Process the request.
var response = await GeodataManager.GetBoundary(request, "YourBingMapsKey");

//Do something with the response.
```

<a name="QueryAPI"></a>
## Query API

This API provides the ability to perform spatial queries against data sources hosted in the Bing Spatial Data Services.Documentation for the underlying service can be found [here](https://msdn.microsoft.com/en-us/library/gg585126.aspx).

**Main Classes**

* [FilterExpression Class](ApiReference/QueryApiReference.md#FilterExpression)
* [FilterGroup Class](ApiReference/QueryApiReference.md#FilterGroup)
* [FindByPropertyRequest Class](ApiReference/QueryApiReference.md#FindByPropertyRequest)
* [FindInBoundingBoxRequest Class](ApiReference/QueryApiReference.md#FindInBoundingBoxRequest)
* [FindNearByRequest Class](ApiReference/QueryApiReference.md#FindNearByRequest)
* [FindNearRouteRequest Class](ApiReference/QueryApiReference.md#FindNearRouteRequest)
* [IntersectionSearchRequest Class](ApiReference/QueryApiReference.md#IntersectionSearchRequest)
* [QueryManager Class](ApiReference/QueryApiReference.md#QueryManager)

### Example: Find by property

This example searchs for all results that has an "entityId" column value of "1", "2", or "3".

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _queryApiDataSourceSettings = new BasicDataSourceInfo()
{
	QueryURL = "https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops",
	QueryKey = "YourBingMapsKey"
};

//Create a request.
var request = new FindByPropertyRequest(_queryApiDataSourceSettings)
{
	Filter = new FilterExpression("entityId", LogicalOperator.IsIn, new List<string>(){ "1", "2", "3"})
};

//Process the request.
var response = await QueryManager.ProcessQuery(request);

//Do something with the response.
```

### Example: Find nearby 

This example searches for results that are within 45 kilometers of "Seattle, WA".

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _queryApiDataSourceSettings = new BasicDataSourceInfo()
{
	QueryURL = "https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops",
	QueryKey = "YourBingMapsKey"
};

//Create a request.
var request = new FindNearByRequest(_queryApiDataSourceSettings)
{
	Distance = 45,
	DistanceUnits = DistanceUnitType.Kilometers,
	request.Address = "Seattle, WA"
};

//Process the request.
var response = await QueryManager.ProcessQuery(request);

//Do something with the response.
```

### Example: Find in bounding box

This example searches for results that are in a bounding box.

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _queryApiDataSourceSettings = new BasicDataSourceInfo()
{
	QueryURL = "https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops",
	QueryKey = "YourBingMapsKey"
};

//Create a request.
var request = new FindInBoundingBoxRequest(_queryApiDataSourceSettings)
{
	North = 45,
	South = 40,
	West = -110,
	East = -100
};

//Process the request.
var response = await QueryManager.ProcessQuery(request);

//Do something with the response.
```

### Example: Find along route

This example searches for results that are within 1 mile of a driving route that is optimized for time with traffic between "Seattle, WA" and "Redmond, WA".

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _queryApiDataSourceSettings = new BasicDataSourceInfo()
{
	QueryURL = "https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops",
	QueryKey = "YourBingMapsKey"
};

//Create a request.
var request = new FindNearRouteRequest(_queryApiDataSourceSettings)
{
	DistanceUnits = DistanceUnitType.Miles,
	TravelMode = TravelModeType.Driving,
	Optimize = RouteOptimizationType.TimeWithTraffic,
	StartAddress = "Seattle, WA",
	EndAddress = "Redmond, WA"
};

//Process the request.
var response = await QueryManager.ProcessQuery(request);

//Do something with the response.
```

### Example: Intersection search

This example finds all results that intersect with a point geometry at (45, -110).

```csharp
//Specify the data source to query and the Bing Maps key to use to access the data source.
var _queryApiDataSourceSettings = new BasicDataSourceInfo()
{
	QueryURL = "https://spatial.virtualearth.net/REST/v1/data/20181f26d9e94c81acdf9496133d4f23/FourthCoffeeSample/FourthCoffeeShops",
	QueryKey = "YourBingMapsKey"
};

//Create a request.
var request = new IntersectionSearchRequest(_queryApiDataSourceSettings)
{
	Geography = new Geography()
	{
		WellKnownText = "POINT(-110 45)"
	}
};

//Process the request.
var response = await QueryManager.ProcessQuery(request);

//Do something with the response.
```
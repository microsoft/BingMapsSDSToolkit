![Bing Maps Logo](https://github.com/Microsoft/Bing-Maps-V8-TypeScript-Definitions/blob/master/images/BingMapsLogoTeal.png)

[![NuGet](https://img.shields.io/badge/NuGet-1.0.5-blue.svg)](https://www.nuget.org/packages/BingMapsSDSToolkit)
[![license](https://img.shields.io/badge/license-MIT-yellow.svg)](LICENSE)

# Bing Maps Spatial Data Services (SDS) Toolkit for .NET

This is a portable .NET class library which provides a set of tools that make it easy to access the Bing Maps Spatial Data Services in .NET based apps. The Bing MAps Spatial Data Services provides the following key functionalities:

* Batch forward and reverse Geocoding of up to 200,000 rows of data in a single request.
* Ability to upload a two dimensional table of data as a data source and expose it as a spatial REST service. 
* Access public data sources that contain informaiton such as points of interest and census data.
* Perform spatial queries against data sources using:
	* Find Nearby (radial)
	* Find in bounding box
	* Find by Property
	* Find along a route
	* Intersection search (i.e. search within a custom shape.)
* Access to adminstrative boundary data via the GeoData API. Boundary data types available vary from country to country. Supported boundary types; zip/postal codes, neighbourhoods, cities, counties, states/provinces, countries, and continents.

If you are working with the Bing Maps REST services which provides; ondemand forward and reverse geocoding, routing, static imagery, traffic and elevation data, take a look at the [Bing Maps REST Toolkit](https://github.com/Microsoft/BingMapsRESTToolkit).

This toolkit tries to align with the naming conventions used in the [documentation](https://msdn.microsoft.com/en-us/library/ff701734.aspx) for the Bing Spatial Data Services as much as possible. Additionaly, the [MainWindow.xaml.cs](https://www.nuget.org/packages/BingMapsSDSToolkit/master/Samples/MainWindow.xaml.cs) file of the BingSDSTestApp contains code samples on how to use all features of this toolkit.

This project makes use of the [Microsoft Compression](https://www.nuget.org/packages/Microsoft.Bcl.Compression) package.

## NuGet Package

The Bing Maps Spatial Data Services Toolkit is available as a [NuGet package](https://www.nuget.org/packages/BingMapsSDSToolkit). If using Visual Studio, open the nuget package manager, select the Browse tab and search for "Bing Maps SDS". This should reduce the list of results enough to find the "BingMapsSDSToolkit" package. The owner of the package is bingmaps and the author is Microsoft.

Alternatively, if you are using the nuget command line:

> PM&gt; Install-Package BingMapsSDSToolkit

## Supported Platforms

* .NET Framework 4.5+ 
* ASP.NET Core 1.0
* Universal Windows Platform (UWP) 
* Windows 10
* Windows 8.1
* Windows 8
* Windows Phone 10
* Windows Phone 8.1
* Xamarin.Android
* Xamarin.iOS
* Xamarin.iOS (Classic)

## Features

### Data Source API:

* Can read compressed file streams.
* Automatically compresses data before uploading.
* Supports uploading of delimited (pipe, tab, comma), XML (as per SDS schema), KML, KMZ and SHP files.
* Local Data Source validation. Reduces the number of invalid data sources that are uploaded, thus reduce the number of wasted SDS jobs that are created.
* Better support for delimited files. 
	* Schema text not required.
	* Handles quoted column header values.
	* Handles extra delimiters in schema text line.
* Wraps 99% of functionalities (no support for downloading the data schema of a data source as it isn't needed by this library).

### Geocode Data Flow API:

* Forward and reverse geocode up to 200,000 entities in a single request.
* Combines like addresses are combined as a single request. This allows rows with the same addresses to only create a single row in the geocode process. This helps to maximize the 1M free batch geocode limit.

### GeoData API:

* Search for boundary data such as zip/postal codes, neighbourhoods, cities, counties, states/provinces, countries, and continents.
* Provides compression tools for handling the encoded polygon data,

### Query API:

* Classes for easily creating queries for a data source. 
* Parses response from any data source into a common QueryResult object.
* Support for multiple distance units (km, miles, feet, meters, yards)
* Support for filters.
* Handles encoding filter values, and escapes single quotes by using the OData convention of using two single quotes side by side.

## Contributing

We welcome contributions. Feel free to file issues and pull requests on the repo and we'll address them as we can. Learn more about how you can help on our [Contribution Rules & Guidelines](CONTRIBUTING.md). 

You can reach out to us anytime with questions and suggestions using our communities below:
* [MSDN Forums](https://social.msdn.microsoft.com/Forums/en-US/home?forum=bingmapsajax&filter=alltypes&sort=lastpostdesc)
* [StackOverflow](http://stackoverflow.com/questions/tagged/bing-maps)

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## TODO List

The following is a list of tasks or ideas to do on this project.

* Add support for XLSX.
* Get culture for geocoding from country property
* Option to download existing data source when geocoding, pull coordinates of already geocoded rows if address and id have not changed.

## License

MIT
 
See [License](LICENSE.md) for full license text.

## Additional Resources

* [Bing Maps Spatial Data Services MSDN Documentation](https://msdn.microsoft.com/en-us/library/ff701734.aspx)
* [Bing Maps REST Toolkit](https://github.com/Microsoft/BingMapsRESTToolkit)
* [Bing Maps MSDN Documentation](https://msdn.microsoft.com/en-us/library/dd877180.aspx)
* [Bing Maps Blog](http://blogs.bing.com/maps)
* [Bing Maps forums](https://social.msdn.microsoft.com/Forums/en-US/home?forum=bingmapsajax&filter=alltypes&sort=lastpostdesc)
* [Bing Maps for Enterprise site](https://www.microsoft.com/maps/)


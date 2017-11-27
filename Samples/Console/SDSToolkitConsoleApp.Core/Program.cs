using BingMapsSDSToolkit.GeocodeDataflowAPI;
using System;
using System.Collections.Generic;

namespace SDSToolkitConsoleApp.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //This sample shows how to perform a back geocode request from a console app.

            var bingMapsKey = "YOUR_BING_MAPS_KEY";

            var geocodeFeed = new GeocodeFeed()
            {
                Entities = new List<GeocodeEntity>()
                {
                    new GeocodeEntity("New York, NY"),
                    new GeocodeEntity("Seattle, WA")
                }
            };

            Console.WriteLine(string.Format("Creating batch geocode job consisting of {0} entities.\n", geocodeFeed.Entities.Count));

            var geocodeManager = new BatchGeocodeManager();
            var r = geocodeManager.Geocode(geocodeFeed, bingMapsKey).GetAwaiter().GetResult();

            if (!string.IsNullOrEmpty(r.Error))
            {
                Console.WriteLine("Error: " + r.Error);
            }
            else
            {
                Console.WriteLine("Batch geocode job complete:\n");

                if (r.Succeeded != null && r.Succeeded.Entities != null)
                {
                    Console.WriteLine(string.Format("Succeeded: {0}\n", r.Succeeded.Entities.Count));

                    Console.WriteLine("Query\tLatitude\tLongitude\n----------------------------------");
                    foreach (var e in r.Succeeded.Entities)
                    {
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}", e.GeocodeResponse[0].Name, e.GeocodeResponse[0].GeocodePoint[0].Latitude, e.GeocodeResponse[0].GeocodePoint[0].Longitude));
                    }
                }

                if (r.Failed != null && r.Failed.Entities != null)
                {
                    Console.WriteLine(string.Format("Failed: {0}\n", r.Failed.Entities.Count));

                    Console.WriteLine("Query\n-----------");
                    foreach (var e in r.Failed.Entities)
                    {
                        Console.WriteLine(e.GeocodeRequest.Query);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}

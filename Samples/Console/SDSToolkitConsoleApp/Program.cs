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
using System.Collections.Generic;
using System;

namespace SDSToolkitConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //This sample shows how to perform a back geocode request from a console app.

            var bingMapsKey = System.Configuration.ConfigurationManager.AppSettings.Get("BingMapsKey");
                        
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

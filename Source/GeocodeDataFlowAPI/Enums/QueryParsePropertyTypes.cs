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


namespace BingMapsSDSToolkit.GeocodeDataflowAPI
{
    /// <summary>
    /// The different property names of a QueryParse object.
    /// </summary>
    public static class QueryParsePropertyTypes
    {
        /// <summary>
        /// A string specifying the street line of an address. The AddressLine property is the most precise, 
        /// official line for an address relative to the postal agency that services the area specified by the 
        /// Locality, PostalTown, or PostalCode properties.
        /// </summary>
        public const string AddressLine = "AddressLine";

        /// <summary>
        /// A string specifying the populated place for the address. This commonly refers to a city, but may 
        /// refer to a suburb or a neighborhood in certain countries.
        /// </summary>
        public const string Locality = "Locality";

        /// <summary>
        /// A string specifying the subdivision name within the country or region for an address. 
        /// This element is also commonly treated as the first order administrative subdivision; but in some cases, 
        /// it is the second, third, or fourth order subdivision within a country, a dependency, or a region.
        /// </summary>
        public const string AdminDistrict = "AdminDistrict";

        /// <summary>
        /// A string specifying the higher level administrative subdivision used in some countries or regions.
        /// </summary>
        public const string AdminDistrict2 = "AdminDistrict2";

        /// <summary>
        /// A string specifying the post code, postal code, or ZIP Code of an address.
        /// </summary>
        public const string PostalCode = "PostalCode";

        /// <summary>
        /// A string specifying the country or region name of an address.
        /// </summary>
        public const string CountryRegion = "CountryRegion";

        /// <summary>
        /// A string specifying a landmark associated with an address.
        /// </summary>
        public const string Landmark = "Landmark";
    }
}

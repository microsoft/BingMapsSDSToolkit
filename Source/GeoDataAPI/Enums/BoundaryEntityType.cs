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


namespace BingMapsSDSToolkit.GeodataAPI
{
    /// <summary>
    /// The supported boundary entity types. Note that not all all entity types are available in all regions.
    /// </summary>
    public enum BoundaryEntityType
    {
        /// <summary>
        /// First administrative level within the country/region level, such as a state or a province.
        /// </summary>
        AdminDivision1,

        /// <summary>
        /// Second administrative level within the country/region level, such as a county.
        /// </summary>
        AdminDivision2,

        /// <summary>
        /// Country or region
        /// </summary>
        CountryRegion,
        
        /// <summary>
        /// A section of a populated place that is typically well-known, but often with indistinct boundaries. 
        /// </summary>
        Neighborhood,

        /// <summary>
        /// A concentrated area of human settlement, such as a city, town or village.
        /// </summary>
        PopulatedPlace,

        /// <summary>
        /// The smallest post code category, such as a zip code. 
        /// </summary>
        Postcode1,

        /// <summary>
        /// The next largest post code category after Postcode1 that is created by aggregating Postcode1 areas. 
        /// </summary>
        Postcode2,

        /// <summary>
        /// The next largest post code category after Postcode2 that is created by aggregating Postcode2 areas.
        /// </summary>
        Postcode3,

        /// <summary>
        /// The next largest post code category after Postcode3 that is created by aggregating Postcode3 areas.
        /// </summary>
        Postcode4
    }
}

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

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// The data format of a data source. Note data sources should follow the documented file schema.
    /// </summary>
    public enum DataSourceFormat
    {
        /// <summary>
        /// Comma seperated (comma delimited) file format.
        /// </summary>
        CSV,

        /// <summary>
        /// Pipe (|) delimited file format.
        /// </summary>
        PIPE,

        /// <summary>
        /// Tab delimited file format.
        /// </summary>
        TAB,

        /// <summary>
        /// XML file that matches the schema required for Bing Spatial Data Services data sources.
        /// </summary>
        XML,

        /// <summary>
        /// A file in KML or KMZ format.
        /// </summary>
        KML,

        /// <summary>
        /// A file in the ESRI Shapefile format.
        /// </summary>
        SHP
    }
}

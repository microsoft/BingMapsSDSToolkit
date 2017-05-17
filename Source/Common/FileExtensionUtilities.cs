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

using BingMapsSDSToolkit.DataSourceAPI;
using BingMapsSDSToolkit.GeocodeDataflowAPI;

namespace BingMapsSDSToolkit
{
    /// <summary>
    /// Useful utilities for working with file extensions.
    /// </summary>
    public static class FileExtensionUtilities
    {
        /// <summary>
        /// Give a data source format, this method provides a default and file extension filter string. 
        /// </summary>
        /// <param name="format">The data source file format to get the extensions for.</param>
        /// <param name="defaultExt">Default file extension.</param>
        /// <param name="filters">A file extension filter string</param>
        public static void GetFileExtensions(DataSourceFormat format, out string defaultExt, out string filters)
        {
            switch (format)
            {
                case DataSourceFormat.CSV:
                    defaultExt = ".csv";
                    filters = "CSV (*.csv)|*.csv|Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.TAB:
                case DataSourceFormat.PIPE:
                    defaultExt = ".txt";
                    filters = "Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.SHP:
                    defaultExt = ".zip";
                    filters = "Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.KML:
                    defaultExt = ".kml";
                    filters = "Kml (*.kml)|*.kml|Kmz (*.kmz)|*.kmz|Xml (.xml)|*.xml|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case DataSourceFormat.XML:
                default:
                    defaultExt = ".xml";
                    filters = "Xml (*.xml)|*.xml|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
            }
        }

        /// <summary>
        /// Give a batch geocoding file format, this method provides a default and file extension filter string. 
        /// </summary>
        /// <param name="format">The batch geocoding file file format to get the extensions for.</param>
        /// <param name="defaultExt">Default file extension.</param>
        /// <param name="filters">A file extension filter string</param>
        public static void GetFileExtensions(BatchFileFormat format, out string defaultExt, out string filters)
        {
            switch (format)
            {
                case BatchFileFormat.CSV:
                    defaultExt = ".csv";
                    filters = "CSV (*.csv)|*.csv|Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case BatchFileFormat.TAB:
                case BatchFileFormat.PIPE:
                    defaultExt = ".txt";
                    filters = "Text (*.txt)|*.txt|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
                case BatchFileFormat.XML:
                default:
                    defaultExt = ".xml";
                    filters = "Xml (.xml)|*.xml|Zip (*.zip)|*.zip|All files (*.*)|*.*";
                    break;
            }
        }
    }
}

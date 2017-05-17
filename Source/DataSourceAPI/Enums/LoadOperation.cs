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
    /// The type of operation to perform when uploading a data source.
    /// </summary>
    public enum LoadOperation
    {
        /// <summary>
        /// Publishes entity data to a new data source or overwrites an existing data source. 
        /// </summary>
        Complete,

        /// <summary>
        /// Performs the same operation as "complete" except that the data source data is staged and not published. 
        /// Use the query URLs with the isStaged parameter set to 1 (true) to query the staged data source. 
        /// Staged data sources that are not published are deleted after 30 days.
        /// </summary>
        CompleteStaging,

        /// <summary>
        /// Edit, add and delete entities in an existing data source. If you want to delete entities, you must add a 
        /// property named __deleteEntity to the schema and set it to 1 or true for each entity that you want to remove.
        /// </summary>
        Incremental,

        /// <summary>
        /// Performs the same operation as "incremental" except that the data source update is staged and not published. 
        /// Use the query URLs with the isStaged parameter set to 1 or true to query the staged data source. 
        /// Staged data sources that are not published are deleted after 30 days.
        /// </summary>
        IncrementalStaging
    }
}

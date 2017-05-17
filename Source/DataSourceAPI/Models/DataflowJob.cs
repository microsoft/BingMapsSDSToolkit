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

using System.Runtime.Serialization;

namespace BingMapsSDSToolkit.DataSourceAPI
{
    /// <summary>
    /// An object that represents a single data flow job.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    public class DataflowJob : DataServiceJob
    {
        #region Constructor

        /// <summary>
        /// An object that represents a single data flow job.
        /// </summary>
        public DataflowJob()
        {
        }

        /// <summary>
        /// An object that represents a single data flow job.
        /// </summary>
        /// <param name="job">A data service job to convert into a DataflowJob.</param>
        public DataflowJob(DataServiceJob job)
        {
            this.Id = job.Id;
            this.Links = job.Links;
            this.ErrorMessage = job.ErrorMessage;
            this.CompletedDate = job.CompletedDate;
            this.CreatedDate = job.CreatedDate;
            this.Description = job.Description;
            this.Status = job.Status;
            this.Type = job.Type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The number of entities that did not process successfully because of an error.
        /// </summary>
        [DataMember(Name = "failedEntityCount", EmitDefaultValue = false)]
        public int FailedEntityCount { get; set; }

        /// <summary>
        /// The number of entities that were processed. This number included entities that were processed successfully and those that failed. If the field is set to 0, the number of processed entries is not known.
        /// </summary>
        [DataMember(Name = "processedEntityCount", EmitDefaultValue = false)]
        public int ProcessedEntityCount { get; set; }

        /// <summary>
        /// The total number of entities that were uploaded.
        /// </summary>
        [DataMember(Name = "totalEntityCount", EmitDefaultValue = false)]
        public int TotalEntityCount { get; set; }

        #endregion
    }
}

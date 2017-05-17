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

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BingMapsSDSToolkit.Internal
{
    internal class ServiceHelper
    {
        /// <summary>
        /// Downloads data as a string from a URL.
        /// </summary>
        /// <param name="url">URL that points to data to download.</param>
        /// <returns>A string with the data.</returns>
        public static Task<string> GetStringAsync(Uri url)
        {
            var tcs = new TaskCompletionSource<string>();

            var request = HttpWebRequest.Create(url);
            request.BeginGetResponse((a) =>
            {
                try
                {
                    var r = (HttpWebRequest)a.AsyncState;
                    HttpWebResponse response = (HttpWebResponse)r.EndGetResponse(a);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        tcs.SetResult(reader.ReadToEnd());
                    }
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }, request);

            return tcs.Task;
        }

        /// <summary>
        /// Downloads data as a stream from a URL.
        /// </summary>
        /// <param name="url">URL that points to data to download.</param>
        /// <returns>A stream with the data.</returns>
        public static Task<Stream> GetStreamAsync(Uri url)
        {
            var tcs = new TaskCompletionSource<Stream>();

            var request = HttpWebRequest.Create(url);
            request.BeginGetResponse((a) =>
            {
                try{
                    var r = (HttpWebRequest)a.AsyncState;
                    HttpWebResponse response = (HttpWebResponse)r.EndGetResponse(a);
                    tcs.SetResult(response.GetResponseStream());
                }catch(Exception ex){
                    tcs.SetException(ex);
                }
            }, request);

            return tcs.Task;
        }
    }
}

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

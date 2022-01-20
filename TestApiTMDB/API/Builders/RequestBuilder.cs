using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using TestApiTMDB.API.Response;
using TestApiTMDB.Extensions;

namespace TestApiTMDB.API.Builders
{
    public class RequestBuilder
    {

        private static HttpRequestMessage request;
        private static Uri BaseServiseUri { get; set; }

        public RequestBuilder(string url)
        {
            request = new HttpRequestMessage();
            BaseServiseUri = new Uri(url);
        }

        //public RequestBuilder WithHeader(string key, string value)
        //{
        //    _request.Headers.Add(key, value);
        //    return this;
        //}

        //public RequestBuilder WithHeaders(Dictionary<string, string> headers)
        //{
        //    foreach (var header in headers)
        //    {
        //        _request.Headers.Add(header.Key, header.Value);
        //    }
        //    return this;
        //}

        public RequestBuilder Method(HttpMethod method)
        {
            request.Method = method;
            return this;
        }

        public RequestBuilder Uri(string url)
        {
            request.RequestUri = BaseServiseUri.Append(url);
            Console.WriteLine(request);
            return this;
        }

        public RequestBuilder AddParams(string url, string key, string value)
        {
            request.RequestUri = BaseServiseUri.Append(url).AddOrUpdateParams(key, value);
            Console.WriteLine(request);
            return this;
        }
        public RequestBuilder AddGuestSession(string url, string key, string value, string sessionId, string valueId)
        {
            request.RequestUri = BaseServiseUri.Append(url).AddOrUpdateParams(key, value).AddOrUpdateParams(sessionId, valueId);
            Console.WriteLine(request);
            return this;
        }

        public RequestBuilder WithBody(string requestBody)
        {
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            return this;
        }
        public ApiResponse Execute()
        {
            using (var httpClient = new HttpClient())
            {
                request.Headers.Referrer = request.RequestUri;
                var response = httpClient.SendAsync(request, CancellationToken.None).Result;
                return new ApiResponse(response);
            }
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;

namespace TestApiTMDB.API.Response
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string ContentAsString { get; private set; }
        public JToken ContentAsJson => JToken.Parse(ContentAsString);

        public ApiResponse(HttpResponseMessage responseMessage)
        {
            StatusCode = responseMessage.StatusCode;
            ContentAsString = responseMessage.Content.ReadAsStringAsync().Result;
        }

        public T Content<T>()
        {
            try
            {
                return ContentAsJson.ToObject<T>();
            }
            catch (Exception)
            {
                throw new Exception($"Eror deserializing content. StatusCode = {StatusCode} \nContent = {ContentAsString}");
            }
        }

        public void Ensure(HttpStatusCode httpStatusCode)
        {
            if (StatusCode != httpStatusCode)
            {
                throw new Exception($"Status code is not match. SatusCode = {StatusCode} \nContetnt = {ContentAsString}");
            }
        }

        public void EnsureSuccessful()
        {
            if ((int)StatusCode < 200 || (int)StatusCode >= 300)
            {
                throw new Exception($"Expected to be successful. SatusCode = {StatusCode} \nContetnt = {ContentAsString}");
            }
        }

        public HttpStatusCode ifUnSuccesful()
        {
            if ((int)StatusCode < 200 || (int)StatusCode >= 300)
            {
                return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.OK;
        }

    }
}

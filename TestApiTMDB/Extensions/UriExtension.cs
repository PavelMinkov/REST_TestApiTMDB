using System;
using System.Linq;
using System.Web;

namespace TestApiTMDB.Extensions
{
    public static class UriExtension
    {
        public static Uri Append(this Uri uri, params string[] paht)
        {
            return new Uri(paht.Aggregate(uri.AbsoluteUri, (current, path) =>
            $"{ current.TrimEnd('/')}/{path.TrimStart('/')}"));
        }

        public static Uri AddOrUpdateParams(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (query.AllKeys.Contains(paramName))
            {
                query[paramName] = paramValue;
            }
            else
            {
                query.Add(paramName, paramValue);
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}

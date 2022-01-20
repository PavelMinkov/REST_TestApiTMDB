using System.Net.Http;
using TestApiTMDB.API.Builders;
using TestApiTMDB.API.Response;
using TestApiTMDB.Utils;

namespace TestApiTMDB.API.Request
{
    class ApiRequest
    {
        public RequestBuilder request;
        readonly PropertyReader property = new();

        public ApiRequest()
        {
            request = new RequestBuilder(property.GetURL());
        }

        public ApiResponse GetPopular()
        {
            return request.Method(HttpMethod.Get).AddParams("/movie/popular", "api_key", property.GetApiKey()).Execute();
        }
        public ApiResponse GetPopularWithoutApiKey()
        {
            return request.Method(HttpMethod.Get).Uri("/movie/popular").Execute();
        }

        public ApiResponse GetDetails(int movie_id)
        {
            return request.Method(HttpMethod.Get).AddParams($"/movie/{movie_id}", "api_key", property.GetApiKey()).Execute();
        }

        public ApiResponse CreateGuestSession()
        {
            return request.Method(HttpMethod.Get).AddParams("/authentication/guest_session/new", "api_key", property.GetApiKey()).Execute();
        }
        public ApiResponse PostRateMovie(int movie_id, string guestSessionId, string rating)
        {
            return request.Method(HttpMethod.Post)
                .AddGuestSession($"/movie/{movie_id}/rating", "api_key", property.GetApiKey(), "guest_session_id", guestSessionId)
                .WithBody($"{{\"value\":{rating}}}")
                .Execute();
        }
        public ApiResponse PostRateMovieWithoutGuestSession(int movie_id, string rating)
        {
            return request.Method(HttpMethod.Post).AddParams($"/movie/{movie_id}/rating", "api_key", property.GetApiKey())
                .WithBody($"{{\"value\":{rating}}}")
                .Execute();
        }

        public ApiResponse DeleteRatingMovie(int movie_id, string guestSessionId, string rating)
        {
            return request.Method(HttpMethod.Delete)
                .AddGuestSession($"/movie/{movie_id}/rating", "api_key", property.GetApiKey(), "guest_session_id", guestSessionId)
                .Execute();
        }

        public ApiResponse GetTopRated()
        {
            return request.Method(HttpMethod.Get).AddParams("/movie/top_rated", "api_key", property.GetApiKey()).Execute();
        }

    }
}

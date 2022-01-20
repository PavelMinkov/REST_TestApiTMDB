using NUnit.Framework;
using System.Linq;
using System.Net;
using TestApiTMDB.API.Request;
using TestApiTMDB.DataTransferObject;
using TestApiTMDB.Utils;

namespace TestApiTMDB.API.TestsAPI
{
    public class Tests
    {
        [Test]
        public void GetListPopularMovies()
        {
            var response = new ApiRequest().GetPopular();
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());
            Assert.That(response, Is.Not.Null);

            MoviesDto popularMovies = response.Content<MoviesDto>();
            Assert.That(popularMovies.MovieListResult, Is.Not.Null);
            Assert.That(popularMovies.MovieListResult[0].Popularity, Is.GreaterThan(popularMovies.MovieListResult[1].Popularity));
        }
        [Test]
        public void GetListPopularMoviesWithoutApiKey()
        {
            var response = new ApiRequest().GetPopularWithoutApiKey();
            response.Ensure(response.StatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.ifUnSuccesful());

            MoviesDto popularMovies = response.Content<MoviesDto>();
            Assert.That(popularMovies.MovieListResult, Is.Null);
        }

        [Test]
        public void GetInformationAboutMovie()
        {
            var response = new ApiRequest().GetPopular();

            MoviesDto popularMovies = response.Content<MoviesDto>();
            int movie_id = popularMovies.MovieListResult[0].Id;
            response = new ApiRequest().GetDetails(movie_id);
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());

            Result detailsMovies = response.Content<Result>();
            Assert.That(detailsMovies, Is.Not.Null);
        }
        [Test]
        public void GetGuestSession()
        {
            var response = new ApiRequest().CreateGuestSession();
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());

            GuestSessionDto guestSession = response.Content<GuestSessionDto>();
            Assert.IsNotEmpty(guestSession.guestSessionId);
            Assert.IsNotNull(guestSession.guestSessionId);
        }

        [Test]
        public void AddRateMovie()
        {
            var response = new ApiRequest().CreateGuestSession();

            GuestSessionDto guestSession = response.Content<GuestSessionDto>();
            string guestSessionId = guestSession.guestSessionId;

            response = new ApiRequest().GetPopular();

            MoviesDto popularMovies = response.Content<MoviesDto>();
            int movie_id = popularMovies.MovieListResult[1].Id;

            response = new ApiRequest().PostRateMovie(movie_id, guestSessionId, "9.5");
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());
        }
        [Test]
        public void AddRateMovieWithoutGuestSession()
        {
            var response = new ApiRequest().GetPopular();

            MoviesDto popularMovies = response.Content<MoviesDto>();
            int movie_id = popularMovies.MovieListResult[1].Id;

            response = new ApiRequest().PostRateMovieWithoutGuestSession(movie_id, "9.5");
            response.Ensure(response.StatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.ifUnSuccesful());
        }

        [Test]
        public void RemoveRratingMovie()
        {
            var response = new ApiRequest().CreateGuestSession();

            GuestSessionDto guestSession = response.Content<GuestSessionDto>();
            string guestSessionId = guestSession.guestSessionId;

            response = new ApiRequest().GetPopular();

            MoviesDto popularMovies = response.Content<MoviesDto>();
            int movie_id = popularMovies.MovieListResult[1].Id;

            response = new ApiRequest().DeleteRatingMovie(movie_id, guestSessionId, "8.5");
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());
        }
        [Test]
        public void GetListRatedMovies()
        {
            var response = new ApiRequest().GetTopRated();
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());

            MoviesDto ratedMovies = response.Content<MoviesDto>();
            Assert.That(ratedMovies.MovieListResult, Is.Not.Null);
            Assert.That(ratedMovies.MovieListResult[0].VoteAverage, Is.GreaterThan(ratedMovies.MovieListResult[10].VoteAverage));
        }

        [Test]
        public void CompareInformationAboutMovieWithTemplate()
        {
            MovieDetailsReader template = new();

            var response = new ApiRequest().GetDetails(template.GetId());
            response.EnsureSuccessful();
            Assert.AreEqual(HttpStatusCode.OK, response.ifUnSuccesful());

            Result detailsrMovie = response.Content<Result>();
            Assert.AreEqual(template.GetId(), detailsrMovie.Id);
            Assert.AreEqual(template.GetTitleOriginal(), detailsrMovie.OriginalTitle);
            Assert.AreEqual(template.GetReleaseDate(), detailsrMovie.ReleaseDate);
            Assert.AreEqual(template.GetRevenue(), detailsrMovie.Revenue);
            Assert.AreEqual(template.GetRuntime(), detailsrMovie.Runtime);
            Assert.AreEqual(template.GetAdult(), detailsrMovie.Adult);
            Assert.AreEqual(template.GetOverview(), detailsrMovie.Overview);
            Assert.AreEqual(template.GetOriginalLanguage(), detailsrMovie.OriginalLanguage);
            Assert.AreEqual(template.GetVoteAverage(), detailsrMovie.VoteAverage);
        }
        [Test]
        public void CheckSortPopular()
        {
            var response = new ApiRequest().GetPopular();
            response.EnsureSuccessful();
            MoviesDto popularMovies = response.Content<MoviesDto>();
            Assert.That(popularMovies.MovieListResult.Select(x => x.Popularity), Is.Not.Ordered.Descending);
            Assert.That(popularMovies.MovieListResult.Select(x => x.Popularity), Is.Not.Ordered.Ascending);
        }

        [Test]
        public void CheckSortRated()
        {
            var response = new ApiRequest().GetTopRated();
            response.EnsureSuccessful();
            MoviesDto ratedMovies = response.Content<MoviesDto>();
            Assert.That(ratedMovies.MovieListResult.Select(x => x.VoteAverage), Is.Ordered.Descending);
        }

    }
}

using System;
using System.IO;
using System.Runtime.Serialization.Json;
using TestApiTMDB.DataTest;

namespace TestApiTMDB.Utils
{
    class MovieDetailsReader
    {
        private MovieDetails properties;

        public MovieDetailsReader()
        {
            ReadPropertiesJson();
        }

        private void ReadPropertiesJson()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(MovieDetails));
            try
            {
                string path = Directory.GetCurrentDirectory();
                path = path.Substring(0, path.Length - 17);
                path = Path.Combine(path, @"DataTest\MovieDetails.json");
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    properties = (MovieDetails)jsonFormatter.ReadObject(fs);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Can`t open properties file");
            }
        }
        public int GetId()
        {
            return properties.id;
        }
        public double GetPopularity()
        {
            return properties.popularity;
        }
        public double GetVoteAverage()
        {
            return properties.vote_average;
        }
        public string GetTitleOriginal()
        {
            return properties.original_title;
        }
        public string GetReleaseDate()
        {
            return properties.release_date;
        }
        public int GetRevenue()
        {
            return properties.revenue;
        }
        public int GetRuntime()
        {
            return properties.runtime;
        }
        public bool GetAdult()
        {
            return properties.adult;
        }
        public string GetOverview()
        {
            return properties.overview;
        }
        public string GetOriginalLanguage()
        {
            return properties.original_language;
        }
    }
}


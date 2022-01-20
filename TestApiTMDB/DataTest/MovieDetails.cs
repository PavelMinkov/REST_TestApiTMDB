using System.Runtime.Serialization;

namespace TestApiTMDB.DataTest
{

    [DataContract]
    public class MovieDetails
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public double popularity { get; set; }

        [DataMember]
        public double vote_average { get; set; }

        [DataMember]
        public string original_title { get; set; }

        [DataMember]
        public string release_date { get; set; }

        [DataMember]
        public int revenue { get; set; }

        [DataMember]
        public int runtime { get; set; }

        [DataMember]
        public bool adult { get; set; }

        [DataMember]
        public string overview { get; set; }

        [DataMember]
        public string original_language { get; set; }
    }
}

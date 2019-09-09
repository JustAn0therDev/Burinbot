using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Regalia.net.Entities
{
    public class AnimeList
    {
        public List<Recommendation> Recommendations { get; set; } = new List<Recommendation>();
    }

    public class Recommendation
    {
        [DeserializeAs(Name = "mal_id")]
        public decimal MalID { get; set; }
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }
        [DeserializeAs(Name = "url")]
        public string URL { get; set; }
    }
}

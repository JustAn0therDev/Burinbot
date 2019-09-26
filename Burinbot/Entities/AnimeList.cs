using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class AnimeList
    {
        [DeserializeAs(Name = "recommendations")]
        public List<Anime> Recommendations { get; set; } = new List<Anime>();
    }

    public class Anime
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
        [DeserializeAs(Name = "mal_id")]
        public int MalID { get; set; }
        //The Rank property is only used by the TopAnimes module.
        [DeserializeAs(Name = "rank")]
        public int Rank { get; set; }
        [DeserializeAs(Name = "url")]
        public string URL { get; set; }
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }
        [DeserializeAs(Name = "synopsis")]
        public string Synopsis { get; set; }
        [DeserializeAs(Name = "episodes")]
        public int Episodes { get; set; }
        [DeserializeAs(Name = "score")]
        public decimal Score { get; set; }
    }
}

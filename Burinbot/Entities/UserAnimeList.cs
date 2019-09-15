using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class UserAnimeList
    {
        [DeserializeAs(Name = "anime")]
        public List<UserAnime> UserAnimes { get; set; } = new List<UserAnime>();
    }
    public class UserAnime
    {
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }
        [DeserializeAs(Name = "url")]
        public string URL { get; set; }
        [DeserializeAs(Name = "score")]
        public double Score { get; set; }
        [DeserializeAs(Name = "rating")]
        public string Rating { get; set; }
    }
}

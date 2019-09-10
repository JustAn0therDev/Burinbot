using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Burinbot.Entities
{
    public class MangaList
    {
        [DeserializeAs(Name = "recommendations")]
        public List<Manga> Recommendations { get; set; } = new List<Manga>();
    }
    public class Manga
    {
        [DeserializeAs(Name = "mal_id")]
        public int MalID { get; set; }
        [DeserializeAs(Name = "url")]
        public string URL { get; set; }
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }
        [DeserializeAs(Name = "synopsis")]
        public string Synopsis { get; set; }
        [DeserializeAs(Name = "chapters")]
        public int Chapters { get; set; }
        [DeserializeAs(Name = "score")]
        public decimal Score { get; set; }
    }
}

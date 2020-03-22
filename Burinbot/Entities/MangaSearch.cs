using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Burinbot.Entities
{
    public class MangaSearch
    {
        [DeserializeAs(Name = "results")]
        public List<Manga> Results { get; set; } = new List<Manga>();
    }
}

using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class AnimeSearch
    {
        [DeserializeAs(Name = "results")]
        public List<Anime> Results { get; set; } = new List<Anime>();
    }
}

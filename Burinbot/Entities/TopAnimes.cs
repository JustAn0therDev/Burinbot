using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class TopAnimes
    {
        [DeserializeAs(Name = "top")]
        public List<Anime> Top { get; set; } = new List<Anime>();
    }
}

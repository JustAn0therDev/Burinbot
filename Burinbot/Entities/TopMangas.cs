using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Burinbot.Entities
{
    public class TopMangas
    {
        [DeserializeAs(Name = "top")]
        public List<Manga> Top { get; set; } = new List<Manga>();
    }
}

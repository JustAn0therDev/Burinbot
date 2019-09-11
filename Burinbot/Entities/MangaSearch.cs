using System.Collections.Generic;
using RestSharp.Deserializers;

namespace Burinbot.Entities
{
    public class MangaSearch
    {
        //Instance of the list is made here so we don't bump into an ArgumentNullException
        [DeserializeAs(Name = "results")]
        public List<Manga> Results { get; set; } = new List<Manga>();
    }
}

using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class AnimeSearch
    {
        //Instance of the list is made here so we don't bump into an ArgumentNullException.
        [DeserializeAs(Name = "results")]
        public List<Anime> Results { get; set; } = new List<Anime>();
    }
}

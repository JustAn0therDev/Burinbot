using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Burinbot.Entities
{
    public class TopAnimes
    {
        [DeserializeAs(Name = "top")]
        public List<Anime> Top { get; set; } = new List<Anime>();
    }
}

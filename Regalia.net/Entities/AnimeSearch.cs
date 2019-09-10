using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class AnimeSearch
    {
        public List<Result> Results { get; set; } = new List<Result>();
    }
    public class Result
    {
        [DeserializeAs(Name = "mal_id")]
        public decimal MalID { get; set; }
    }
}

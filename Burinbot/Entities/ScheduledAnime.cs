using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class ScheduledAnime
    {
        [DeserializeAs(Name = "monday")]
        public List<Anime> Monday { get; set; } = new List<Anime>();       

        [DeserializeAs(Name = "tuesday")]
        public List<Anime> Tuesday { get; set; } = new List<Anime>();

        [DeserializeAs(Name = "wednesday")]
        public List<Anime> Wednesday { get; set; } = new List<Anime>();

        [DeserializeAs(Name = "thursday")]
        public List<Anime> Thursday { get; set; } = new List<Anime>();

        [DeserializeAs(Name = "friday")]
        public List<Anime> Friday { get; set; } = new List<Anime>();

        [DeserializeAs(Name = "saturday")]
        public List<Anime> Saturday { get; set; } = new List<Anime>();

        [DeserializeAs(Name = "sunday")]
        public List<Anime> Sunday { get; set; } = new List<Anime>();
    }
}

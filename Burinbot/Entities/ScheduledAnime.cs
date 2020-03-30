using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class ScheduledAnime
    {
        [DeserializeAs(Name = "monday")]
        public List<Anime> Monday { get; set; }      

        [DeserializeAs(Name = "tuesday")]
        public List<Anime> Tuesday { get; set; }

        [DeserializeAs(Name = "wednesday")]
        public List<Anime> Wednesday { get; set; }

        [DeserializeAs(Name = "thursday")]
        public List<Anime> Thursday { get; set; }

        [DeserializeAs(Name = "friday")]
        public List<Anime> Friday { get; set; }

        [DeserializeAs(Name = "saturday")]
        public List<Anime> Saturday { get; set; }

        [DeserializeAs(Name = "sunday")]
        public List<Anime> Sunday { get; set; }
    }
}

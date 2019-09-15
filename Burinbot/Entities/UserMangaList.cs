using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class UserMangaList
    {
        [DeserializeAs(Name = "manga")]
        public List<UserManga> UserMangas { get; set; } = new List<UserManga>();
    }
    public class UserManga
    {
        [DeserializeAs(Name = "title")]
        public string Title { get; set; }
        [DeserializeAs(Name = "url")]
        public string URL { get; set; }
        [DeserializeAs(Name = "score")]
        public double Score { get; set; }
    }
}


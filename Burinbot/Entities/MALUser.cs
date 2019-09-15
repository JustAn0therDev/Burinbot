using RestSharp.Deserializers;
using System.Collections.Generic;

namespace Burinbot.Entities
{
    public class MALUser
    {
        [DeserializeAs(Name = "username")]
        public string Username { get; set; }
        [DeserializeAs(Name = "url")]
        public string URL { get; set; }
        [DeserializeAs(Name = "anime_stats")]
        public AnimeStats AnimeStats { get; set; }
        [DeserializeAs(Name = "manga_stats")]
        public MangaStats MangaStats { get; set; }
        [DeserializeAs(Name = "favorites")]
        public Favorites Favorites { get; set; }
    }

    public class AnimeStats
    {
        [DeserializeAs(Name = "days_watched")]
        public double DaysWatched { get; set; }
        [DeserializeAs(Name = "mean_score")]
        public double MeanScore { get; set; }
        [DeserializeAs(Name = "watching")]
        public int Watching { get; set; }
        [DeserializeAs(Name ="completed")]
        public int Completed { get; set; }
        [DeserializeAs(Name = "on_hold")]
        public int OnHold { get; set; }
        [DeserializeAs(Name = "dropped")]
        public int Dropped { get; set; }
        [DeserializeAs(Name = "plan_to_watch")]
        public int PlanToWatch { get; set; }
        [DeserializeAs(Name = "total_entries")]
        public int TotalEntries { get; set; }
        [DeserializeAs(Name = "rewatched")]
        public int ReWatched { get; set; }
        [DeserializeAs(Name = "episodes_watched")]
        public int EpisodesWatched { get; set; }
    }
    public class MangaStats
    {
        [DeserializeAs(Name = "days_read")]
        public double DaysRead { get; set; }
        [DeserializeAs(Name = "mean_score")]
        public double MeanScore { get; set; }
        [DeserializeAs(Name = "reading")]
        public int Reading { get; set; }
        [DeserializeAs(Name = "completed")]
        public int Completed { get; set; }
        [DeserializeAs(Name = "on_hold")]
        public int OnHold { get; set; }
        [DeserializeAs(Name = "dropped")]
        public int Dropped { get; set; }
        [DeserializeAs(Name = "plan_to_read")]
        public int PlanToRead { get; set; }
        [DeserializeAs(Name = "total_entries")]
        public int TotalEntries { get; set; }
        [DeserializeAs(Name = "rewatched")]
        public int ReRead { get; set; }
        [DeserializeAs(Name = "chapters_read")]
        public int ChaptersRead { get; set; }
        [DeserializeAs(Name = "volumes_read")]
        public int VolumesRead { get; set; }
    }

    public class Favorites
    {
        [DeserializeAs(Name = "anime")]
        public List<Anime> Animes { get; set; } = new List<Anime>();
        [DeserializeAs(Name = "manga")]
        public List<Manga> Mangas { get; set; } = new List<Manga>();
    }
}

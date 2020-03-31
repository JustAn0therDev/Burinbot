using System.Threading.Tasks;

namespace Burinbot.Modules.SubClassesForModules
{
    public class AnimeScheduleEmbedMessageBuilder : AnimeSchedule
    {
        public Task BuildEmbedMessageWithAnimeScheduledForMonday()
        {
            Parallel.ForEach(Animes.Monday, anime =>
             {
                 if (Animes.Monday.Count < 25)
                     Animes.Monday.Add(anime);
             });

            Parallel.ForEach(Animes.Monday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }

        public Task BuildEmbedMessageWithAnimeScheduledForTuesday()
        {
            Parallel.ForEach(Animes.Tuesday, anime =>
            {
                if (Animes.Tuesday.Count < 25)
                    Animes.Tuesday.Add(anime);
            });

            Parallel.ForEach(Animes.Tuesday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }

        public Task BuildEmbedMessageWithAnimeScheduledForWednesday()
        {
            Parallel.ForEach(Animes.Wednesday, anime =>
            {
                if (Animes.Wednesday.Count < 25)
                    Animes.Wednesday.Add(anime);
            });

            Parallel.ForEach(Animes.Wednesday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }

        public Task BuildEmbedMessageWithAnimeScheduledForThursday()
        {
            Parallel.ForEach(Animes.Thursday, anime =>
            {
                if (Animes.Thursday.Count < 25)
                    Animes.Thursday.Add(anime);
            });

            Parallel.ForEach(Animes.Thursday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }

        public Task BuildEmbedMessageWithAnimeScheduledForFriday()
        {
            Parallel.ForEach(Animes.Friday, anime =>
            {
                if (Animes.Friday.Count < 25)
                    Animes.Friday.Add(anime);
            });

            Parallel.ForEach(Animes.Friday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }

        public Task BuildEmbedMessageWithAnimeScheduledForSaturday()
        {
            Parallel.ForEach(Animes.Saturday, anime =>
            {
                if (Animes.Saturday.Count < 25)
                    Animes.Saturday.Add(anime);
            });

            Parallel.ForEach(Animes.Saturday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }

        public Task BuildEmbedMessageWithAnimeScheduledForSunday()
        {
            Parallel.ForEach(Animes.Sunday, anime =>
            {
                if (Animes.Sunday.Count < 25)
                    Animes.Sunday.Add(anime);
            });

            Parallel.ForEach(Animes.Sunday, anime =>
            {
                EmbedMessage.AddField(x =>
                {
                    x.Name = $"{anime.Title ?? anime.Name}";
                    x.Value = $"More Info: {anime.URL}\nEpisodes: {anime.Episodes}\nScore: {anime.Score}";
                    x.IsInline = false;
                });
            });

            return Task.CompletedTask;
        }
    }
}

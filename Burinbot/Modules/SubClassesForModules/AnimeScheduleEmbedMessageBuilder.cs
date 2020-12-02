using System.Threading.Tasks;

namespace Burinbot.Modules.SubClassesForModules
{
    public class AnimeScheduleEmbedMessageBuilder : AnimeSchedule
    {
        public Task BuildEmbedMessageWithAnimeScheduledForMonday()
        {
            Parallel.ForEach(Animes.Monday, anime =>
            {
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
                if (EmbedMessage.Fields.Count < LIMIT_OF_FIELDS_PER_EMBED_MESSAGE) 
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
using AniListNet.Parameters;
using AnimeRecommender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniListNet.Objects;
using AniListNet;

namespace AnimeRecommender.Services
{
    public class AniListClient : IAniListClient
    {
        private readonly AniClient _client;

        public AniListClient(AniClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<AnimeDataModel>> GetSpecificAnimeDataAsync(AnimeSearchQuery query)
        {
            await _client.TryAuthenticateAsync(AccessToken.Token);

            var filter = new SearchMediaFilter
            {
                Query = query.Query,
            };
            
            var results = await _client.SearchMediaAsync(filter);

            return results.Data.Select(x => new AnimeDataModel
            {
                Title = x.Title.PreferredTitle,
                Description = x.Description,
                ImageUrl = x.Cover.MediumImageUrl,
                Genres = x.Genres
            });
        }

        public async Task<IEnumerable<AnimeDataModel>> GetFeaturedAnimeAsync()
        {
            await _client.TryAuthenticateAsync(AccessToken.Token);

            var filter = new SearchMediaFilter
            {
                Season = MediaSeason.Winter,
                SeasonYear = DateTime.Now.Year,
                Type = MediaType.Anime,
                IsAdult = false,
                Sort = MediaSort.Popularity,
            };

            var results = await _client.SearchMediaAsync(filter);

            return results.Data.Select(x => new AnimeDataModel
            {
                Title = x.Title.PreferredTitle,
                ImageUrl = x.Cover.LargeImageUrl,
            });
        }

        public async Task<IEnumerable<MangaDataModel>> GetFeaturedMangaAsync()
        {
            await _client.TryAuthenticateAsync(AccessToken.Token);

            var filter = new SearchMediaFilter
            {
                Type = MediaType.Manga,
                IsAdult = false,
                Sort = MediaSort.Trending
            };

            var results = await _client.SearchMediaAsync(filter);

            return results.Data.Select(x => new MangaDataModel
            {
                Title = x.Title.PreferredTitle,
                ImageUrl = x.Cover.LargeImageUrl,
            });
        }

        public async Task<Uri> GetUserImageUrlAsync()
        {
            var isAuthenticated = await _client.TryAuthenticateAsync(AccessToken.Token);

            if(!isAuthenticated)
            {
                return new Uri("user_icon.png");
            }

            var user = await _client.GetAuthenticatedUserAsync();

            return user.Avatar.LargeImageUrl;
        }
    }
}

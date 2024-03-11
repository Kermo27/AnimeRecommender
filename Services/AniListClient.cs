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
                Type = MediaType.Anime,
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
                Season = GetSeason(),
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

        public async Task<string> GetUserImageUrlAsync()
        {
            var isAuthenticated = await _client.TryAuthenticateAsync(AccessToken.Token);

            if(!isAuthenticated)
            {
                return "user_icon.png";
            }

            var user = await _client.GetAuthenticatedUserAsync();

            return user.Avatar.LargeImageUrl.ToString();
        }

        public MediaSeason GetSeason()
        {
            switch(DateTime.Now.Month)
            {
                case 1:
                case 2:
                case 3:
                    return MediaSeason.Winter;
                case 4:
                case 5:
                case 6:
                    return MediaSeason.Spring;
                case 7:
                case 8:
                case 9:
                    return MediaSeason.Summer;
                case 10:
                case 11:
                case 12:
                    return MediaSeason.Fall;
                default:
                    return MediaSeason.Winter;
            }
        }
    }
}

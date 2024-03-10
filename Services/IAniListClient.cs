using AnimeRecommender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.Services
{
    public interface IAniListClient
    {
        Task<IEnumerable<AnimeDataModel>> GetSpecificAnimeDataAsync(AnimeSearchQuery query);
        Task<IEnumerable<AnimeDataModel>> GetFeaturedAnimeAsync();
        Task<IEnumerable<MangaDataModel>> GetFeaturedMangaAsync();
        Task<Uri> GetUserImageUrlAsync();
    }
}

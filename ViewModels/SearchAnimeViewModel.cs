using AnimeRecommender.Models;
using AnimeRecommender.Services;
using AnimeRecommender.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.ViewModels
{
    [QueryProperty(nameof(SearchText), nameof(SearchText))]
    public partial class SearchAnimeViewModel : ObservableObject
    {
        private readonly IAniListClient _client;

        [ObservableProperty]
        ObservableCollection<AnimeDataModel> _anime = new ObservableCollection<AnimeDataModel>();

        [ObservableProperty]
        private string _searchText;

        public SearchAnimeViewModel(IAniListClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
            SearchAnime();
        }

        [RelayCommand]
        private async Task SearchAnime()
        {
            var query = new AnimeSearchQuery
            {
                Query = SearchText
            };

            var result = await _client.GetSpecificAnimeDataAsync(query);
            Anime.Clear();
            foreach (var item in result)
            {
                Anime.Add(item);
            }
        }

        [RelayCommand]
        private async Task ItemTapped(MediaModel mediaModel)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
               { nameof(DetailsViewModel.MediaModel), mediaModel }
            });
        }
    }
}

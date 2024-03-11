using AnimeRecommender.Models;
using AnimeRecommender.Services;
using AnimeRecommender.View;
using CommunityToolkit.Maui.Alerts;
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
    public partial class MainViewModel : ObservableObject
    {
        private readonly IAniListClient _client;

        private readonly IAuthenticatorService authenticator = new AuthenticatorService();

        [ObservableProperty]
        ObservableCollection<MediaModel> _featuredAnime = new ObservableCollection<MediaModel>();

        [ObservableProperty]
        ObservableCollection<MediaModel> _featuredManga = new ObservableCollection<MediaModel>();

        [ObservableProperty]
        private string _userImageUrl = "user_icon.png";

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private string _top20AnimeTitle;

        private readonly string _top20AnimeTitleTemplate = "Top 20 Anime {0} {1}";

        public MainViewModel(IAniListClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
            var season = _client.GetSeason();
            var year = DateTime.Now.Year;

            Top20AnimeTitle = string.Format(_top20AnimeTitleTemplate, season, year);

            LoadFeaturedAnime();
            LoadFeaturedManga();
        }

        [RelayCommand]
        private async Task AuthorizeUser()
        {
            string code = await authenticator.GetAccessCodeAsync();

            string accessToken = await authenticator.ExchangeCodeForTokenAsync(code);

            if (!string.IsNullOrEmpty(accessToken))
            {
                AccessToken.Token = accessToken;
                UserImageUrl = await _client.GetUserImageUrlAsync();
                await Toast.Make("You are now logged in!").Show();

                return;
            }

            await Toast.Make("Authorization failed").Show();
        }

        [RelayCommand]
        private async Task SearchAnime()
        {
            await Shell.Current.GoToAsync(nameof(SearchAnimePage), new Dictionary<string, object>
            {
                { nameof(SearchAnimeViewModel.SearchText), SearchText }
            });
        }

        [RelayCommand]
        private async Task ItemTapped(MediaModel mediaModel)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            {
               { nameof(DetailsViewModel.MediaModel), mediaModel }
            });
        }

        private async Task LoadFeaturedAnime()
        {
            var result = await _client.GetFeaturedAnimeAsync();
            FeaturedAnime.Clear();
            foreach (var item in result)
            {
                FeaturedAnime.Add(new AnimeDataModel
                {
                    Title = item.Title,
                    ImageUrl = item.ImageUrl,
                });
            }
        }

        private async Task LoadFeaturedManga()
        {
            var result = await _client.GetFeaturedMangaAsync();
            FeaturedManga.Clear();
            foreach (var item in result)
            {
                FeaturedManga.Add(new MangaDataModel
                {
                    Title = item.Title,
                    ImageUrl = item.ImageUrl,
                });
            }
        }
    }
}

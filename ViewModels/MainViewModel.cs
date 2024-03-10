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
        ObservableCollection<AnimeDataModel> _featuredAnime = new ObservableCollection<AnimeDataModel>();

        [ObservableProperty]
        ObservableCollection<MangaDataModel> _featuredManga = new ObservableCollection<MangaDataModel>();

        [ObservableProperty]
        private string _userImageUrl = "user_icon.png";

        [ObservableProperty]
        private string _searchText;

        public MainViewModel(IAniListClient client)
        {
            _client = client;
        }

        public void Initialize()
        {
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
        private async Task OnItemTapped()
        {
            //await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
            //{
            //    { nameof(DetailsViewModel.MediaModel), mediaModel }
            //});

        }

        private async Task LoadFeaturedAnime()
        {
            var result = await _client.GetFeaturedAnimeAsync();
            FeaturedAnime.Clear();
            int index = 1;
            foreach (var item in result)
            {
                FeaturedAnime.Add(new AnimeDataModel
                {
                    Title = item.Title,
                    ImageUrl = item.ImageUrl,
                    Index = index
                });
                index++;
            }
        }

        private async Task LoadFeaturedManga()
        {
            var result = await _client.GetFeaturedMangaAsync();
            FeaturedManga.Clear();
            int index = 1;
            foreach (var item in result)
            {
                FeaturedManga.Add(new MangaDataModel
                {
                    Title = item.Title,
                    ImageUrl = item.ImageUrl,
                    Index = index
                });
                index++;
            }
        }
    }
}

using AnimeRecommender.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.ViewModels
{
    [QueryProperty(nameof(MediaModel), nameof(MediaModel))]
    public partial class DetailsViewModel : ObservableObject
    {
        public IMediaModel MediaModel { get; set; }

        [ObservableProperty]
        private string _title;

        public void Initialize()
        {
            Title = MediaModel.Title;
        }
    }
}

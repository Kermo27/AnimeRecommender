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
    public partial class DetailsViewModel : ObservableObject, IQueryAttributable
    {
        public MediaModel MediaModel { get; set; }

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private Uri _imageUrl;

        public void Initialize()
        {
            Title = MediaModel.Title;
            Description = MediaModel.Description;
            ImageUrl = MediaModel.ImageUrl;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            MediaModel = query[nameof(MediaModel)] as MediaModel?? new MediaModel() { Title = "No data", Description = "No data"};
        }
    }
}

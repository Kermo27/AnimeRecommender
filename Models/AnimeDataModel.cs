using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.Models
{
    public class AnimeDataModel : IMediaModel
    {
        private string title;
        public string Title { get => title; set => title = TrimTitle(value, 25); }

        public string? Description { get; internal set; }
        public Uri ImageUrl { get; internal set; }
        public string[] Genres { get; internal set; }
        public string Season { get; internal set; }
        public int SeasonYear { get; internal set; }
        public int Index { get; internal set; }

        private string TrimTitle(string title, int maxLength)
        {
            if (title.Length <= maxLength) return title;
            return title.Substring(0, maxLength) + "...";   
        }
    }
}

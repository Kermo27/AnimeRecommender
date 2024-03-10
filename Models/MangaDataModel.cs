using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.Models
{
    public class MangaDataModel : IMediaModel
    {
        private string title;
        public string Title { get => title; set => title = TrimTitle(value, 25); }
        public Uri ImageUrl { get; internal set; }
        public int Index { get; internal set; }

        private string TrimTitle(string title, int maxLength)
        {
            if (title.Length <= maxLength) return title;
            return title.Substring(0, maxLength) + "...";
        }
    }
}

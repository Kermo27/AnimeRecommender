using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.Models
{
    public class MediaModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Uri ImageUrl { get; set; } = new Uri("https://via.placeholder.com/150");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommender.Models
{
    public class AnimeDataModel : MediaModel
    {
        public string[] Genres { get; internal set; }
        public string Season { get; internal set; }
        public int SeasonYear { get; internal set; }
    }
}

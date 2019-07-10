using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Infrastructure;
using VideoPlayer.Models;

namespace VideoPlayer
{
    class Storage: NotifyProperty
    {
        #region private
        private bool theme;
        private string language;
        private string sort;
        #endregion

        #region Properties
        [JsonProperty("Theme")]
        public bool Theme
        {
            get => theme;
            set
            {
                theme = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty("Language")]
        public string Language
        {
            get => language;
            set
            {
                language = value;
                NotifyPropertyChanged();
            }
        }
        [JsonProperty("Sort")]
        public string Sort
        {
            get => sort;
            set
            {
                sort = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public static Dictionary<string, Film> DictionarySizeImages()
        {
            Dictionary<string, Film> film = new Dictionary<string, Film>();
            film["Big"] = new Film() { SizeImageHeightPoster = "200", SizeImageWidthPoster = "140", SizeImageHighGrid = "290", FontSize = "14" };
            film["Average"] = new Film() { SizeImageHeightPoster = "170", SizeImageWidthPoster = "120", SizeImageHighGrid = "255", FontSize = "12" };
            film["Little"] = new Film() { SizeImageHeightPoster = "140", SizeImageWidthPoster = "100", SizeImageHighGrid = "235", FontSize = "10" };
            return film;
        }
    }
}

using Newtonsoft.Json;
using System;
using VideoPlayer.Infrastructure;

namespace VideoPlayer.Models
{
    public class Film: NotifyProperty, ICloneable
    {
        #region private
        string title;
        string year;
        string genre;
        string director;
        string plot;
        string poster;
        string rating;
        string urlFilm;
        string id;

        string sizeImageHeightPoster;
        string sizeImageWidthPoster;
        string sizeImageHighGrid;
        string fontSize;
        #endregion

        #region Property
        [JsonProperty("Id")]
        public string Id
        {
            get => id;
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("Title")]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("Year")]
        public string Year
        {
            get => year;
            set
            {
                year = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("Genre")]
        public string Genre
        {
            get => genre;
            set
            {
                genre = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("Director")]
        public string Director
        {
            get => director;
            set
            {
                director = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("Plot")]
        public string Plot
        {
            get => plot;
            set
            {
                plot = value;
                NotifyPropertyChanged();
            }
        }

        [JsonProperty("Poster")]
        public string Poster
        {
            get => poster;
            set
            {
                poster = value;
                NotifyPropertyChanged();
            }
        }

        public string Rating
        {
            get => rating;
            set
            {
                rating = value;
                NotifyPropertyChanged();
            }
        }

        public string URLFilm
        {
            get => urlFilm;
            set
            {
                urlFilm = value;
                NotifyPropertyChanged();
            }
        }

        public string SizeImageHeightPoster
        {
            get => sizeImageHeightPoster;
            set
            {
                sizeImageHeightPoster = value;
                NotifyPropertyChanged();
            }
        }

        public string SizeImageWidthPoster
        {
            get => sizeImageWidthPoster;
            set
            {
                sizeImageWidthPoster = value;
                NotifyPropertyChanged();
            }
        }

        public string SizeImageHighGrid
        {
            get => sizeImageHighGrid;
            set
            {
                sizeImageHighGrid = value;
                NotifyPropertyChanged();
            }
        }

        public string FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                NotifyPropertyChanged();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Infrastructure;
using VideoPlayer.Models;

namespace VideoPlayer
{
    public class ObservableCollectionFilms: NotifyProperty
    {
        private ObservableCollection<Film> films;

        public ObservableCollection<Film> Films
        {
            get => films;
            set
            {
                films = value;
                NotifyPropertyChanged();
            }
        }

        public void Remove(Film film)
        {
            Films.Remove(film);
        }

        public void Add(Film film)
        {
            Films.Add(film);
        }

        public Film AggregateID()
        {
           return Films.Aggregate((film1, film2) => Convert.ToInt32(film1.Id) > Convert.ToInt32(film2.Id) ? film1 : film2);
        }

        public Film FindID(string id)
        {
            return Films.FirstOrDefault(x => x.Id == id);
        }

        public ObservableCollection<Film> GetFilms()
        {
            return Films;
        }
    }
}

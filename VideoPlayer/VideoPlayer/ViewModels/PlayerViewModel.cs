using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VideoPlayer.Infrastructure;
using VideoPlayer.Models;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Documents;
using System.Windows.Data;

namespace VideoPlayer.ViewModels
{
    class PlayerViewModel : NotifyProperty
    {

        private static Film getFilm;
        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }

        public Film GetFilm
        {
            get => getFilm;
            set
            {
                getFilm = value;
                NotifyPropertyChanged();
            }
        }

        public PlayerViewModel()
        {
            AllCommands();
        }

        private void AllCommands()
        {
            CloseCommand = new RelayCommand(x => { (x as Window).Close(); });
            MinimizeCommand = new RelayCommand(x => { if (x is Window window) window.WindowState = WindowState.Minimized; });
            MaximizeCommand = new RelayCommand(x => { if (x is Window window) window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal; });
        }

        public void SendMessege(Film messege)
        {
            if (messege != null)
                this.GetFilm = messege;
        }

        public static string GetURLFIlm()
        {
            if (getFilm.URLFilm != null)
                return getFilm.URLFilm;
            return "null";
        }
    }
}

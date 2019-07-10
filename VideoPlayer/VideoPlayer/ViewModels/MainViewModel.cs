using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using VideoPlayer.Infrastructure;
using VideoPlayer.Interface;
using VideoPlayer.Models;

namespace VideoPlayer.ViewModels
{
    class MainViewModel : NotifyProperty
    {



        #region PrivateFields
        private IIOService<ObservableCollection<Film>> inOutService;
        ObservableCollectionFilms filmsColl = new ObservableCollectionFilms();
        private Film selecterFilm;
        private Film newFilm;
        private ViewFactory factory;
        private string size;
        private bool isDialogOpenAdd;
        private bool isDialogOpenEdit;
        private Storage storage = new Storage();
        private bool isCheckedTheme;
        private string search;
        #endregion

        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand SortCommand { get; set; }
        public ICommand SizeImageCommand { get; set; }
        public ICommand PlayVideoCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand AddFilmCommand { get; set; }
        public ICommand CancelAddFilmCommand { get; set; }
        public ICommand CancelEditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChoiceThemeCommand { get; set; }
        public ICommand AddFilmTrailer { get; set; }
        public ICommand SaveEditFilmCommand { get; set; }
        public ICommand LanguageCommand { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<Film> Films
        {
            get => filmsColl.Films;
            set
            {
                filmsColl.Films = value;
                NotifyPropertyChanged();
            }
        }

        public Film SelectedFilm
        {
            get => selecterFilm;
            set
            {
                selecterFilm = value;
                NotifyPropertyChanged();
            }
        }

        public Film NewFilm
        {
            get => newFilm;
            set
            {
                newFilm = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsDialogOpenAdd
        {
            get => isDialogOpenAdd;
            set
            {
                isDialogOpenAdd = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsDialogOpenEdit
        {
            get => isDialogOpenEdit;
            set
            {
                isDialogOpenEdit = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsCheckedTheme
        {
            get => isCheckedTheme;
            set
            {
                isCheckedTheme = value;
                NotifyPropertyChanged();
            }
        }

        public ICollectionView ItemsFilm { get; set; }
        public string Search
        {
            get => search;
            set
            {
                search = value;
                NotifyPropertyChanged(nameof(Search));
                ItemsFilm.Refresh();
            }
        }

        #endregion

        #region Constructor
        public MainViewModel(IIOService<ObservableCollection<Film>> inOutService)
        {
            AllCommands();
            this.inOutService = inOutService;
        }
        #endregion

        #region Metods
        private void AllCommands()
        {

            factory = new ViewFactory();

            CloseCommand = new RelayCommand(x => { if (x is Window window) window.Close(); });
            MaximizeCommand = new RelayCommand(x => { if (x is Window window) window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal; });
            MinimizeCommand = new RelayCommand(x => { if (x is Window window) window.WindowState = WindowState.Minimized; });
            SaveCommand = new RelayCommand(x => Save());
            LoadCommand = new RelayCommand(x => Load());
            EditCommand = new RelayCommand(x => ShowEditWindow((string)x));
            SortCommand = new RelayCommand(SortMethod);
            SizeImageCommand = new RelayCommand(SizeImageMethod);
            PlayVideoCommand = new RelayCommand(x => OpenPlayerWindow((string)x));
            AddCommand = new RelayCommand(x => Add());
            AddFilmCommand = new RelayCommand(x => AddFilm());
            DeleteCommand = new RelayCommand(x => { FindSelectedItem((string)x); Films.Remove(SelectedFilm); });
            ChoiceThemeCommand = new RelayCommand(x => ChoiceTheme(x));
            AddFilmTrailer = new RelayCommand(x => { SelectedFilm.URLFilm = OpenDirectory(); });
            LanguageCommand = new RelayCommand(LanguageMethod);
            CancelAddFilmCommand = new RelayCommand(x => IsDialogOpenAdd = false);
            CancelEditCommand = new RelayCommand(x => CancelEdit());
            SaveEditFilmCommand = new RelayCommand(x => ApplyEdit());
        }

        private bool Filter(Film film)
        {
            return Search == null
                || film.Title.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
                || film.Year.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1
                || film.Director.IndexOf(Search, StringComparison.OrdinalIgnoreCase) != -1;
        }

        private void ApplyEdit()
        {
            Films.Remove(SelectedFilm);
            Films.Add(NewFilm);
            IsDialogOpenEdit = false;
        }

        private void CancelEdit()
        {
            IsDialogOpenEdit = false;
        }

        private void AddFilm()
        {
            var film = Films.Aggregate((film1, film2) => Convert.ToInt32(film1.Id) > Convert.ToInt32(film2.Id) ? film1 : film2);
            NewFilm.Id = ((Convert.ToInt32(film.Id)) + 1).ToString();
            Films.Add(NewFilm);
            NewFilm.SizeImageHeightPoster = SelectedFilm.SizeImageHeightPoster;
            NewFilm.SizeImageHighGrid = SelectedFilm.SizeImageHighGrid;
            NewFilm.SizeImageWidthPoster = SelectedFilm.SizeImageWidthPoster;
            NewFilm.FontSize = SelectedFilm.FontSize;
            IsDialogOpenAdd = false;
        }

        private void Load()
        {
            Films = inOutService.Load();
            ItemsFilm = CollectionViewSource.GetDefaultView(Films);
            ItemsFilm.Filter = new Predicate<object>(o => Filter(o as Film));
            NotifyPropertyChanged(nameof(ItemsFilm));
            LoadStorage();
        }

        private void Save()
        {
            inOutService.Save(Films);
            File.WriteAllText("Storage.json", JsonConvert.SerializeObject(storage, Formatting.Indented));
        }

        private void LoadStorage()
        {
            storage = JsonConvert.DeserializeObject<Storage>(File.ReadAllText("Storage.json"));
            ChoiceTheme(storage.Theme);
            LanguageMethod(storage.Language);
            SortMethod(storage.Sort);
        }

        private void Add()
        {
            NewFilm = new Film();
            ShowAddWindow(NewFilm.Id);
            if (size != null)
                SizeImageMethod(size);
            else
            {
                size = "Big";
                SizeImageMethod(size);
            }
        }

        private string OpenDirectory()
        {
            string path = string.Empty;
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Video files (*.mp4)|*.mp4";
            if (save.ShowDialog() == true)
            {
                if (save.FileName != "")
                    path = save.FileName;
                return path;
            }

            return SelectedFilm.URLFilm;
        }

        private void ShowAddWindow(string obj)
        {
            IsDialogOpenAdd = true;
        }

        private void ShowEditWindow(string obj)
        {
            FindSelectedItem(obj);
            NewFilm = new Film();
            NewFilm = SelectedFilm.Clone() as Film;
            IsDialogOpenEdit = true;
        }

        private void ChoiceTheme(object param)
        {
            var darkTheme = Theme.GetDark();
            var lightTheme = Theme.GetLight();

            if ((bool)param == false)
            {
                foreach (var light in lightTheme)
                    Application.Current.Resources.MergedDictionaries.Remove(light);
                foreach (var dark in darkTheme)
                    Application.Current.Resources.MergedDictionaries.Add(dark);
                storage.Theme = false;
                IsCheckedTheme = false;
            }
            else
            {
                foreach (var dark in darkTheme)
                    Application.Current.Resources.MergedDictionaries.Remove(dark);
                foreach (var light in lightTheme)
                    Application.Current.Resources.MergedDictionaries.Add(light);
                storage.Theme = true;
                IsCheckedTheme = true;
            }
        }

        private void OpenPlayerWindow(string obj)
        {
            FindSelectedItem(obj);
            var vm = new PlayerViewModel();
            vm.SendMessege(SelectedFilm);
            factory.GetView(vm, "PlayerView").ShowDialog();
        }

        private void FindSelectedItem(object parametr)
        {
            SelectedFilm = filmsColl.FindID(parametr.ToString());
        }

        private void SortMethod(object param)
        {
            ICollectionView FilmsView = CollectionViewSource.GetDefaultView(Films);
            FilmsView.SortDescriptions.Clear();
            FilmsView.SortDescriptions.Add(new SortDescription(param.ToString(), ListSortDirection.Ascending));
            storage.Sort = param.ToString();
        }

        private void LanguageMethod(object parametr)
        {

            if (parametr.ToString() == "Russian")
            {
                Application.Current.Resources.MergedDictionaries.Remove(Translator.GetEnglish());
                Application.Current.Resources.MergedDictionaries.Add(Translator.GetRusian());
            }
            else if (parametr.ToString() == "English")
            {
                Application.Current.Resources.MergedDictionaries.Remove(Translator.GetRusian());
                Application.Current.Resources.MergedDictionaries.Add(Translator.GetEnglish());
            }
            storage.Language = parametr.ToString();
        }

        private void SizeImageMethod(object param)
        {
            size = param.ToString();

            foreach (KeyValuePair<string, Film> keyValue in Storage.DictionarySizeImages())
            {
                if (param.ToString() == keyValue.Key)
                {
                    foreach (var movie in filmsColl.GetFilms())
                    {
                        movie.SizeImageHeightPoster = keyValue.Value.SizeImageHeightPoster;
                        movie.SizeImageWidthPoster = keyValue.Value.SizeImageWidthPoster;
                        movie.SizeImageHighGrid = keyValue.Value.SizeImageHighGrid;
                        movie.FontSize = keyValue.Value.FontSize;
                    }
                }
            }
        }

        #endregion
    }
}

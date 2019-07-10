using System;
using System.Windows;

namespace VideoPlayer.Infrastructure
{
    static class Translator
    {
        public static ResourceDictionary GetEnglish()
        {
            var en = new Uri("Language/EN/RecourceString.xaml", UriKind.Relative);
            return Application.LoadComponent(en) as ResourceDictionary;
        }

        public static ResourceDictionary GetRusian()
        {
            var ru = new Uri("Language/RU/RecourceString.xaml", UriKind.Relative);
            return Application.LoadComponent(ru) as ResourceDictionary;
        }
    }
}

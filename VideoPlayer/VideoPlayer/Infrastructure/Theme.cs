using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoPlayer.Infrastructure
{
    public class Theme
    {
        public static ResourceDictionary[] GetLight()
        {
            var uriXaml = new Uri("Styles/Light.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uriXaml) as ResourceDictionary;
            return new ResourceDictionary[]
            {
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml") },
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml") },
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Grey.xaml") },
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.LightBlue.xaml")},
                resourceDictionary
            };
        }

        public static ResourceDictionary[] GetDark()
        {
            var uriXaml = new Uri("Styles/Dark.xaml", UriKind.Relative);
            ResourceDictionary resourceDictionary = Application.LoadComponent(uriXaml) as ResourceDictionary;
            return new ResourceDictionary[]
            {
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml") },
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml") },
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.BlueGrey.xaml") },
                new ResourceDictionary {Source= new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml")},
                resourceDictionary
            };
        }

        
    }
}

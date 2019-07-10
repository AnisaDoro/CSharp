using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Infrastructure;
using VideoPlayer.Interface;
using VideoPlayer.Models;
using VideoPlayer.ViewModels;

namespace VideoPlayer.Ico
{
    class IoC
    {
        public static IKernel Kernel { get; set; } = new StandardKernel();
        static Config file = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

        public static void Setup()
        {
            Kernel.Bind<MainViewModel>().ToSelf();
            Kernel.Bind<IIOService<ObservableCollection<Film>>>().To<JsonService<ObservableCollection<Film>>>();
            Kernel.Bind<ResourcePath>().To<ResourcePath>().WithPropertyValue("Path", file.FileName);
        }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}

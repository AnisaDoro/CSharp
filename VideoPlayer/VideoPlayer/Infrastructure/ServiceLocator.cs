using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ninject;
using VideoPlayer.Ico;
using VideoPlayer.Interface;
using VideoPlayer.Models;
using VideoPlayer.ViewModels;

namespace VideoPlayer.Infrastructure
{
    class ServiceLocator
    {

        public MainViewModel MainViewModel { get; set; }

        public ServiceLocator() => MainViewModel = IoC.Get<MainViewModel>();

    }
}

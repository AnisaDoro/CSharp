using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer.Infrastructure
{
    class Config : NotifyProperty
    {
        string fileName;

        [JsonProperty("FileName")]
        public string FileName
        {
            get => fileName;
            set
            {
                fileName = value;
                NotifyPropertyChanged();
            }
        }
    }
}

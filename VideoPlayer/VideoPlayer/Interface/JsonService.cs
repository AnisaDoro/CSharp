using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoPlayer.Models;

namespace VideoPlayer.Interface
{
    class JsonService<T> : IIOService<T>
        where T : new()
    {
        readonly ResourcePath filePath;
        public JsonService(ResourcePath path) => filePath = path;

        public T Load()
        {
            if (File.Exists(filePath.Path))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath.Path));
            return new T();
        }

        public void Save(T data)
        {
            File.WriteAllText(filePath.Path, JsonConvert.SerializeObject(data, Formatting.Indented));
        }

    }
}

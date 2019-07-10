using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPlayer.Interface
{
    interface IIOService<T>
    {
        void Save(T data);
        T Load();
    }
}

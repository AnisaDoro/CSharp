using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoPlayer.Infrastructure
{
    class ViewFactory
    {
        public Window GetView(object dataContext, string view)
        {
            Type typeWindow = Type.GetType($"VideoPlayer.{view}");
            var window = Activator.CreateInstance(typeWindow) as Window;
            window.DataContext = dataContext;
            return window;
        }
    }
}

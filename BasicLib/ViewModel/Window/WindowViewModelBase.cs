using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    class WindowViewModelBase : PackageViewModelBase
    {
        public WindowViewModelBase(string windowName) : base(windowName)
        {
            view = Frame.AllPanel[windowName];
        }
        protected override iFeature GetFeature(string Featurename, string elementName)
        {
            throw new NotImplementedException();
        }
    }
}

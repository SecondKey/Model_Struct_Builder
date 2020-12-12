using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    interface iFeatureProperty : iFeature
    {
        void CreatePropertyFeature(FrameworkElement viewElement, string viewName, string groupName);
    }
}

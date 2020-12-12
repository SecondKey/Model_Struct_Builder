using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    public abstract class FeaturePropertyBase : FeatureBase, iFeatureProperty
    {
        public virtual void CreatePropertyFeature(FrameworkElement viewElement, string viewName, string groupName)
        {
            CreateFeature(viewElement, viewName);
        }
    }
}

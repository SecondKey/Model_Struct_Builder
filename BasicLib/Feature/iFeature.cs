using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    public interface iFeature
    {
        void CreateFeature(FrameworkElement viewElement, string viewName);
        void ChangeModel(iFrameElement element);

        bool DoFeatureEvent(string token, params object[] parameters);
    }
}

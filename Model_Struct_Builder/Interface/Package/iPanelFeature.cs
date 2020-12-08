using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model_Struct_Builder
{
    public interface iPanelFeature
    {
        void CreateFeature(FrameworkElement view, string viewName);
        void ChangeModel(iFrameElement element);
    }
}

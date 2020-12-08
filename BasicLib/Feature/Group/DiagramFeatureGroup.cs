using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model_Struct_Builder;

namespace BasicLib
{
    class DiagramFeatureGroup : iPanelFeature
    {
        iFrameElement model;
        Dictionary<string, iPanelFeature> allFeature = new Dictionary<string, iPanelFeature>();

        public void CreateFeature(FrameworkElement view, string viewName)
        {
            allFeature.Add("AcceptDrop", new AcceptDropFeature());
            allFeature.Add("MouseSelect", new MouseSelectFeature());

            foreach (iPanelFeatureProperty feature in allFeature.Values)
            {
                feature.CreatePropertyFeature(view, viewName, "DiagramGroup");
            }
        }

        public void ChangeModel(iFrameElement element)
        {
            model = element;
            foreach (iPanelFeature feature in allFeature.Values)
            {
                feature.ChangeModel(element);
            }
        }
    }
}

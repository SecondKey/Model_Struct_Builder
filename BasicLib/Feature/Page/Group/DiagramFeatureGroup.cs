using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model_Struct_Builder;

namespace BasicLib
{
    class DiagramFeatureGroup : FeatureBase
    {
        Dictionary<string, iFeatureProperty> allFeature;

        public override void ChangeModel(iFrameElement element) { }

        protected override void Create(FrameworkElement viewElement, string viewName)
        {
            allFeature = new Dictionary<string, iFeatureProperty>()
            {
                {"AcceptDrop", new AcceptDropFeature() },
                {"MouseSelect", new MouseSelectFeature() },
                {"NodeControl", new NodeControlFeature() },
                { "AddAdorner",new AddAdornerFeature()},
            };

            foreach (var kv in allFeature)
            {
                (viewElement.DataContext as PackageViewModelBase).AllFeature.Add(kv.Key, kv.Value);
                kv.Value.CreatePropertyFeature(viewElement, viewName, "DiagramGroup");
            }
        }
    }
}

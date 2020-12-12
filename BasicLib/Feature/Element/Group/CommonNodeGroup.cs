using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    class CommonNodeGroup : FeatureBase
    {
        Dictionary<string, iFeatureProperty> allFeature;
        public override void ChangeModel(iFrameElement element) { }
        protected override void Create(FrameworkElement viewElement, string viewName)
        {
            allFeature = new Dictionary<string, iFeatureProperty>()
            {
                {"ItemSelected", new ItemSelectedFeature() },
                {"ItemResize",new ItemResizeFeature()},

                {"AddAdorner", new AddAdornerFeature() },
            };

            foreach (var kv in allFeature)
            {
                (viewElement.DataContext as PackageViewModelBase).AllFeature.Add(kv.Key, kv.Value);
                kv.Value.CreatePropertyFeature(viewElement, viewName, "CommonNodeGroup");
            }
        }
    }
}

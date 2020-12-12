using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BasicLib
{
    public abstract class PackageViewModelBase : AppViewModelBase
    {
        public FrameworkElement view;
        public iFrameElement model;
        public iFrameElement Model
        {
            get { return model; }
            set
            {
                model = value;
                foreach (iFeature feature in AllFeature.Values)
                {
                    feature.ChangeModel(value);
                }
            }
        }

        public PackageViewModelBase(string VMName) : base(VMName) { }
        public PackageViewModelBase(iFrameElement model) { this.model = model; }

        #region Feature
        public Dictionary<string, iFeature> AllFeature = new Dictionary<string, iFeature>();

        protected abstract iFeature GetFeature(string Featurename, string elementName);

        protected void CreateFeature()
        {
            List<string> tmp = new List<string>(AllFeature.Keys);
            foreach (string t in tmp)
            {
                AllFeature[t].CreateFeature(view, viewModelName);
            }
        }

        public void ChangeModel(iFrameElement model)
        {
            foreach (iFeature feature in AllFeature.Values)
            {
                feature.ChangeModel(model);
            }
        }
        #endregion
    }
}

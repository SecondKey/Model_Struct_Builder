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
    public class PackageViewModelBase : AppViewModelBase
    {
        iFrameElement model;

        FrameworkElement view;
        public iFrameElement Model { get { return model; } }

        #region build
        Dictionary<string, iPanelFeature> allFeature = new Dictionary<string, iPanelFeature>();

        public PackageViewModelBase(FrameworkElement view, string elementName)
        {
            MsgCenter.RegistSelf(this, AllAppMsg.PanelCreateComplete, (msg) => { CreateFeature(); });

            viewModelName = elementName;
            PanelInfo info = Frame.AllPanelInfo[elementName];
            this.view = view;
            foreach (string feature in info.feature)
            {
                allFeature.Add(feature, GetPanelFeature(feature, view, elementName));
            }
        }

        iPanelFeature GetPanelFeature(string Featurename, FrameworkElement view, string elementName)
        {
            iPanelFeature feature;
            switch (Featurename)
            {
                #region Property
                case "AcceptDrop":
                    feature = new AcceptDropFeature();
                    break;
                case "MouseSelect":
                    feature = new MouseSelectFeature();
                    break;
                #endregion

                #region Group
                case "DiagramGroup":
                    feature = new DiagramFeatureGroup();
                    break;
                #endregion
                default:
                    return null;
            }
            return feature;
        }

        void CreateFeature()
        {
            foreach (var kv in allFeature)
            {
                kv.Value.CreateFeature(view, viewModelName);
            }
        }
        #endregion
    }
}

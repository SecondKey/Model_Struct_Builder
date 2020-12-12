using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    public class PageViewModelBase : PackageViewModelBase
    {
        public PageViewModelBase(string panelName) : base(panelName)
        {
            this.RegistSelf(AllAppMsg.PanelCreateComplete, (msg) => { CreateFeature(); });

            view = Frame.AllPanel[panelName];
            PanelInfo info = Frame.AllPanelInfo[panelName];
            foreach (string feature in info.feature)
            {
                AllFeature.Add(feature, GetFeature(feature, panelName));
            }
        }

        protected override iFeature GetFeature(string Featurename, string elementName)
        {
            iFeature feature;
            switch (Featurename)
            {
                #region Property
                #region Page
                case "AcceptDrop":
                    feature = new AcceptDropFeature();
                    break;
                case "MouseSelect":
                    feature = new MouseSelectFeature();
                    break;
                #endregion 
                #region General
                case "AddAdorner":
                    feature = new AddAdornerFeature();
                    break;
                #endregion 

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
    }
}

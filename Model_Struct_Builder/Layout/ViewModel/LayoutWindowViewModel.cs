using Model_Struct_Builder.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 页面/窗口 的VM
    /// </summary>
    public class LayoutWindowViewModel : LayoutPanelViewModelBase
    {
        public LayoutWindowViewModel(PanelInfo targetStruct) : base(targetStruct)
        {
            MsgCenter.RegistSelf(this, AllAppMsg.ShowHideWindow, ChangeVisible<MsgBase>);
            MsgCenter.RegistSelf(this, AllAppMsg.AutoVisible, AutoVisible<MsgBase>);
            MsgCenter.RegistSelf(this, AllAppMsg.SaveUserVisible, SaveUserVisible<MsgBase>);
            MsgCenter.RegistSelf(this, AllAppMsg.LoadUserVisible, LoadUserVisible<MsgBase>);
        }


        #region AutoVisible
        bool userVisible = false;
        bool autoVisible = false;

        void AutoVisible<T>(MsgBase msg)
        {
            autoVisible = true;
            MsgVar<string> tmpMsg = msg as MsgVar<string>;
            if (PanelInfo.link != null)
            {
                if (PanelInfo.link.Contains(tmpMsg.parameter) && userVisible)
                {
                    IsVisible = true;
                }
                else
                {
                    IsVisible = false;
                }
            }
        }

        public void SaveUserVisible<T>(MsgBase msg)
        {
            MsgVar<string> tmpMSg = msg as MsgVar<string>;
            RWXml.TemporaryAddPropertySetContent(
                PanelInfo.name,
                userVisible.ToString(),
                FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout") + tmpMSg.parameter + ".xml",
                "UserVisible"
                );
        }

        public void LoadUserVisible<T>(MsgBase msg)
        {
            autoVisible = true;
            MsgVar<string> tmpMSg = msg as MsgVar<string>;
            userVisible = bool.Parse(RWXml.TemporaryReadContent(
                PanelInfo.name,
                FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout") + tmpMSg.parameter + ".xml",
                "UserVisible"
                ));
        }
        #endregion

        #region IsVisible
        void ChangeVisible<T>(MsgBase msg)
        {
            MsgVar<KeyValuePair<string, bool>> tmpMsg = (MsgVar<KeyValuePair<string, bool>>)msg;
            if (tmpMsg.parameter.Key == Name)
            {
                if (PanelInfo.link != null && !PanelInfo.link.Contains(ViewModelLocator.instence.Main.ActiveDocument.Name))
                {
                    ViewModelLocator.instence.Main.WindowActionList[Name].P2Property = false;
                }
                else
                {
                    IsVisible = tmpMsg.parameter.Value;
                }
            }
        }

        public bool IsVisible
        {
            get
            {
                return ViewModelLocator.instence.Main.WindowActionList[Name].P2Property;
            }
            set
            {
                if (!autoVisible)
                {
                    userVisible = value;
                }
                else
                {
                    autoVisible = false;
                }
                if (ViewModelLocator.instence.Main.WindowActionList[Name].P2Property != value)
                {
                    ViewModelLocator.instence.Main.WindowActionList[Name].P2Property = value;
                }
                RaisePropertyChanged(() => IsVisible);
            }
        }
        #endregion
    }
}

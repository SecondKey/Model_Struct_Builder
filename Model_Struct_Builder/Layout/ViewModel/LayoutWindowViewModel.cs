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
    class LayoutWindowViewModel : LayoutPanelViewModelBase
    {
        public LayoutWindowViewModel(FramePanelStruct targetStruct) : base(targetStruct)
        {
            MsgCenter.RegistSelf(this, AllAppMsg.ShowHideWindow, ChangeVisible<MsgBase>);
        }

        #region IsVisible
        void ChangeVisible<T>(MsgBase msg)
        {
            MsgVar<KeyValuePair<string, bool>> tmpMsg = (MsgVar<KeyValuePair<string, bool>>)msg;
            if (tmpMsg.parameter.Key == Name)
            {
                IsVisible = tmpMsg.parameter.Value;
            }
        }

        public bool IsVisible
        {
            get
            { 
                return ViewModelLocator.instence.Main.PageActionList[Name].P2Property;
            }
            set
            {
                if (ViewModelLocator.instence.Main.PageActionList[Name].P2Property != value)
                {
                    ViewModelLocator.instence.Main.PageActionList[Name].P2Property = value;
                }
                RaisePropertyChanged(() => IsVisible);
            }
        }
        #endregion
    }
}

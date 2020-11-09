using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 页面 的VM
    /// </summary>
    class LayoutPanelViewModelBase : AppViewModelBase
    {
        public LayoutPanelViewModelBase(FramePanelStruct targetStruct)
        {
            panelInfo = targetStruct;
        }

        #region PanelInfo
        /// <summary>
        /// panel类型所在的包
        /// </summary>
        private FramePanelStruct panelInfo;
        public FramePanelStruct PanelInfo
        {
            get { return panelInfo; }
            private set
            {
                panelInfo = value;
                RaisePropertyChanged(() => PanelInfo);
            }
        }
        #endregion

        #region Name
        /// <summary>
        /// panel的类型，在设置时根据类型来创建内容
        /// </summary>
        public string Name
        {
            get { return PanelInfo.name; }
        }
        #endregion

        #region Title
        public string Title
        {
            get { return FrameController.GetInstence().FrameDataText["Page_" + Name]; }
        }
        #endregion

        #region IsSelected

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        #endregion

        #region IsActive

        private bool _isActive = false;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        #endregion
    }
}

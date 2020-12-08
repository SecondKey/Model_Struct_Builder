using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Model_Struct_Builder
{
    /// <summary>
    /// Avalondock_MVVM 中DockingManager的LayoutItemTemplate
    /// 负责根据自身的 FramePanelStruct 来生成指定的 子页面或窗口
    /// </summary>
    public partial class LayoutPanel : UserControl
    {
        public LayoutPanel()
        {
            InitializeComponent();
            DataContextChanged += (sender, e) =>
            {
                LayoutPanelViewModelBase localVM = DataContext as LayoutPanelViewModelBase;
                Content = FrameController.GetInstence().AllPanel[localVM.PanelInfo.name];
            };
        }

        #region Old
        //public LayoutPanel()
        //{
        //    InitializeComponent();
        //    Loaded += (sender, e) =>
        //    {
        //        if (AppController.GetInstence().NowLoadPage.Contains((DataContext as LayoutPanelViewModelBase).parentName))
        //        {
        //            CreateContent();
        //        }
        //        else
        //        {
        //            MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelCreateComplete, (DataContext as LayoutPanelViewModelBase).viewModelName));

        //        }
        //    };
        //}

        ///// <summary>
        ///// 创建该页面内容
        ///// </summary>
        //void CreateContent()
        //{
        //    LayoutPanelViewModelBase localVM = DataContext as LayoutPanelViewModelBase;
        //    if (localVM.PanelInfo.type == "EmptyPanel")//如果当前页面是一个布局页面，创建一个EmptyDockingManager负责布局
        //    {
        //        DockingManagerViewModel vm = new DockingManagerViewModel(localVM.PanelInfo.name);
        //        EmptyDockingManager emptyDockingManager = new EmptyDockingManager();
        //        emptyDockingManager.DataContext = vm;
        //        Content = emptyDockingManager;
        //    }
        //    else//如果当前页面是包含具体控件，创建控件,并发送消息说明该页已经加载完成
        //    {
        //        Content = FrameController.GetInstence().GetFrameObject(localVM.PanelInfo.package, localVM.PanelInfo.type);
        //        MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelChildLoadComplete, localVM.PanelInfo.name));//页面作为一个元素加载完成，详见DockingManagerViewModel ElementLoadOver
        //    }
        //}

        //private bool disposedValue;
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposedValue)
        //    {
        //        if (disposing)
        //        {

        //        }
        //    }
        //}

        //// TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
        //~LayoutPanel()
        //{
        //    // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //    Dispose(disposing: false);
        //}

        //public void Dispose()
        //{
        //    // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
        //    Dispose(disposing: true);
        //    GC.SuppressFinalize(this);
        //}
        #endregion 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
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
        }


        public static DependencyProperty PanelInfoProperty =
            DependencyProperty.RegisterAttached(
                "PanelInfo",
                typeof(FramePanelStruct),
                typeof(LayoutPanel),
                new PropertyMetadata(new FramePanelStruct(), new PropertyChangedCallback(CreateContent)));
        /// <summary>
        /// 当前布局页面的结构
        /// </summary>
        public FramePanelStruct PanelInfo
        {
            get { return (FramePanelStruct)GetValue(PanelInfoProperty); }
            set { SetValue(PanelInfoProperty, value); }
        }

        /// <summary>
        /// 创建该页面内容
        /// </summary>
        static void CreateContent(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LayoutPanel panel = (LayoutPanel)sender;
            if (panel.PanelInfo.type == "EmptyPanel")//如果当前页面是一个布局页面，创建一个EmptyDockingManager负责布局
            {
                DockingManagerViewModel vm = new DockingManagerViewModel(panel.PanelInfo.name, panel.PanelInfo.content);
                EmptyDockingManager emptyDockingManager = new EmptyDockingManager();
                emptyDockingManager.DataContext = vm;
                panel.Content = emptyDockingManager;
            }
            else//如果当前页面是包含具体控件，创建控件,并发送消息说明该页已经加载完成
            {
                panel.Content = FrameController.GetInstence().GetFrameObject(panel.PanelInfo.package, panel.PanelInfo.type);
                MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelCreateOver, panel.PanelInfo.name));//页面作为一个元素加载完成，详见DockingManagerViewModel ElementLoadOver
            }
        }
    }
}

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
    /// LayoutPanel.xaml 的交互逻辑
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
                new PropertyMetadata(new FramePanelStruct(), new PropertyChangedCallback(CreateContentElement)));

        public FramePanelStruct PanelInfo
        {
            get { return (FramePanelStruct)GetValue(PanelInfoProperty); }
            set { SetValue(PanelInfoProperty, value); }
        }

        static void CreateContentElement(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LayoutPanel panel = (LayoutPanel)sender;
            if (panel.PanelInfo.type == "EmptyPanel")
            {
                DockingManagerViewModel vm = new DockingManagerViewModel(panel.PanelInfo.content);
                EmptyDockingManager emptyDockingManager = new EmptyDockingManager();
                emptyDockingManager.DataContext = vm;
                panel.Content = emptyDockingManager;
            }
            else
            {
                panel.Content = FrameController.GetInstence().GetFrameObject(panel.PanelInfo.package, panel.PanelInfo.type);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace Model_Struct_Builder
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MsgCenter.RegistSelf(this, AllAppMsg.PanelStructLoadComplete, StartLoadPanelStruct<MsgBase>);
            MsgCenter.SendMsg(new MsgString(AllAppMsg.LoadFrame, "Story_Design_Reviewer"));
        }

        #region ViewMenu
        void StartLoadPanelStruct<T>(MsgBase msg)
        {
            Menu_View.IsEnabled = true;
            LoadMenu(Menu_View, FrameController.GetInstence().PanelStruct);
            LoadPanel(DocumentPane, AnchorablePane, FrameController.GetInstence().PanelStruct);
        }

        void LoadMenu(MenuItem item, List<FramePanelStruct> panelStruct)
        {
            item.Items.Clear();
            foreach (FramePanelStruct tmp in panelStruct)
            {
                MenuItem i = new MenuItem();
                BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding() { Source = FrameController.GetInstence(), Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]") });
                if (tmp.content != null)
                {
                    LoadMenu(i, tmp.content);
                }
                item.Items.Add(i);
            }
        }

        void LoadPanel(LayoutDocumentPane dPanel, LayoutAnchorablePane aPanel, List<FramePanelStruct> panelStruct)
        {
            foreach (var tmp in panelStruct)
            {
                if (tmp.dockType == "Document")
                {
                    LayoutDocument panel = new LayoutDocument();
                    BindingOperations.SetBinding(panel, LayoutDocument.TitleProperty, new Binding() { Source = FrameController.GetInstence(), Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]") });
                    panel.ContentId = tmp.name;

                    if (tmp.type == "EmptyPanel")
                    {
                        CreateDockManager(panel, tmp.content);
                    }
                    else
                    {
                        object contetn = FrameController.GetInstence().GetFrameObject(tmp.package, tmp.type);
                        panel.Content = contetn;
                        dPanel.Children.Add(panel);
                    }
                }
                else
                {
                    LayoutAnchorable panel = new LayoutAnchorable();
                    BindingOperations.SetBinding(panel, LayoutAnchorable.TitleProperty, new Binding() { Source = FrameController.GetInstence(), Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]") });
                    panel.ContentId = tmp.name;

                    object contetn = FrameController.GetInstence().GetFrameObject(tmp.package, tmp.type);
                    panel.Content = contetn;
                    aPanel.Children.Add(panel);
                }
            }
        }

        void CreateDockManager(LayoutDocument document, List<FramePanelStruct> panelStruct)
        {
            DockingManager dockingManager = new DockingManager();
            LayoutRoot layoutRoot = new LayoutRoot();
            LayoutPanel layoutPanel = new LayoutPanel();

            LayoutDocumentPaneGroup layoutDocumentPaneGroup = new LayoutDocumentPaneGroup();
            LayoutDocumentPane layoutDocumentPane = new LayoutDocumentPane();
            layoutDocumentPaneGroup.Children.Add(layoutDocumentPane);

            LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = new LayoutAnchorablePaneGroup();
            LayoutAnchorablePane layoutAnchorablePane = new LayoutAnchorablePane();
            layoutAnchorablePaneGroup.Children.Add(layoutAnchorablePane);

            layoutPanel.Children.Add(layoutDocumentPaneGroup);
            layoutPanel.Children.Add(layoutAnchorablePaneGroup);
            layoutRoot.RootPanel = layoutPanel;
            dockingManager.Layout = layoutRoot;

            LoadPanel(layoutDocumentPane, layoutAnchorablePane, panelStruct);
        }
        #endregion
    }
}

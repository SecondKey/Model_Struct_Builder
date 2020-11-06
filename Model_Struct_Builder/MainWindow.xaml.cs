using GalaSoft.MvvmLight;
using Model_Struct_Builder.ViewModel;
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
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Converters;
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

        void StartLoadPanelStruct<T>(MsgBase msg)
        {
            Menu_View.IsEnabled = true;
            Menu_View.Items.Clear();
            ViewModelLocator.instence.Main.PageActionList.Clear();
            LoadMenu(Menu_View, FrameController.GetInstence().PanelStruct);
            LoadPanel();
        }

        void LoadMenu(MenuItem item, List<FramePanelStruct> panelStruct)
        {
            foreach (FramePanelStruct tmp in panelStruct)
            {
                if (tmp.content != null)
                {
                    MenuItem i = new MenuItem();
                    BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding()
                    {
                        Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]")
                    });
                    LoadMenu(i, tmp.content);
                    item.Items.Add(i);
                }
                else if (tmp.dockType == "Window")
                {
                    MenuItem i = new MenuItem();
                    BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding()
                    {
                        Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]")
                    });
                    ViewModelLocator.instence.Main.PageActionList.Add(tmp.name,new MsgKVProperty<string, bool>(AllAppMsg.ShowHideWindow, tmp.name, true));
                    BindingOperations.SetBinding(i, MenuItem.IsCheckedProperty, new Binding()
                    {
                        Path = new PropertyPath("PageActionList[" + tmp.name + "].SenderP2Property"),
                        Mode = BindingMode.TwoWay
                    });
                    i.IsCheckable = true;
                    item.Items.Add(i);
                }
            }
        }

        void LoadPanel()
        {
            DockingManagerViewModel vm = new DockingManagerViewModel(FrameController.GetInstence().PanelStruct);
            DockingBase.DataContext = vm;
        }

        //#region LoadPanel
        //void StartLoadPanelStruct<T>(MsgBase msg)
        //{
        //    Menu_View.IsEnabled = true;
        //    Menu_View.Items.Clear();
        //    ViewModelLocator.instence.Main.PageActionList.Clear();
        //    LoadMenu(Menu_View, FrameController.GetInstence().PanelStruct);
        //    LoadPanel(DocumentPane, AnchorablePane, FrameController.GetInstence().PanelStruct);
        //}

        //void LoadMenu(MenuItem item, List<FramePanelStruct> panelStruct)
        //{
        //    foreach (FramePanelStruct tmp in panelStruct)
        //    {
        //        MenuItem i = new MenuItem();
        //        BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding() { Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]") });
        //        ViewModelLocator.instence.Main.PageActionList.Add(tmp.name, true);
        //        if (tmp.content != null)
        //        {
        //            MenuItem iWindow = new MenuItem();
        //            BindingOperations.SetBinding(iWindow, MenuItem.HeaderProperty, new Binding() { Path = new PropertyPath("AppDataText[Menu_View_Window]") });
        //            BindingOperations.SetBinding(iWindow, MenuItem.IsCheckedProperty, new Binding() { Path = new PropertyPath("PageActionList[" + tmp.name + "]"), Mode = BindingMode.TwoWay });
        //            iWindow.IsCheckable = true;
        //            i.Items.Add(iWindow);

        //            LoadMenu(i, tmp.content);
        //        }
        //        else
        //        {
        //            i.IsCheckable = true;
        //            BindingOperations.SetBinding(i, MenuItem.IsCheckedProperty, new Binding() { Path = new PropertyPath("PageActionList[" + tmp.name + "]"), Mode = BindingMode.TwoWay });
        //        }
        //        item.Items.Add(i);
        //    }
        //}

        //void LoadPanel(LayoutDocumentPane dPanel, LayoutAnchorablePane aPanel, List<FramePanelStruct> panelStruct)
        //{
        //    foreach (var tmp in panelStruct)
        //    {
        //        if (tmp.dockType == "Document")
        //        {
        //            LayoutDocument panel = new LayoutDocument();
        //            panel.ContentId = tmp.name;

        //            BindingOperations.SetBinding(panel, LayoutDocument.TitleProperty, new Binding()
        //            {
        //                Source = FrameController.GetInstence(),
        //                Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]")
        //            });

        //            if (tmp.type == "EmptyPanel")
        //            {
        //                CreateDockManager(panel, tmp.content);
        //            }
        //            else
        //            {
        //                object contetn = FrameController.GetInstence().GetFrameObject(tmp.package, tmp.type);
        //                panel.Content = contetn;
        //            }

        //            dPanel.Children.Add(panel);
        //        }
        //        else
        //        {
        //            LayoutAnchorable panel = new LayoutAnchorable();
        //            panel.ContentId = tmp.name;
        //            BindingOperations.SetBinding(panel, LayoutAnchorable.TitleProperty, new Binding()
        //            {
        //                Source = FrameController.GetInstence(),
        //                Path = new PropertyPath("FrameDataText[Page_" + tmp.name + "]")
        //            });

        //            object contetn = FrameController.GetInstence().GetFrameObject(tmp.package, tmp.type);
        //            panel.Content = contetn;
        //            aPanel.Children.Add(panel);
        //        }
        //    }
        //}

        //void CreateDockManager(LayoutDocument document, List<FramePanelStruct> panelStruct)
        //{
        //    DockingManager dockingManager = new DockingManager();
        //    LayoutRoot layoutRoot = new LayoutRoot();
        //    LayoutPanel layoutPanel = new LayoutPanel();

        //    LayoutDocumentPaneGroup layoutDocumentPaneGroup = new LayoutDocumentPaneGroup();
        //    LayoutDocumentPane layoutDocumentPane = new LayoutDocumentPane();

        //    LayoutAnchorablePaneGroup layoutAnchorablePaneGroup = new LayoutAnchorablePaneGroup();
        //    LayoutAnchorablePane layoutAnchorablePane = new LayoutAnchorablePane();

        //    layoutDocumentPaneGroup.Children.Add(layoutDocumentPane);
        //    layoutAnchorablePaneGroup.Children.Add(layoutAnchorablePane);

        //    layoutPanel.Children.Add(layoutDocumentPaneGroup);
        //    layoutPanel.Children.Add(layoutAnchorablePaneGroup);

        //    layoutRoot.RootPanel = layoutPanel;
        //    dockingManager.Layout = layoutRoot;
        //    document.Content = dockingManager;

        //    LoadPanel(layoutDocumentPane, layoutAnchorablePane, panelStruct);
        //}
        //#endregion

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Root.Hidden != null)
        //    {
        //        while (Root.Hidden.Count > 0)
        //        {
        //            Root.Hidden[0].Show();//调用show方法，恢复窗体显示。
        //        }
        //    }
        //}



        void cb_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("响应自定义命令MyCommand");
        }
    }
}

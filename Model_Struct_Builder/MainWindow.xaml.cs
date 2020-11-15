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
    /// 程序的开始
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MsgCenter.RegistSelf(this, AllAppMsg.AllPanelStructLoadComplete, StartLoadPanel<MsgBase>);
        }

        /// <summary>
        /// 开始加载页面
        /// </summary>
        /// <param name="msg"></param>
        void StartLoadPanel<T>(MsgBase msg)
        {
            Menu_View.IsEnabled = true;//加载页面布局后，启用布局菜单项

            Menu_View_Window.Items.Clear();//清空原本窗口菜单
            ViewModelLocator.instence.Main.PageActionList.Clear();//清空原本窗口显示列表

            LoadMenu(Menu_View_Window, FrameController.GetInstence().PanelStruct);//加载菜单项
            LoadPanel();//加载页面
        }

        /// <summary>
        /// 递归加载菜单
        /// </summary>
        /// <param name="item">父级菜单项</param>
        /// <param name="panelStruct">菜单项列表</param>
        void LoadMenu(MenuItem item, List<FramePanelStruct> panelStruct)
        {
            foreach (FramePanelStruct tmp in panelStruct)
            {
                if (tmp.content != null)//如果该菜单项还有包含的菜单，递归加载子菜单项
                {
                    MenuItem i = new MenuItem();//新建菜单项i
                    BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding()//绑定菜单项的Header
                    {
                        Path = new PropertyPath("Frame.FrameDataText[Page_" + tmp.name + "]")
                    });
                    LoadMenu(i, tmp.content);//递归加载i的子菜单项
                    item.Items.Add(i);//将i添加到父级菜单项中
                }
                else if (tmp.dockType == "Window")//如果菜单项布局类型是Window，为菜单项添加关闭打开页面的功能（Page类型默认不允许关闭，所以没有菜单项）
                {
                    MenuItem i = new MenuItem();//新建菜单项i
                    BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding()//绑定菜单项的Header
                    {
                        Path = new PropertyPath("Frame.FrameDataText[Page_" + tmp.name + "]")
                    });
                    ViewModelLocator.instence.Main.PageActionList.Add(tmp.name, new MsgKVProperty<string, bool>(AllAppMsg.ShowHideWindow, tmp.name, true));//在窗口显示列表中新增一项
                    BindingOperations.SetBinding(i, MenuItem.IsCheckedProperty, new Binding()//绑定IsChecked属性到窗口显示列表
                    {
                        Path = new PropertyPath("PageActionList[" + tmp.name + "].SenderP2Property"),
                        Mode = BindingMode.TwoWay
                    });
                    i.IsCheckable = true;//指定菜单项是可选项
                    item.Items.Add(i);//将i添加到父级菜单项中
                }
            }
        }

        /// <summary>
        /// 加载页面
        /// BaseDockingPage是布局的根元素
        /// </summary>
        void LoadPanel()
        {
            DockingManagerViewModel vm = new DockingManagerViewModel("BaseDocking", FrameController.GetInstence().PanelStruct);//创建DockingManagerViewModel实例
            EmptyDockingManager BaseDocking = new EmptyDockingManager();
            BaseDocking.DataContext = vm;//设置BaseDockingPage的ViewModel
            WorkingArea.Children.Add(BaseDocking);
        }


        #region OldLoadPanel
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
        #endregion

    }
}

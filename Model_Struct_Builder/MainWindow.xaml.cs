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
using Xceed.Wpf.AvalonDock.Layout.Serialization;

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
            this.RegistSelf(AllAppMsg.PanelCreateComplete, StartLoadPanel<MsgBase>);

            this.RegistSelf(AllAppMsg.LoadLayout, (msg) =>
            {
                MsgVar<string> tmpMsg = (MsgVar<string>)msg;
                LoadLayout(tmpMsg.parameter);
            });
            this.RegistSelf( AllAppMsg.SaveLayout, (msg) =>
            {
                MsgVar<string> tmpMsg = (MsgVar<string>)msg;
                SaveLayout(tmpMsg.parameter);
            });

            Closing += (sender, e) => { if (!string.IsNullOrEmpty(FrameController.GetInstence().frameName)) { SaveLayout("Last"); } };
        }

        /// <summary>
        /// 开始加载页面
        /// </summary>
        /// <param name="msg"></param>
        void StartLoadPanel<T>(MsgBase msg)
        {
            Menu_View.IsEnabled = true;//加载页面布局后，启用布局菜单项
            ViewModelLocator.instence.Main.WindowActionList.Clear();//清空原本窗口显示列表
            LoadMenu();//加载菜单项
            MsgCenter.SendMsg(new MsgBase(AllAppMsg.MenuLoadComplete));
        }

        /// <summary>
        /// 加载菜单
        /// </summary>
        /// <param name="item">父级菜单项</param>
        /// <param name="panelStruct">菜单项列表</param>
        void LoadMenu()
        {
            Menu_View_Window.Items.Clear();//清空原本窗口菜单

            foreach (var kv in FrameController.GetInstence().AllPanelInfo)
            {
                if (kv.Value.panelType == PanelType.Page && kv.Value.link.Count != 0)
                {
                    MenuItem i = new MenuItem();//新建菜单项i
                    BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding()
                    {
                        Path = new PropertyPath("Frame.FrameDataText[Page_" + kv.Key + "]")
                    });//绑定菜单项的Header
                    foreach (string t in kv.Value.link)
                    {
                        i.Items.Add(CreateCheckableMenuItem(t));
                    }
                    Menu_View_Window.Items.Add(i);
                }
            }
            Separator s = new Separator();
            Menu_View_Window.Items.Add(s);
            foreach (var kv in FrameController.GetInstence().AllPanelInfo)
            {
                if (kv.Value.panelType == PanelType.Window)
                {
                    ViewModelLocator.instence.Main.WindowActionList.Add(kv.Key, true);//在窗口显示列表中新增一项
                    Menu_View_Window.Items.Add(CreateCheckableMenuItem(kv.Key));
                }
            }
        }

        MenuItem CreateCheckableMenuItem(string itemName)
        {
            MenuItem i = new MenuItem();//新建菜单项i
            BindingOperations.SetBinding(i, MenuItem.HeaderProperty, new Binding()
            {
                Path = new PropertyPath("Frame.FrameDataText[Page_" + itemName + "]")
            });//绑定菜单项的Header
            BindingOperations.SetBinding(i, MenuItem.IsCheckedProperty, new Binding()
            {
                Path = new PropertyPath("WindowActionList[" + itemName + "]"),
                Mode = BindingMode.TwoWay
            });//绑定IsChecked属性到窗口显示列表
            i.IsCheckable = true;//指定菜单项是可选项
            return i;
        }

        void LoadLayout(string LayoutName)
        {
            if (!string.IsNullOrEmpty(LayoutName))
            {
                using (AppController.GetInstence().LoadLayoutState.SetScope())
                {
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadUserVisible, LayoutName));
                    List<string> tmp = new List<string>(ViewModelLocator.instence.Main.WindowActionList.Keys);
                    foreach (string t in tmp)
                    {
                        ViewModelLocator.instence.Main.WindowActionList[t] = true;
                    }

                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadUserVisible, LayoutName));
                    XmlLayoutSerializer serializer = new XmlLayoutSerializer(WorkingArea);//创建序列化器
                    serializer.Deserialize(FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout") + LayoutName + ".xml");
                }
            }
        }

        void SaveLayout(string LayoutName)
        {
            if (!string.IsNullOrEmpty(LayoutName))
            {
                XmlLayoutSerializer serializer = new XmlLayoutSerializer(WorkingArea);//创建序列化器
                serializer.Serialize(FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout") + LayoutName + ".xml");//根据路径储存布局
                MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.SaveUserVisible, LayoutName));
            }
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

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Model_Struct_Builder
{
    /// <summary>
    /// EmptyDockingManager 的VM
    /// </summary>
    class DockingManagerViewModel : AppViewModelBase
    {
        public DockingManagerViewModel(string name, List<FramePanelStruct> targetList)
        {
            this.viewModelName = name;
            MsgCenter.RegistSelf(this, AllAppMsg.PanelCreateComplete, CreatePanelLoadNext<MsgBase>);
            MsgCenter.RegistSelf(this, AllAppMsg.PanelChildLoadComplete, LoadChildLoadNext<MsgBase>);


            foreach (FramePanelStruct panel in targetList)//创建VM时，添加LayoutItem
            {
                checkList.Add(panel.name, false);
                if (panel.dockType == "Page")
                {
                    LayoutPageViewModel tmp = new LayoutPageViewModel(name, panel);
                    pages.Add(tmp);
                    pageList.Add(panel.name, pageList.Count);
                }
                else
                {
                    LayoutWindowViewModel tmp = new LayoutWindowViewModel(name, panel);
                    windows.Add(tmp);
                }
            }
        }
        /// <summary>
        /// 详见Avalondock_MVVM （Files）
        /// </summary>
        ObservableCollection<LayoutPageViewModel> pages = new ObservableCollection<LayoutPageViewModel>();
        ReadOnlyObservableCollection<LayoutPageViewModel> readonyPages = null;
        public ReadOnlyObservableCollection<LayoutPageViewModel> Pages
        {
            get
            {
                if (readonyPages == null)
                    readonyPages = new ReadOnlyObservableCollection<LayoutPageViewModel>(pages);
                return readonyPages;
            }
        }

        /// <summary>
        /// 详见Avalondock_MVVM （Tools）
        /// </summary>
        ObservableCollection<LayoutWindowViewModel> windows = new ObservableCollection<LayoutWindowViewModel>();
        ReadOnlyObservableCollection<LayoutWindowViewModel> readonyWindows = null;
        public ReadOnlyObservableCollection<LayoutWindowViewModel> Windows
        {
            get
            {
                if (readonyWindows == null)
                    readonyWindows = new ReadOnlyObservableCollection<LayoutWindowViewModel>(windows);
                return readonyWindows;
            }
        }

        #region ActiveDocument
        /// <summary>
        /// 当前选择的页，详见Avalondock_MVVM ActiveDocument
        /// </summary>
        private LayoutPageViewModel _activeDocument = null;
        public LayoutPageViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                RaisePropertyChanged(() => ActiveDocument);
                if (ActiveDocumentChanged != null)
                    ActiveDocumentChanged(this, EventArgs.Empty);
            }
        }
        public event EventHandler ActiveDocumentChanged;
        #endregion

        #region Load 
        //页面创建共计将全部页面遍历执行三遍
        //第一遍用于创建所有子元素的实例，如果不创建，无法加载布局
        //第二遍用于加载后保证所有元素已经获取DataContext，加载布局会重新创建子元素
        //第三遍用于创建子元素内容

        /// <summary>
        /// 检查队列，用于检查子元素是否全部加载完成
        /// </summary>
        Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        /// <summary>
        /// 页在 pages字典 中的位置的队列
        /// </summary>
        Dictionary<string, int> pageList = new Dictionary<string, int>();

        /// <summary>
        /// 加载布局时加载下一个页面
        /// </summary>
        void CreatePanelLoadNext<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            if (checkList.ContainsKey(tmpMsg.parameter))
            {
                checkList[tmpMsg.parameter] = true;//指定checkList中该元素已经加载完成
                if (!checkList.ContainsValue(false))//当所有的子元素全部加载完
                {
                    MsgCenter.UnRegistSelf(this, AllAppMsg.PanelCreateComplete);
                    AppController.GetInstence().NowLoadPage.Add(viewModelName);
                    ClearCheckList();
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, viewModelName));
                    //MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelLoadChild, viewModelName));//自己作为元素已经加载完成
                    ActiveDocument = pages[0];
                }
                else if (pageList.ContainsKey(tmpMsg.parameter) && pageList.Count > pageList[tmpMsg.parameter] + 1)//如果没有全部加载完成，且加载完成的元素是一个页，且不是最后一个页
                {
                    ActiveDocument = pages[pageList[tmpMsg.parameter] + 1];//显示并加载下一个页
                }
            }
        }

        /// <summary>
        /// 当有一个元素加载完成时执行
        /// 仅当页面显示时会加载页面的内容
        /// 当页面不显示时，不加载内容，也就无法加载布局
        /// 当一个子元素加载完成后，显示下一个子元素，遍历显示所有子元素触发加载布局方法
        /// <summary>
        public void LoadChildLoadNext<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            if (checkList.ContainsKey(tmpMsg.parameter))
            {
                checkList[tmpMsg.parameter] = true;//指定checkList中该元素已经加载完成
                Console.WriteLine(tmpMsg.parameter);
                if (!checkList.ContainsValue(false))//当所有的子元素全部加载完
                {
                    MsgCenter.UnRegistSelf(this, AllAppMsg.PanelChildLoadComplete);//注销消息接收
                    AppController.GetInstence().NowLoadPage.Remove(viewModelName);
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelChildLoadComplete, viewModelName));//自己作为元素已经加载完成
                }
                else if (pageList.ContainsKey(tmpMsg.parameter) && pageList.Count > pageList[tmpMsg.parameter] + 1)//如果没有全部加载完成，且加载完成的元素是一个页，且不是最后一个页
                {
                    ActiveDocument = pages[pageList[tmpMsg.parameter] + 1];//显示并加载下一个页
                }
            }
        }

        /// <summary>
        /// 用于重置检查队列
        /// </summary>
        void ClearCheckList()
        {
            Dictionary<string, bool> tmp = new Dictionary<string, bool>((IDictionary<string, bool>)checkList);
            checkList.Clear();
            foreach (var kv in tmp)
            {
                checkList.Add(kv.Key, false);
            }
        }

        void ChildLoadLayout<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            if (tmpMsg.parameter == viewModelName)
            {
                foreach (LayoutPageViewModel p in pages)
                {
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, p.viewModelName));
                }
            }
        }

        #endregion

        #region Old

        ////页面创建共计将全部页面遍历执行三遍
        ////第一遍用于创建所有子元素的实例，如果不创建，无法加载布局
        ////第二遍用于加载后保证所有元素已经获取DataContext，加载布局会重新创建子元素
        ////第三遍用于创建子元素内容

        ///// <summary>
        ///// 当前是否已经进入加载布局阶段
        ///// </summary>
        //bool NowLoadLayout = false;
        ///// <summary>
        ///// 检查队列，用于检查子元素是否全部加载完成
        ///// </summary>
        //Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        ///// <summary>
        ///// 页在 pages字典 中的位置的队列
        ///// </summary>
        //Dictionary<string, int> pageList = new Dictionary<string, int>();

        ///// <summary>
        ///// 加载布局时加载下一个页面
        ///// </summary>
        //void CreatePanelLoadNext<T>(MsgBase msg)
        //{
        //    MsgVar<string> tmpMsg = (MsgVar<string>)msg;
        //    if (checkList.ContainsKey(tmpMsg.parameter))
        //    {
        //        checkList[tmpMsg.parameter] = true;//指定checkList中该元素已经加载完成
        //        if (!checkList.ContainsValue(false))//当所有的子元素全部加载完
        //        {
        //            ActiveDocument = pages[0];
        //            if (!NowLoadLayout)
        //            {
        //                NowLoadLayout = true;
        //                AppController.GetInstence().TestToken = 2;
        //                ClearCheckList();
        //                MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, viewModelName));
        //            }
        //            else
        //            {
        //                MsgCenter.UnRegistSelf(this, AllAppMsg.PanelCreateComplete);//注销消息接收   
        //                ClearCheckList();
        //                MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelLoadChild, viewModelName));//自己作为元素已经加载完成
        //            }
        //        }
        //        else if (pageList.ContainsKey(tmpMsg.parameter) && pageList.Count > pageList[tmpMsg.parameter] + 1)//如果没有全部加载完成，且加载完成的元素是一个页，且不是最后一个页
        //        {
        //            ActiveDocument = pages[pageList[tmpMsg.parameter] + 1];//显示并加载下一个页
        //        }
        //    }
        //}

        ///// <summary>
        ///// 当有一个元素加载完成时执行
        ///// 仅当页面显示时会加载页面的内容
        ///// 当页面不显示时，不加载内容，也就无法加载布局
        ///// 当一个子元素加载完成后，显示下一个子元素，遍历显示所有子元素触发加载布局方法
        ///// <summary>
        //public void LoadChildLoadNext<T>(MsgBase msg)
        //{
        //    MsgVar<string> tmpMsg = (MsgVar<string>)msg;
        //    if (checkList.ContainsKey(tmpMsg.parameter))
        //    {
        //        checkList[tmpMsg.parameter] = true;//指定checkList中该元素已经加载完成
        //        if (!checkList.ContainsValue(false))//当所有的子元素全部加载完
        //        {
        //            MsgCenter.UnRegistSelf(this, AllAppMsg.PanelChildLoadComplete);//注销消息接收   
        //            MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelChildLoadComplete, viewModelName));//自己作为元素已经加载完成
        //        }
        //        else if (pageList.ContainsKey(tmpMsg.parameter) && pageList.Count > pageList[tmpMsg.parameter] + 1)//如果没有全部加载完成，且加载完成的元素是一个页，且不是最后一个页
        //        {
        //            ActiveDocument = pages[pageList[tmpMsg.parameter] + 1];//显示并加载下一个页
        //        }
        //    }
        //}

        ///// <summary>
        ///// 用于重置检查队列
        ///// </summary>
        //void ClearCheckList()
        //{
        //    Dictionary<string, bool> tmp = new Dictionary<string, bool>((IDictionary<string, bool>)checkList);
        //    checkList.Clear();
        //    foreach (var kv in tmp)
        //    {
        //        checkList.Add(kv.Key, false);
        //    }
        //}

        //void ChildLoadLayout<T>(MsgBase msg)
        //{
        //    MsgVar<string> tmpMsg = (MsgVar<string>)msg;
        //    if (tmpMsg.parameter == viewModelName)
        //    {
        //        foreach (LayoutPageViewModel p in pages)
        //        {
        //            MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, p.viewModelName));
        //        }
        //    }
        //}


        //public void StartCreateChild<T>(MsgBase msg)
        //{
        //if (checkList.ContainsKey(tmpMsg.parameter))
        //{
        //    checkList[tmpMsg.parameter] = true;
        //    Console.WriteLine(tmpMsg.parameter);
        //    if (checkList.ContainsValue(false))
        //    {
        //        //if (pageList.ContainsKey(tmpMsg.parameter))
        //        //{
        //        //    ShowNext(tmpMsg.parameter);
        //        //}
        //    }
        //    else
        //    {
        //        ViewFunction("LoadLayout");
        //        foreach (var kv in pages)
        //        {
        //            kv.LoadContene();
        //        }
        //        //Dictionary<string, int> tmp = new Dictionary<string, int>((IDictionary<string, int>)checkList);
        //        //checkList.Clear();
        //        //foreach (var kv in tmp)
        //        //{
        //        //    checkList.Add(kv.Key, false);
        //        //}
        //    }
        //}
        //}
        //public void ShowNext(string panelName)
        //{
        //    ActiveDocument = pages[pageList[panelName] + 1];//显示下一个页
        //}

        //public void StartCreateChild<T>(MsgBase msg)
        //{
        //    MsgVar<string> tmpMsg = (MsgVar<string>)msg;
        //    if (tmpMsg.parameter == viewModelName)
        //    {
        //        ActiveDocument = pages[0];
        //        MsgCenter.RegistSelf(this, AllAppMsg.PanelLoadComplete, LoadChild<MsgBase>);
        //        foreach (LayoutWindowViewModel w in windows)
        //        {
        //            w.LoadContene();
        //        }
        //        //ActiveDocument.LoadContene();
        //    }
        //}
        #endregion
    }
}

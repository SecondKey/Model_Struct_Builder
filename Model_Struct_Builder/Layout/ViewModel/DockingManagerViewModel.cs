using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        /// <summary>
        /// EmptyDockingManager的名字，保存布局和加载布局时使用，用于指定布局文件的文件名
        /// </summary>
        public string name;

        public DockingManagerViewModel(string name, List<FramePanelStruct> targetList)
        {
            MsgCenter.RegistSelf(this, AllAppMsg.PanelCreateOver, ElementLoadOver<MsgBase>);

            this.name = name;

            foreach (FramePanelStruct panel in targetList)//创建VM时，添加LayoutItem
            {
                checkList.Add(panel.name, false);
                if (panel.dockType == "Page")
                {
                    LayoutPageViewModel tmp = new LayoutPageViewModel(panel);
                    pages.Add(tmp);
                    pageList.Add(panel.name, pageList.Count);
                }
                else
                {
                    LayoutWindowViewModel tmp = new LayoutWindowViewModel(panel);
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
                if (_activeDocument != value)
                {
                    _activeDocument = value;
                    RaisePropertyChanged(() => ActiveDocument);
                    if (ActiveDocumentChanged != null)
                        ActiveDocumentChanged(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler ActiveDocumentChanged;
        #endregion

        #region LoadPage
        /// <summary>
        /// 检查队列，用于检查子元素是否全部加载完成
        /// </summary>
        Dictionary<string, bool> checkList = new Dictionary<string, bool>();
        /// <summary>
        /// 页在 pages字典 中的位置的队列
        /// </summary>
        Dictionary<string, int> pageList = new Dictionary<string, int>();

        /// <summary>
        /// 当有一个元素加载完成时
        /// </summary>
        public void ElementLoadOver<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            if (checkList.ContainsKey(tmpMsg.parameter))//当元素是自己的子元素时
            {
                checkList[tmpMsg.parameter] = true;//指定checkList中该元素已经加载完成
                if (!checkList.ContainsValue(false))//当所有的子元素全部加载完
                {
                    MsgCenter.UnRegistSelf(this, AllAppMsg.PanelCreateOver);//注销消息接收
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.PanelCreateOver, name));//自己作为元素已经加载完成
                }
                else if (pageList.ContainsKey(tmpMsg.parameter) && pageList.Count > pageList[tmpMsg.parameter] + 1)//如果没有全部加载完成，且加载完成的元素是一个页，且不是最后一个页
                {
                    ActiveDocument = pages[pageList[tmpMsg.parameter] + 1];//显示并加载下一个页
                }
            }
        }
        #endregion
    }
}

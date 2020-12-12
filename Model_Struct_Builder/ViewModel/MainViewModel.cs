using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Model_Struct_Builder
{
    /// <summary>
    /// MainWindow的ViewModel
    /// </summary>
    public class MainViewModel : AppViewModelBase
    {
        public MainViewModel()
        {
            viewModelName = "Main";
            this.RegistSelf(AllAppMsg.MenuLoadComplete, (msg) => { LoadPanel(); });
        }

        #region View
        #region WindowActionList
        //ObservableDictionary<string, MsgKVProperty<string, bool>> windowActionList = new ObservableDictionary<string, MsgKVProperty<string, bool>>();
        ///// <summary>
        ///// 窗口显示列表，用于绑定窗口是否显示
        ///// 两个string值一样，都是页面的name。第一个string用于字典查找value，第二个value用于MKVP发送消息的消息验证
        ///// </summary>
        //public ObservableDictionary<string, MsgKVProperty<string, bool>> WindowActionList
        //{
        //    get { return windowActionList; }
        //    set
        //    {
        //        windowActionList = value;
        //        RaisePropertyChanged(() => WindowActionList);
        //    }
        //}

        ObservableDictionary<string, bool> windowActionList = new ObservableDictionary<string, bool>();
        /// <summary>
        /// 窗口显示列表，用于绑定窗口是否显示
        /// 两个string值一样，都是页面的name。第一个string用于字典查找value，第二个value用于MKVP发送消息的消息验证
        /// </summary>
        public ObservableDictionary<string, bool> WindowActionList
        {
            get { return windowActionList; }
            set
            {
                windowActionList = value;
                RaisePropertyChanged(() => WindowActionList);
            }
        }
        #endregion

        #region AvalonBinding
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

        /// <summary>
        /// 当前选择的页，详见Avalondock_MVVM ActiveDocument
        /// </summary>
        private LayoutPageViewModel _activeDocument;
        public LayoutPageViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                RaisePropertyChanged(() => ActiveDocument);
                if (ActiveDocumentChanged != null)
                    ActiveDocumentChanged(this, new VarEventArgs<PanelInfo>(value.PanelInfo));
            }
        }
        public event EventHandler ActiveDocumentChanged;
        #endregion

        #region Load
        public void LoadPanel()
        {
            foreach (var panel in FrameController.GetInstence().AllPanelInfo)//创建VM时，添加LayoutItem
            {
                switch (panel.Value.panelType)
                {
                    case PanelType.Page:
                        LayoutPageViewModel tmpPage = new LayoutPageViewModel(panel.Value);
                        pages.Add(tmpPage);
                        break;
                    case PanelType.Window:
                        LayoutWindowViewModel tmpWindow = new LayoutWindowViewModel(panel.Value);
                        windows.Add(tmpWindow);
                        break;
                }
            }
            MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, "Last"));
        }

        #endregion
        #endregion

        #region ChangeLanguage
        public RelayCommand<string> ChangeLanguageCommand
        {
            get
            {
                return new RelayCommand<string>((p) => { AppController.GetInstence().Language = p; });
            }
        }
        #endregion

        #region Frame
        /// <summary>
        /// 加载布局
        /// </summary>
        public RelayCommand LoadLayout
        {
            get
            {
                return new RelayCommand(() =>
                {
                    DialogueWindowController.ShowLoadLayoutWindow();
                });
            }
        }
        /// <summary>
        /// 保存布局
        /// </summary>
        public RelayCommand SaveLayout
        {
            get
            {
                return new RelayCommand(() =>
                {
                    DialogueWindowController.ShowSaveLayoutWindow();
                });
            }
        }
        #endregion

        #region Test
        public RelayCommand LoadFrame
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadFrame, "Story_Design_Reviewer"));//发送-加载框架--Test
                });
            }
        }

        public RelayCommand LoadEventLayout
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, "Event"));//发送-加载框架--Test
                });
            }
        }
        #endregion


    }
}
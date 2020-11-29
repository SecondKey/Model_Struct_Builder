using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Model_Struct_Builder
{
    /// <summary>
    /// MainWindow��ViewModel
    /// </summary>
    public class MainViewModel : AppViewModelBase
    {
        public MainViewModel()
        {
            viewModelName = "Main";
            MsgCenter.RegistSelf(this, AllAppMsg.MenuLoadComplete, (msg) => { LoadPanel(); });
        }

        #region View
        #region WindowActionList
        ObservableDictionary<string, MsgKVProperty<string, bool>> windowActionList = new ObservableDictionary<string, MsgKVProperty<string, bool>>();
        /// <summary>
        /// ������ʾ�б����ڰ󶨴����Ƿ���ʾ
        /// ����stringֵһ��������ҳ���name����һ��string�����ֵ����value���ڶ���value����MKVP������Ϣ����Ϣ��֤
        /// </summary>
        public ObservableDictionary<string, MsgKVProperty<string, bool>> WindowActionList
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
        /// ���Avalondock_MVVM ��Files��
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
        /// ���Avalondock_MVVM ��Tools��
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
        /// ��ǰѡ���ҳ�����Avalondock_MVVM ActiveDocument
        /// </summary>
        private LayoutPageViewModel _activeDocument;
        public LayoutPageViewModel ActiveDocument
        {
            get { return _activeDocument; }
            set
            {
                _activeDocument = value;
                RaisePropertyChanged(() => ActiveDocument);
                MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.AutoVisible, value.Name));
                if (ActiveDocumentChanged != null)
                    ActiveDocumentChanged(this, EventArgs.Empty);
            }
        }
        public event EventHandler ActiveDocumentChanged;
        #endregion

        #region Load
        public void LoadPanel()
        {
            foreach (var page in FrameController.GetInstence().AllPage)//����VMʱ�����LayoutItem
            {
                LayoutPageViewModel tmp = new LayoutPageViewModel(page.Value);
                pages.Add(tmp);
            }
            foreach (var window in FrameController.GetInstence().AllWindow)
            {
                LayoutWindowViewModel tmp = new LayoutWindowViewModel(window.Value);
                windows.Add(tmp);
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
        /// ���ز���
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
        /// ���沼��
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
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadFrame, "Story_Design_Reviewer"));//����-���ؿ��--Test
                });
            }
        }

        public RelayCommand LoadEventLayout
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, "Event"));//����-���ؿ��--Test
                });
            }
        }
        #endregion

        
    }
}
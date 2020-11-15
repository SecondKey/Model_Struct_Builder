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
            MsgCenter.RegistSelf(this, AllAppMsg.PanelChildLoadComplete, LoadPanelOver<MsgBase>);
        }

        #region View
        ObservableDictionary<string, MsgKVProperty<string, bool>> pageActionList = new ObservableDictionary<string, MsgKVProperty<string, bool>>();
        /// <summary>
        /// 窗口显示列表，用于绑定窗口是否显示
        /// 两个string值一样，都是页面的name。第一个string用于字典查找value，第二个value用于MKVP发送消息的消息验证
        /// </summary>
        public ObservableDictionary<string, MsgKVProperty<string, bool>> PageActionList
        {
            get { return pageActionList; }
            set
            {
                pageActionList = value;
                RaisePropertyChanged(() => PageActionList);
            }
        }

        /// <summary>
        /// 页面加载完成
        /// </summary>
        /// <param name="msg">如果当前加载完成的页面是main</param>
        void LoadPanelOver<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            if (tmpMsg.parameter == "BaseDocking")
            {
                Console.WriteLine(123);
            }
        }
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
                    LoadLayoutMethod();
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
                return new RelayCommand(() => { MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.SaveLayout, "Common")); });
            }
        }

        public void LoadLayoutMethod()
        {
            foreach (var kv in PageActionList)
            {
                kv.Value.SenderP2Property = true;
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
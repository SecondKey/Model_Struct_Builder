using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
                return new RelayCommand(() => { MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.SaveLayout, "CommonLayout1")); });
            }
        }

        public void LoadLayoutMethod()
        {
            foreach (var kv in PageActionList)
            {
                kv.Value.SenderP2Property = true;
            }
            MsgCenter.SendMsg(new MsgVar<string>(AllAppMsg.LoadLayout, "CommonLayout1"));
        }
        #endregion
    }
}
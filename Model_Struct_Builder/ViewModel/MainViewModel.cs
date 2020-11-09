using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        #region View
        ObservableDictionary<string, MsgKVProperty<string, bool>> pageActionList = new ObservableDictionary<string, MsgKVProperty<string, bool>>();
        /// <summary>
        /// ������ʾ�б����ڰ󶨴����Ƿ���ʾ
        /// ����stringֵһ��������ҳ���name����һ��string�����ֵ����value���ڶ���value����MKVP������Ϣ����Ϣ��֤
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
        /// ���ز���
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
        /// ���沼��
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
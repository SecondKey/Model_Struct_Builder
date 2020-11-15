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
            MsgCenter.RegistSelf(this, AllAppMsg.PanelChildLoadComplete, LoadPanelOver<MsgBase>);
        }

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

        /// <summary>
        /// ҳ��������
        /// </summary>
        /// <param name="msg">�����ǰ������ɵ�ҳ����main</param>
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
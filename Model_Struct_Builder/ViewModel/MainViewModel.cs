using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Model_Struct_Builder
{
    public class MainViewModel : AppViewModelBase
    {
        #region View
        ObservableDictionary<string, MsgKVProperty<string, bool>> pageActionList = new ObservableDictionary<string, MsgKVProperty<string, bool>>();

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

        private string testText = "12345";

        public string TestText
        {
            get { return testText; }
            set
            {
                TestText = value;
                RaisePropertyChanged(() => TestText);
            }
        }

        #region CloseCommand
        RelayCommand _closeCommand = null;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(() => show(), true);
                }
                return _closeCommand;
            }
        }

        void show()
        {
            System.Console.WriteLine("bangding");
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
    }
}
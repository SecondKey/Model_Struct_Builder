using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model_Struct_Builder
{
    public class MainViewModel : AppViewModelBase
    {
        private ObservableCollection<string> testList = new ObservableCollection<string>() { "123", "123" ,"654"};

        public ObservableCollection<string> TestList
        {
            get { return testList; }
            set
            {
                testList = value;
                RaisePropertyChanged(() => TestList);
            }
        }
    }
}
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    class DockingManagerViewModel : AppViewModelBase
    {
        public DockingManagerViewModel(List<FramePanelStruct> targetList)
        {
            foreach (FramePanelStruct panel in targetList)
            {
                if (panel.dockType == "Page")
                {
                    LayoutPageViewModel tmp = new LayoutPageViewModel(panel);
                    pages.Add(tmp);
                }
                else
                {
                    LayoutWindowViewModel tmp = new LayoutWindowViewModel(panel);
                    windows.Add(tmp);
                }
            }
        }

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
    }
}

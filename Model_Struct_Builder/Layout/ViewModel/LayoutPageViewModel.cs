using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Model_Struct_Builder
{
    class LayoutPageViewModel : LayoutPanelViewModelBase
    {
        static ImageSourceConverter ISC = new ImageSourceConverter();

        public LayoutPageViewModel(FramePanelStruct targetStruct) : base(targetStruct)
        {
        }

        #region CloseCommand
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(() => { }, false);
            }
        }
        #endregion

    }
}

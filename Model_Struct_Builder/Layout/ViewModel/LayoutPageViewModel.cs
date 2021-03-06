﻿using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 页面/页 的VM
    /// </summary>
    public class LayoutPageViewModel : LayoutPanelViewModelBase
    {
        static ImageSourceConverter ISC = new ImageSourceConverter();

        public LayoutPageViewModel(PanelInfo targetStruct) : base(targetStruct)
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

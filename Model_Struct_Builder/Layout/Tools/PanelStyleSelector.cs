using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Model_Struct_Builder
{
    class PanelStyleSelector : StyleSelector
    {
        public Style WindowStyle
        {
            get;
            set;
        }

        public Style PageStyle
        {
            get;
            set;
        }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is LayoutWindowViewModel)
                return WindowStyle;

            if (item is LayoutPageViewModel)
                return PageStyle;

            return base.SelectStyle(item, container);
        }
    }
}

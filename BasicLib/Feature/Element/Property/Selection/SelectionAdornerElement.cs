using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicLib
{
    public class SelectionAdornerElement : Control
    {
        static SelectionAdornerElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectionAdornerElement), new FrameworkPropertyMetadata(typeof(SelectionAdornerElement)));
        }
    }
}

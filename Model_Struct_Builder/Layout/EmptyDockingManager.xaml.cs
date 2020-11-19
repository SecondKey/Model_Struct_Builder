using System;
using System.Collections.Generic;
using System.IO;
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
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 空的 AvalonDock_MVVM 的 DockingManager
    /// </summary>
    public partial class EmptyDockingManager : DockingManager
    {
        public EmptyDockingManager()
        {
            InitializeComponent();
        }
    }
}

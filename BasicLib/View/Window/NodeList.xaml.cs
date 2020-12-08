using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Model_Struct_Builder;
namespace BasicLib
{
    /// <summary>
    /// NodeList.xaml 的交互逻辑
    /// </summary>
    public partial class NodeList : UserControl
    {
        public NodeList()
        {
            InitializeComponent();
            DataContextChanged += (sender, e) =>
            {
                if (e.NewValue is NodeListViewModel)
                {
                    AddNode();
                }
            };
        }


        void AddNode()
        {
            NodeListViewModel localVM = DataContext as NodeListViewModel;
            foreach (var vm in localVM.NodeList)
            {
                var node = vm.GetNode();
                node.DataContext = vm;
                node.Tag = vm.viewModelName;
                node.Width = 90;
                node.Height = 45;
                node.Margin = new Thickness(5);
                DragElement.Items.Add(node);
            }
        }

        public void ListBox_Answers_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        #region Old


        //public static DependencyProperty elementList = DependencyProperty.Register(
        //    "ElementList",
        //    typeof(ObservableCollection<string>),
        //    typeof(NodeList));

        //public ObservableCollection<string> ElementList
        //{
        //    get { return (ObservableCollection<string>)GetValue(elementList); }
        //    set { SetValue(elementList, value); }
        //}
        #endregion
    }
}

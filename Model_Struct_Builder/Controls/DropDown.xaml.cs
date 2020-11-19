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

namespace Model_Struct_Builder
{
    /// <summary>
    /// DropDown.xaml 的交互逻辑
    /// </summary>
    public partial class DropDown : UserControl
    {
        public DropDown()
        {
            InitializeComponent();
        }

        public static DependencyProperty InputAreaWidthProperty = DependencyProperty.Register
            (
                "InputAreaWidth",
                typeof(int),
                typeof(DropDown),
                new PropertyMetadata(300)
            );

        public static DependencyProperty InputNameProperty = DependencyProperty.Register
            (
                "InputName",
                typeof(string),
                typeof(DropDown),
                new PropertyMetadata("测试")
            );

        public static DependencyProperty InputListProperty = DependencyProperty.Register
            (
                "InputList",
                typeof(List<string>),
                typeof(DropDown),
                new PropertyMetadata(new List<string>())
            );


        public static DependencyProperty SelectedItemProperty = DependencyProperty.Register
            (
                "SelectedItem",
                typeof(string),
                typeof(DropDown),
                new PropertyMetadata("")
            );

        public int InputAreaWidth
        {
            get { return (int)GetValue(InputAreaWidthProperty); }
            set { SetValue(InputAreaWidthProperty, value); }
        }

        public string InputName
        {
            get { return (string)GetValue(InputNameProperty); }
            set { SetValue(InputNameProperty, value); }
        }

        public List<string> InputList
        {
            get { return (List<string>)GetValue(InputListProperty); }
            set { SetValue(InputListProperty, value); }
        }

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
    }
}

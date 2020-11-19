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
    /// InputLine.xaml 的交互逻辑
    /// </summary>
    public partial class InputLine : UserControl
    {
        public InputLine()
        {
            InitializeComponent();
        }

        public static DependencyProperty InputAreaWidthProperty = DependencyProperty.Register
            (
                "InputAreaWidth",
                typeof(int),
                typeof(InputLine),
                new PropertyMetadata(300)
            );

        public static DependencyProperty InputNameProperty = DependencyProperty.Register
            (
                "InputName",
                typeof(string),
                typeof(InputLine),
                new PropertyMetadata("测试")
            );

        public static DependencyProperty InputTextProperty = DependencyProperty.Register
            (
                "InputText",
                typeof(string),
                typeof(InputLine),
                new PropertyMetadata("测试")
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

        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }

    }
}

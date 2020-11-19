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
    /// InputDropDown.xaml 的交互逻辑
    /// </summary>
    public partial class InputDropDown : UserControl
    {
        public InputDropDown()
        {
            InitializeComponent();
        }

        public static DependencyProperty InputAreaWidthProperty = DependencyProperty.Register
            (
                "InputAreaWidth",
                typeof(int),
                typeof(InputDropDown),
                new PropertyMetadata(300)
            );

        public static DependencyProperty InputNameProperty = DependencyProperty.Register
            (
                "InputName",
                typeof(string),
                typeof(InputDropDown),
                new PropertyMetadata("测试")
            );

        public static DependencyProperty InputListProperty = DependencyProperty.Register
            (
                "InputList",
                typeof(List<string>),
                typeof(InputDropDown),
                new PropertyMetadata(new List<string>(), new PropertyChangedCallback((sender, e) =>
                 {
                     (sender as InputDropDown).InputList.Add("new");
                 }))
            );

        public static DependencyProperty InputTextProperty = DependencyProperty.Register
            (
                "InputText",
                typeof(string),
                typeof(InputDropDown),
                new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                 {
                     (sender as InputDropDown).EffectiveValue = e.NewValue as string;
                 }))
            );

        public static DependencyProperty SelectedItemProperty = DependencyProperty.Register
            (
                "SelectedItem",
                typeof(string),
                typeof(InputDropDown),
                new PropertyMetadata("", new PropertyChangedCallback((sender, e) =>
                {
                    if (e.NewValue as string == "new")
                    {
                        (sender as InputDropDown).ChangeFlip();
                        (sender as InputDropDown).EffectiveValue = "";
                    }
                    else
                    {
                        (sender as InputDropDown).EffectiveValue = e.NewValue as string;
                    }
                }))
            );

        public static DependencyProperty EffectiveValueProperty = DependencyProperty.Register
            (
                "EffectiveValue",
                typeof(string),
                typeof(InputDropDown),
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

        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }

        public string SelectedItem
        {
            get { return (string)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public string EffectiveValue
        {
            get { return (string)GetValue(EffectiveValueProperty); }
            set { SetValue(EffectiveValueProperty, value); }
        }

        void ChangeFlip()
        {
            Front.Visibility = Visibility.Collapsed;
            Back.Visibility = Visibility.Visible;
            Back.Focus();
        }

    }
}

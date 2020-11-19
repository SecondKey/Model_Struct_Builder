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
    /// DialogueWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogueWindow : Window
    {
        public DialogueWindow()
        {
            InitializeComponent();

            DataContextChanged += (sender, e) => { LoadContent(); };
        }

        void LoadContent()
        {
            DialogueWindowViewModel VM = DataContext as DialogueWindowViewModel;
            foreach (FormStruct formItem in VM.FormStructs)
            {
                switch (formItem.type)
                {
                    case FormItemType.InputLine:
                        InputLine tmpInputLine = new InputLine();
                        tmpInputLine.Margin = new Thickness(0, 0, 0, 20);
                        tmpInputLine.InputAreaWidth = 400;
                        tmpInputLine.InputName = formItem.name;
                        VM.CallBackValues.Add("Value", "");
                        BindingOperations.SetBinding(tmpInputLine, InputLine.InputTextProperty, new Binding()
                        {
                            Path = new PropertyPath("CallBackValues[Value]"),
                            Mode = BindingMode.TwoWay
                        });
                        MainPanel.Children.Add(tmpInputLine);
                        break;
                    case FormItemType.InputText:
                        break;
                    case FormItemType.DropDown:
                        DropDown tmpDropDown = new DropDown();
                        tmpDropDown.Margin = new Thickness(0, 0, 0, 20);
                        tmpDropDown.InputAreaWidth = 400;
                        tmpDropDown.InputName = formItem.name;
                        tmpDropDown.InputList = formItem.parameters as List<string>;
                        VM.CallBackValues.Add("Value", tmpDropDown.InputList[0]);
                        BindingOperations.SetBinding(tmpDropDown, DropDown.SelectedItemProperty, new Binding()
                        {
                            Path = new PropertyPath("CallBackValues[Value]"),
                            Mode = BindingMode.TwoWay
                        });
                        MainPanel.Children.Add(tmpDropDown);
                        break;
                    case FormItemType.InputDropDown:
                        InputDropDown tmpInputDropDown = new InputDropDown();
                        tmpInputDropDown.Margin = new Thickness(0, 0, 0, 20);
                        tmpInputDropDown.InputAreaWidth = 400;
                        tmpInputDropDown.InputName = formItem.name;
                        tmpInputDropDown.InputList = formItem.parameters as List<string>;
                        VM.CallBackValues.Add("Value", "");
                        BindingOperations.SetBinding(tmpInputDropDown, InputDropDown.EffectiveValueProperty, new Binding()
                        {
                            Path = new PropertyPath("CallBackValues[Value]"),
                            Mode = BindingMode.TwoWay
                        });
                        MainPanel.Children.Add(tmpInputDropDown);
                        break;
                }
            }

            Button button = new Button();
            BindingOperations.SetBinding(button, Button.CommandProperty, new Binding()
            {
                Path = new PropertyPath("CallbackCommand"),
            });
            button.Content = "确定";
            button.Click += (sender, e) => { this.Close(); };
            MainPanel.Children.Add(button);
        }
    }
}

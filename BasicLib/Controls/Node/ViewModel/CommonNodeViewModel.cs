using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BasicLib
{
    public enum CommonNodeType
    {
        Basic,
        Custom
    }
    class CommonNodeViewModel : DiagramNodeViewModelBase
    {
        public CommonNodeViewModel(string parameter) : base(parameter)
        {
            nodeType = (CommonNodeType)Enum.Parse(typeof(CommonNodeType), Frame.MainFrameData.GetContent("Node", parameter, "NodeType"));
            switch (nodeType)
            {
                case CommonNodeType.Basic:
                    GetNode = GetBasicNode;
                    break;
                case CommonNodeType.Custom:
                    GetNode = GetCustomNode;
                    break;
            }
        }

        CommonNodeType nodeType;

        /// <summary>
        /// 节点的文本
        /// </summary>
        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public void GetPort()
        {

        }
        #region GetNode
        public override FrameworkElement GetCommonNode()
        {
            CommonNode node = new CommonNode();
            return node;
        }

        public FrameworkElement GetBasicNode()
        {
            var textBlock = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            textBlock.SetBinding(TextBlock.TextProperty, new Binding()
            {
                Path = new PropertyPath("NodeName")
            });

            var ui = new Border();
            ui.Tag = CommonNodeType.Basic;
            ui.BorderBrush = Brushes.Black;
            ui.BorderThickness = new Thickness(1);
            ui.Background = Brushes.Lime; ;
            ui.Child = textBlock;
            return ui;
        }

        public FrameworkElement GetCustomNode()
        {
            CommonNode node = new CommonNode();

            var textBlock = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            textBlock.SetBinding(TextBlock.TextProperty, new Binding()
            {
                Path = new PropertyPath("NodeName")
            });

            var ui = new Path();
            ui.Stroke = Brushes.Black;
            ui.StrokeThickness = 1;
            ui.Fill = Brushes.Pink;
            var converter = new GeometryConverter();
            ui.Data = (Geometry)converter.ConvertFrom(NodeStyle["Geometry"]);
            ui.Stretch = Stretch.Uniform;

            var grid = new Grid();
            grid.Tag = CommonNodeType.Custom;
            grid.Children.Add(ui);
            grid.Children.Add(textBlock);
            return grid;
        }
        #endregion
    }
}

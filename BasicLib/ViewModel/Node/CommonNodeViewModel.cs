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
    public enum CommonNodeRenderType
    {
        Basic,
        Custom
    }
    class CommonNodeViewModel : NodeViewModelBase
    {
        CommonNodeRenderType renderType;

        public CommonNodeViewModel(string nodeType) : base(nodeType)
        {
            renderType = (CommonNodeRenderType)Enum.Parse(typeof(CommonNodeRenderType), Frame.MainFrameData.GetContent("Node", nodeType, "RenderType"));
            BindingGetNodeFun();
        }

        public CommonNodeViewModel(NodeModelBase model) : base(model)
        {
            renderType = (CommonNodeRenderType)Enum.Parse(typeof(CommonNodeRenderType), Frame.MainFrameData.GetContent("Node", model.nodeType, "RenderType"));
            BindingGetNodeFun();
        }

        void BindingGetNodeFun()
        {
            switch (renderType)
            {
                case CommonNodeRenderType.Basic:
                    GetNode = GetBasicNode;
                    break;
                case CommonNodeRenderType.Custom:
                    GetNode = GetCustomNode;
                    break;
            }
        }

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

        #region GetNode
        public override DiagramItem GetCompleteNode()
        {
            FrameworkElement element = GetNode();
            CommonNode node = new CommonNode();
            node.Content = element;
            node.DataContext = this;
            view = node;
            CreateFeature();
            return node;
        }

        protected override FrameworkElement GetCommonNode()
        {
            return null;
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
            ui.BorderBrush = Brushes.Black;
            ui.BorderThickness = new Thickness(0.5);
            ui.Background = Brushes.Lime;
            ui.Child = textBlock;
            return ui;
        }

        public FrameworkElement GetCustomNode()
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

            var ui = new Path();
            ui.Stroke = Brushes.Black;
            ui.StrokeThickness = 0.5;
            ui.Fill = Brushes.Pink;
            var converter = new GeometryConverter();
            ui.Data = (Geometry)converter.ConvertFrom(NodeStyle["Geometry"]);
            ui.Stretch = Stretch.Uniform;

            var grid = new Grid();
            grid.Children.Add(ui);
            grid.Children.Add(textBlock);
            return grid;
        }
        #endregion

        #region Wait
        //public CommonNodeViewModel(string type, NodeModelBase model) : base(type)
        //{
        //    //model = NodeModelBase.GetNodeModel();
        //    viewType = (CommonNodeViewType)Enum.Parse(typeof(CommonNodeViewType), Frame.MainFrameData.GetContent("Node", type, "ViewType"));
        //    BindingGetNodeFun();
        //}
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BasicLib
{
    class DiagramControllerBase : INotifyPropertyChanged
    {
        #region Update
        private UpdateScope _updateScope;

        private class UpdateScope : IDisposable
        {
            private DiagramControllerBase _parent;
            public bool IsInProgress { get; set; }

            public UpdateScope(DiagramControllerBase parent)
            {
                _parent = parent;
            }

            public void Dispose()
            {
                IsInProgress = false;
                _parent.UpdateView();
            }
        }
        /// <summary>
        /// 开始更新
        /// </summary>
        /// <returns></returns>
        private IDisposable BeginUpdate()
        {
            _updateScope.IsInProgress = true;
            return _updateScope;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 表中所有形状
        /// </summary>
        private List<ShapeBase> Shapes { get; set; }
        /// <summary>
        /// 指定视图
        /// </summary>
        private DiagramView View { get; set; }

        public DiagramControllerBase(DiagramView view)
        {
            View = view;
            Shapes = CreateModel();
            UpdateView();
            BindEvents();
        }

        /// <summary>
        /// 创建模型
        /// </summary>
        /// <returns></returns>
        private List<ShapeBase> CreateModel()
        {
            /*for(int x = 0; x<30; x++)
				for (int y = 0; y < 30; y++)
				{
					var s = new RectangleShape();
					s.Location = new Point(x * 20, y * 20);
					s.Size = new Size(15, 15);
				}*/

            var list = new List<ShapeBase>();

            var s = new RectangleShape();
            s.Location = new Point(10, 10);
            s.Size = new Size(50, 50);
            list.Add(s);

            var s2 = new RectangleShape();
            s2.Location = new Point(200, 10);
            s2.Size = new Size(50, 50);
            list.Add(s2);

            var s3 = new EllipseShape();
            s3.Location = new Point(90, 100);
            s3.Size = new Size(80, 50);
            list.Add(s3);

            //var s4 = new EllipseShape();
            //s4.Location = new Point(90, 180);
            //s4.Size = new Size(80, 50);
            //list.Add(s4);

            s.Links.Add(s3);
            s3.Links.Add(s2);
            s2.Links.Add(s);

            return list;
        }

        /// <summary>
        /// 更新视图
        /// </summary>
        private void UpdateView()
        {
            View.Children.Clear();
            foreach (var s in Shapes)
                UpdateUIElement(s);
            foreach (var s in Shapes)
                CreateLinks(s, (NodeBase)View.FindItem(s));
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvents()
        {
            foreach (var s in Shapes)
                s.PropertyChanged += ShapePropertyChanged;
        }

        /// <summary>
        /// 图形属性发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShapePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!_updateScope.IsInProgress)
            {
                var shape = sender as ShapeBase;
                UpdateUIElement(shape);
            }
        }
        /// <summary>
        /// 更新UI元素
        /// </summary>
        /// <param name="shape"></param>
        private void UpdateUIElement(ShapeBase shape)
        {
            UpdateUIElement(shape, (NodeBase)View.FindItem(shape));
        }
        /// <summary>
        /// 更新UI元素
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="item"></param>
        private void UpdateUIElement(ShapeBase shape, NodeBase item)
        {
            //if (item == null)
            //{
            //    item = new Node();
            //    if (shape is RectangleShape)
            //    {
            //        item.Ports.Add(new RectPort() { Visibility = Visibility.Hidden });
            //        var ui = new Border();
            //        ui.CornerRadius = new CornerRadius(15);
            //        ui.BorderBrush = new SolidColorBrush(Colors.Black);
            //        ui.BorderThickness = new Thickness(1);
            //        ui.Background = new SolidColorBrush(Colors.Yellow);
            //        item.Content = ui;
            //    }
            //    else
            //    {
            //        item.Ports.Add(new EllipsePort() { Visibility = Visibility.Hidden });
            //        var ui = new Ellipse();
            //        ui.Fill = Brushes.Green;
            //        ui.Stroke = Brushes.Black;
            //        ui.StrokeThickness = 1;
            //        item.Content = ui;
            //    }
            //    item.ModelElement = shape;

            //    View.Children.Add(item);

            //item.DataContext = shape;
            //item.SetBinding(Canvas.LeftProperty, "Location.X");
            //item.SetBinding(Canvas.TopProperty, "Location.Y");
            //item.SetBinding(FrameworkElement.WidthProperty, "Size.Width");
            //item.SetBinding(FrameworkElement.HeightProperty, "Size.Height");
            //}
            item.Width = shape.Size.Width;
            item.Height = shape.Size.Height;
            item.SetValue(Canvas.LeftProperty, shape.Location.X);
            item.SetValue(Canvas.TopProperty, shape.Location.Y);
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="item"></param>
        private void CreateLinks(ShapeBase shape, NodeBase item)
        {
            //foreach (var dest in shape.Links)
            //{
            //    var destItem = (Node)View.FindItem(dest);
            //    if (destItem != null)
            //    {
            //        var link = new SegmentLink();
            //        link.EndCap = true;
            //        link.Source = item.Ports.First();
            //        link.Target = destItem.Ports.First();
            //        View.Children.Add(link);
            //    }
            //}
        }

        #region IDiagramController Members 控制器接口成员函数

        /// <summary>
        /// 更新元素绑定
        /// </summary>
        /// <param name="items"></param>
        /// <param name="bounds"></param>
        public void UpdateItemsBounds(DiagramItem[] items, Rect[] bounds)
        {
            using (BeginUpdate())
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i] as NodeBase;
                    if (item != null)
                    {
                        var shape = item.ModelElement as ShapeBase;
                        shape.Location = bounds[i].Location;
                        shape.Size = bounds[i].Size;
                        UpdateUIElement(shape, item);
                    }
                }
            }
        }
        /// <summary>
        /// 更新连接
        /// </summary>
        /// <param name="initialState"></param>
        /// <param name="link"></param>
        public void UpdateLink(LinkInfo initialState, ILink link)
        {
        }
        /// <summary>
        /// 是否可以执行方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecuteCommand(System.Windows.Input.ICommand command, object parameter)
        {
            return (command == ApplicationCommands.Delete && View.Selection.Count > 0);
        }
        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        public void ExecuteCommand(System.Windows.Input.ICommand command, object parameter)
        {
            if (command == ApplicationCommands.Delete)
            {
                foreach (var e in View.Selection.Select(p => p.ModelElement))
                {
                    if (e is ShapeBase)
                        Shapes.Remove(e as ShapeBase);
                }
                UpdateView();
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BasicLib
{
    /// <summary>
    /// 选择框装饰器，鼠标左键框选元素时创建
    /// </summary>
    class RubberbandAdorner : Adorner
    {
        /// <summary>
        /// 图表视图
        /// </summary>
        public UIElement View { get; private set; }
        /// <summary>
        /// 绘制选择框笔刷
        /// </summary>
        private Pen _pen;

        /// <summary>
        /// 开始的点
        /// </summary>
        protected Point Start { get; set; }
        /// <summary>
        /// 结束的点
        /// </summary>
        protected Point End { get; set; }


        /// <summary>
        /// 选择框装饰器，鼠标左键框选元素时创建
        /// </summary>
        public RubberbandAdorner(UIElement view, Point start) : base(view)
        {
            View = view;
            End = Start = start;
            _pen = new Pen(Brushes.Black, 0.4);
            this.Loaded += OnLoaded;
        }

        /// <summary>
        /// 加载时尝试捕获鼠标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CaptureMouse();
        }

        /// <summary>
        /// 当鼠标移动时
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            End = e.GetPosition(View);
            InvalidateVisual();
        }

        /// <summary>
        /// 当鼠标抬起时
        /// 如果当前对象在捕捉鼠标，判断是否可以接受拖放并释放对鼠标的捕捉
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                InvalidateVisual();
                this.ReleaseMouseCapture();
            }
        }

        /// <summary>
        /// 当失去鼠标捕捉时清空对象装饰器
        /// 重置鼠标
        /// 结束拖放操作
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            AdornerLayer.GetAdornerLayer(View).Remove(this);
            Mouse.OverrideCursor = null;
            EndDrag();
        }

        /// <summary>
        /// 结束拖拽
        /// </summary>
        void EndDrag()
        {
            //var rect = new Rect(Start, End);
            //var items = View.Items.Where(p => p.CanSelect && rect.Contains(p.Bounds));
            //View.Selection.SetRange(items);
        }
        /// <summary>
        /// 实时渲染选框
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawRectangle(Brushes.Transparent, _pen, new Rect(Start, End));
        }
    }
}

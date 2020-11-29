using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BasicLib
{
    class ItemsControlDragHelper : IDisposable
    {
        /// <summary>
        /// 鼠标按下点（可空）
        /// </summary>
        private Point? _mouseDown;
        /// <summary>
        /// 鼠标偏移量
        /// </summary>
        private Point _offset;
        /// <summary>
        /// 指定拖拽的节点
        /// </summary>
        private ItemsControl _source;
        /// <summary>
        /// 可接受拖拽范围
        /// </summary>
        private UIElement _dragScope;
        /// <summary>
        /// 拖放装饰器
        /// </summary>
        private DragDropAdorner _adorner;
        private DragEventHandler _dragOver, _dragEnter, _dragLeave;

        public ItemsControlDragHelper(ItemsControl source, UIElement dragScope)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            _dragEnter = new DragEventHandler(DragScope_DragEnter);
            _dragOver = new DragEventHandler(DragScope_DragOver);
            _dragLeave = new DragEventHandler(DragScope_DragLeave);

            _source = source;
            _dragScope = dragScope;
            _source.PreviewMouseLeftButtonDown += SourceMouseLeftButtonDown;
            _source.PreviewMouseMove += SourceMouseMove;
        }

        /// <summary>
        /// 当鼠标在节点上按下左键时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var point = e.GetPosition(_source);//获取当前鼠标位置
            if (!IsMouseOverScrollbar(point))//如果当前鼠标不在滚动条上，记录鼠标按下的点
            {
                _mouseDown = point;
            }
        }
        /// <summary>
        /// 当鼠标在节点上移动时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)//鼠标必须按下按钮才有效
            {
                if (_mouseDown.HasValue && HasMoved(e.GetPosition(_source)))//当鼠标按下的点有效，且进行了有效移动
                {
                    var adornerSource = e.OriginalSource as UIElement;//找到选中的按钮                    
                    adornerSource = _source.ContainerFromElement(adornerSource) as UIElement;//查找选中按钮的ItemsControl（最外层元素，防止）
                    var input = adornerSource as IInputElement;
                    if (input != null)//判断input是否为空，不为空则设置偏移量，如果为空则没有偏移量
                        _offset = e.GetPosition(input);
                    else
                        _offset = new Point(0, 0);

                    object subj = e.OriginalSource;//直接获取到拖拽的节点
                    var fe = adornerSource as FrameworkElement;//如果该节点有tag，则使用tag执行DoDragDrop方法。如果没有，用节点执行DoDragDrop
                    if (fe != null && fe.Tag != null)
                        subj = fe.Tag;
                    DoDragDrop(subj, adornerSource);//对拖拽操作执行事件
                    e.Handled = true;//将事件标记位已处理
                }
            }
            else
            {
                _mouseDown = null;
            }
        }
        /// <summary>
        /// 目标是否移动了
        /// 允许鼠标点击节点后有限的运动鼠标但不移动节点
        /// 范围是节点大小的一半
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool HasMoved(Point point)
        {
            return Math.Abs(point.X - _mouseDown.Value.X) > SystemParameters.MinimumHorizontalDragDistance / 2
                || Math.Abs(point.Y - _mouseDown.Value.Y) > SystemParameters.MinimumVerticalDragDistance / 2;
        }

        /// <summary>
        /// 递归命中测试，判断当前鼠标是否在滚动条上
        /// </summary>
        /// <param name="ptMouse"></param>
        /// <returns></returns>
        private bool IsMouseOverScrollbar(Point ptMouse)
        {
            HitTestResult res = VisualTreeHelper.HitTest(_source, ptMouse);
            if (res == null)
                return false;

            DependencyObject depObj = res.VisualHit;
            while (depObj != null)
            {
                if (depObj is System.Windows.Controls.Primitives.ScrollBar)
                    return true;

                // VisualTreeHelper works with objects of type Visual or Visual3D.
                // If the current object is not derived from Visual or Visual3D,
                // then use the LogicalTreeHelper to find the parent element.
                if (depObj is Visual || depObj is System.Windows.Media.Media3D.Visual3D)
                    depObj = VisualTreeHelper.GetParent(depObj);
                else
                    depObj = LogicalTreeHelper.GetParent(depObj);
            }

            return false;
        }

        /// <summary>
        /// 执行拖拽操作，在进行了有效的拖拽操作时执行
        /// </summary>
        /// <param name="dragItem"></param>
        /// <param name="adornerSource"></param>
        private void DoDragDrop(object dragItem, UIElement adornerSource)
        {
            if (adornerSource != null)//如果选中按钮不为空
            {
                var rect = VisualTreeHelper.GetDescendantBounds(adornerSource);//获取拖拽节点的外边框
                var size = new Size((double)Math.Ceiling(rect.Width), (double)Math.Ceiling(rect.Height));//获取比节点大的整数值边框
                var brush = new VisualBrush(adornerSource);//初始化包含节点的VisualBrush
                _adorner = new DragDropAdorner(_dragScope, size, brush);//新建拖拽装饰器
                _adorner.Opacity = 0.7;
                _adorner.Visibility = Visibility.Hidden;
            }

            DragDrop.AddPreviewDragEnterHandler(_dragScope, _dragEnter);
            DragDrop.AddPreviewDragOverHandler(_dragScope, _dragOver);
            DragDrop.AddPreviewDragLeaveHandler(_dragScope, _dragLeave);

            DragDrop.DoDragDrop(_source, dragItem, DragDropEffects.All);//执行拖拽方法，该方法会在拖拽结束后结束并返回
            DragFinished();//执行拖拽结束方法

            DragDrop.RemovePreviewDragEnterHandler(_dragScope, _dragEnter);
            DragDrop.RemovePreviewDragOverHandler(_dragScope, _dragOver);
            DragDrop.RemovePreviewDragLeaveHandler(_dragScope, _dragLeave);
        }

        /// <summary>
        /// 当拖拽进入可接受位置时，为节点添加拖拽装饰器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DragScope_DragEnter(object sender, DragEventArgs args)
        {
            if (_adorner != null)
                AdornerLayer.GetAdornerLayer(_source).Add(_adorner);
        }
        /// <summary>
        /// 当在可接受位置上拖动时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DragScope_DragOver(object sender, DragEventArgs args)
        {
            if (_adorner != null)
            {
                var pos = args.GetPosition(_dragScope);
                _adorner.SetOffsets(pos.X - _offset.X, pos.Y - _offset.Y);
                _adorner.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// 当离开可接受位置时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DragScope_DragLeave(object sender, DragEventArgs args)
        {
            if (_adorner != null)
                AdornerLayer.GetAdornerLayer(_source).Remove(_adorner);
        }

        /// <summary>
        /// 拖拽结束
        /// </summary>
        protected void DragFinished()
        {
            if (_adorner != null)
                AdornerLayer.GetAdornerLayer(_source).Remove(_adorner);
            _adorner = null;
        }

        #region IDisposable Members

        public void Dispose()
        {
            _source.MouseLeftButtonDown -= SourceMouseLeftButtonDown;
            _source.MouseMove -= SourceMouseMove;
        }

        #endregion
    }
}

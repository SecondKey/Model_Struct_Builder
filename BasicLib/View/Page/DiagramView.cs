using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace BasicLib
{
    /// <summary>
    /// 图的View
    /// </summary>
    public class DiagramView : Canvas
    {


        static DiagramView()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DiagramView), new FrameworkPropertyMetadata(typeof(DiagramView)));
        }

        #region DependencyProperty

        #region GridCellSize单元格

        /// <summary>
        /// 单元格大小
        /// </summary>
        public static readonly DependencyProperty GridCellSizeProperty =
            DependencyProperty.Register("GridCellSize",
                                       typeof(Size),
                                       typeof(DiagramView),
                                       new FrameworkPropertyMetadata(new Size(10, 10)));
        /// <summary>
        /// 单元格大小
        /// </summary>
        public Size GridCellSize
        {
            get { return (Size)GetValue(GridCellSizeProperty); }
            set { SetValue(GridCellSizeProperty, value); }
        }

        #endregion

        #region ShowCell是否显示网格
        /// <summary>
        /// 是否显示网格
        /// </summary>
        public static readonly DependencyProperty ShowGridProperty =
            DependencyProperty.Register("ShowGrid",
                                       typeof(bool),
                                       typeof(DiagramView),
                                       new FrameworkPropertyMetadata(false));
        /// <summary>
        /// 是否显示网格
        /// </summary>
        public bool ShowGrid
        {
            get { return (bool)GetValue(ShowGridProperty); }
            set { SetValue(ShowGridProperty, value); }
        }
        #endregion

        #region DocumentSize整张画布大小
        /// <summary>
        /// 整张画布大小
        /// </summary>
        public static readonly DependencyProperty DocumentSizeProperty =
            DependencyProperty.Register("DocumentSize",
                                       typeof(Size),
                                       typeof(DiagramView),
                                       new FrameworkPropertyMetadata(new Size(2000, 2000)));
        /// <summary>
        /// 整张画布大小
        /// </summary>
        public Size DocumentSize
        {
            get { return (Size)GetValue(DocumentSizeProperty); }
            set { SetValue(DocumentSizeProperty, value); }
        }
        #endregion

        #region Zoom画布缩放
        /// <summary>
        /// 画布缩放
        /// </summary>
        public static readonly DependencyProperty ZoomProperty =
            DependencyProperty.Register("Zoom",
                                       typeof(double),
                                       typeof(DiagramView),
                                       new FrameworkPropertyMetadata(1.0, new PropertyChangedCallback(OnZoomChanged)));

        /// <summary>
        /// 画布缩放
        /// </summary>
        public double Zoom
        {
            get { return (double)GetValue(ZoomProperty); }
            set { SetValue(ZoomProperty, value); }
        }

        /// <summary>
        /// 画布缩放时执行操作
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnZoomChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = d as DiagramView;
            var zoom = (double)e.NewValue;
            view._gridPen = view.CreateGridPen();
            if (Math.Abs(zoom - 1) < 0.0001)
                view.LayoutTransform = null;
            else
                view.LayoutTransform = new ScaleTransform(zoom, zoom);
        }
        #endregion

        #endregion

        #region DiagramElement
        /// <summary>
        /// 控制器
        /// </summary>
        public iDiagramController Controller { get; set; }

        /// <summary>
        /// 所有的流程图元素
        /// </summary>
        public IEnumerable<DiagramItem> Items
        {
            get { return Children.OfType<DiagramItem>(); }
        }

        /// <summary>
        /// 拖动装饰器
        /// </summary>
        private Adorner _dragAdorner;
        /// <summary>
        /// 拖动装饰器
        /// </summary>
        public Adorner DragAdorner
        {
            get { return _dragAdorner; }
            set
            {
                if (_dragAdorner != value)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(this);
                    if (_dragAdorner != null)
                        adornerLayer.Remove(_dragAdorner);
                    _dragAdorner = value;
                    if (_dragAdorner != null)
                        adornerLayer.Add(_dragAdorner);
                }
            }
        }
        /// <summary>
        /// 是否正在拖动
        /// </summary>
        public bool IsDragging { get { return DragAdorner != null; } }

        #endregion

        public DiagramView()
        {
            _gridPen = CreateGridPen();
            BindCommands();
            Focusable = true;

            this.LayoutUpdated += new EventHandler(DiagramView_LayoutUpdated);
        }

        /// <summary>
        /// 绑定命令
        /// </summary>
        private void BindCommands()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, ExecuteCommand, CanExecuteCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, ExecuteCommand, CanExecuteCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ExecuteCommand, CanExecuteCommand));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, ExecuteCommand, CanExecuteCommand));
        }


        #region 笔刷
        /// <summary>
        /// 用于绘制网格的笔刷
        /// </summary>
        private Pen _gridPen;
        /// <summary>
        /// 动态生成绘制笔刷（子类重写）
        /// </summary>
        /// <returns></returns>
        protected virtual Pen CreateGridPen()
        {
            return new Pen(Brushes.LightGray, (1 / Zoom));
        }
        #endregion

        #region 重写父类方法

        /// <summary>
        /// 测量排列子元素需要的大小
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            base.MeasureOverride(DocumentSize);
            return DocumentSize;
        }
        /// <summary>
        /// 当开始渲染时
        /// </summary>
        /// <param name="dc"></param>
        protected override void OnRender(DrawingContext dc)
        {
            var rect = new Rect(0, 0, RenderSize.Width, RenderSize.Height);
            dc.DrawRectangle(Background, null, rect);
            if (ShowGrid && GridCellSize.Width > 0 && GridCellSize.Height > 0)
                DrawGrid(dc, rect);
        }

        #endregion

        /// <summary>
        /// 更新布局（通知所有元素更新布局）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DiagramView_LayoutUpdated(object sender, EventArgs e)
        {
            foreach (var n in this.Children.OfType<Node>())
                n.UpdatePosition();
        }
        /// <summary>
        /// 查找所有元素中第一个与传入匹配的元素
        /// </summary>
        /// <param name="modelElement"></param>
        /// <returns></returns>
        public DiagramItem FindItem(object modelElement)
        {
            return Items.FirstOrDefault(p => p.ModelElement == modelElement);
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="rect"></param>
        protected virtual void DrawGrid(DrawingContext dc, Rect rect)
        {
            //using .5 forces wpf to draw a single pixel line
            for (var i = 0.5; i < rect.Height; i += GridCellSize.Height)
                dc.DrawLine(_gridPen, new Point(0, i), new Point(rect.Width, i));
            for (var i = 0.5; i < rect.Width; i += GridCellSize.Width)
                dc.DrawLine(_gridPen, new Point(i, 0), new Point(i, rect.Height));
        }



        /// <summary>
        /// 执行方法
        /// 如果有控制器，则控制器执行操作，
        /// 如果操作时删除，清空选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (Controller != null)
                Controller.ExecuteCommand(e.Command, e.Parameter);
            if (e.Command == ApplicationCommands.Delete)
                Selection.Clear();
        }

        /// <summary>
        /// 是否可以执行方法，使用指定控制器的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanExecuteCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Controller != null)
                e.CanExecute = Controller.CanExecuteCommand(e.Command, e.Parameter);
        }


        #region old

        /// <summary>
        /// 输入工具
        /// </summary>
        public iInputTool InputTool { get; set; }

        /// <summary>
        /// 移动调整工具
        /// </summary>
        public iMoveResizeTool DragTool { get; set; }

        /// <summary>
        /// 连接工具
        /// </summary>
        public iLinkTool LinkTool { get; set; }

        /// <summary>
        /// 拖放工具
        /// </summary>
        public iDragDropTool DragDropTool { get; set; }


        /// <summary>
        /// 选择器
        /// </summary>
        public Selection Selection { get; private set; }



        //_gridPen = CreateGridPen();
        //Selection = new Selection();
        //InputTool = new InputTool(this);
        //DragTool = new MoveResizeTool(this);
        //LinkTool = new LinkTool(this);



        ///// <summary>
        ///// 鼠标按下
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnMouseDown(MouseButtonEventArgs e)
        //{
        //    InputTool.OnMouseDown(e);
        //    base.OnMouseDown(e);
        //    Focus();
        //}
        ///// <summary>
        ///// 当鼠标移动时
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    InputTool.OnMouseMove(e);
        //    base.OnMouseMove(e);
        //}
        ///// <summary>
        ///// 当鼠标抬起时
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnMouseUp(MouseButtonEventArgs e)
        //{
        //    InputTool.OnMouseUp(e);
        //    base.OnMouseUp(e);
        //}
        ///// <summary>
        ///// 当鼠标按下时（Preview）
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    InputTool.OnPreviewKeyDown(e);
        //    base.OnPreviewKeyDown(e);
        //}
        ///// <summary>
        ///// 当开始拖动时
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnDragEnter(DragEventArgs e)
        //{
        //    if (DragDropTool != null)
        //        DragDropTool.OnDragEnter(e);
        //    base.OnDragEnter(e);
        //}
        ///// <summary>
        ///// 当拖动离开时时
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnDragLeave(DragEventArgs e)
        //{
        //    if (DragDropTool != null)
        //        DragDropTool.OnDragLeave(e);
        //    base.OnDragLeave(e);
        //}
        ///// <summary>
        ///// 当拖动结束
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnDragOver(DragEventArgs e)
        //{
        //    if (DragDropTool != null)
        //        DragDropTool.OnDragOver(e);
        //    base.OnDragOver(e);
        //}
        ///// <summary>
        ///// 拖放时
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnDrop(DragEventArgs e)
        //{
        //    if (DragDropTool != null)
        //        DragDropTool.OnDrop(e);
        //    base.OnDrop(e);
        //}

        #endregion
    }
}

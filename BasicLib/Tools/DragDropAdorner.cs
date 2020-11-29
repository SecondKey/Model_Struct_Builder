using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BasicLib
{
    public class DragDropAdorner : Adorner
    {

        #region Data

        /// <summary>
        /// 矩形子对象
        /// </summary>
        private Rectangle child = null;
        /// <summary>
        /// 左侧偏移量
        /// </summary>
        private double offsetLeft = 0;
        /// <summary>
        /// 顶部偏移量
        /// </summary>
        private double offsetTop = 0;

        #endregion // Data

        #region Constructor

        /// <summary>
        /// Initializes a new instance of DragVisualAdorner.
        /// 构造函数，创建一个矩形作为子对象并设置参数
        /// </summary>
        /// <param name="adornedElement">被装饰的元素。</param>
        /// <param name="size">装饰器的大小。</param>
        /// <param name="brush">装饰器的画刷。</param>
        public DragDropAdorner(UIElement adornedElement, Size size, Brush brush)
            : base(adornedElement)
        {
            Rectangle rect = new Rectangle();
            rect.Fill = brush;
            rect.Width = size.Width;
            rect.Height = size.Height;
            rect.IsHitTestVisible = false;
            this.child = rect;
        }

        #endregion // Constructor

        #region Public Interface

        #region GetDesiredTransform

        /// <summary>
        /// Override.
        /// 基于当前装饰的对象，返回装饰器的位置
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
        {
            GeneralTransformGroup result = new GeneralTransformGroup();
            result.Children.Add(base.GetDesiredTransform(transform));
            result.Children.Add(new TranslateTransform(this.offsetLeft, this.offsetTop));
            return result;
        }

        #endregion // GetDesiredTransform

        #region OffsetLeft

        /// <summary>
        /// Gets/sets the horizontal offset of the adorner.
        /// 获取或设置横向偏移装饰器
        /// </summary>
        public double OffsetLeft
        {
            get { return this.offsetLeft; }
            set
            {
                this.offsetLeft = value;
                UpdateLocation();
            }
        }

        #endregion // OffsetLeft

        #region SetOffsets

        /// <summary>
        /// Updates the location of the adorner in one atomic operation.
        /// 更新装饰器的位置
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        public void SetOffsets(double left, double top)
        {
            this.offsetLeft = left;
            this.offsetTop = top;
            this.UpdateLocation();
        }

        #endregion // SetOffsets

        #region OffsetTop

        /// <summary>
        /// Gets/sets the vertical offset of the adorner.
        /// 获取或设置顶部偏移装饰器
        /// </summary>
        public double OffsetTop
        {
            get { return this.offsetTop; }
            set
            {
                this.offsetTop = value;
                UpdateLocation();
            }
        }

        #endregion // OffsetTop

        #endregion // Public Interface

        #region Protected Overrides

        /// <summary>
        /// Override.
        /// 获取装饰器需要的布局控件
        /// </summary>
        /// <param name="constraint"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size constraint)
        {
            this.child.Measure(constraint);
            return this.child.DesiredSize;
        }

        /// <summary>
        /// Override.
        /// 返回装饰器的实际大小
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            this.child.Arrange(new Rect(finalSize));
            return finalSize;
        }

        /// <summary>
        /// Override.
        /// 从元素子集中获取指定索引的子类
        /// 总是返回child
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return this.child;
        }

        /// <summary>
        /// Override.  Always returns 1.
        /// 返回子集的数量
        /// 总是返回1
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        #endregion // Protected Overrides

        #region Private Helpers
        /// <summary>
        /// 更新位置
        /// </summary>
        private void UpdateLocation()
        {
            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
                adornerLayer.Update(this.AdornedElement);
        }

        #endregion // Private Helpers
    }
}

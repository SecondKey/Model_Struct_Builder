using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BasicLib
{
    /// <summary>
    /// 图表元素
    /// </summary>
    public abstract class DiagramItem : ContentControl
    {
        #region Properties 属性
        /// <summary>
        /// 选择时的装饰器
        /// </summary>
        private Adorner SelectionAdorner { get; set; }
        /// <summary>
        /// 模型元素
        /// </summary>
        public object ModelElement { get; set; }
        /// <summary>
        /// 元素的范围
        /// </summary>
        public abstract Rect Bounds { get; }

        #region IsSelected Property 是否被选中
        /// <summary>
        /// 该节点是否被选中
        /// </summary>
        internal static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register("IsSelected",
                               typeof(bool),
                               typeof(DiagramItem),
                               new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsSelectedChanged)));
        /// <summary>
        /// 该节点是否被选中
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            internal set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 当选中发生变化是时修改自身的装饰器
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                d.ClearValue(isPrimarySelectionProperty);
                (d as DiagramItem).HideSelectionAdorner();
            }
            else
                (d as DiagramItem).ShowSelectionAdorner();
        }

        #endregion

        #region IsPrimarySelection Property 是否被选为主要节点
        /// <summary>
        /// 是否被选为主要节点
        /// </summary>
        internal static readonly DependencyProperty isPrimarySelectionProperty =
            DependencyProperty.Register("IsPrimarySelection",
                                       typeof(bool),
                                       typeof(DiagramItem),
                                       new FrameworkPropertyMetadata(false));
        /// <summary>
        /// 是否被选为主要节点
        /// </summary>
        public bool IsPrimarySelection
        {
            get { return (bool)GetValue(isPrimarySelectionProperty); }
            internal set { SetValue(isPrimarySelectionProperty, value); }
        }
        #endregion

        #region CanMove Property是否可以移动
        /// <summary>
        /// 是否可以移动
        /// </summary>
        public bool CanMove
        {
            get { return (bool)GetValue(CanMoveProperty); }
            set { SetValue(CanMoveProperty, value); }
        }
        /// <summary>
        /// 是否可以移动
        /// </summary>
        public static readonly DependencyProperty CanMoveProperty =
            DependencyProperty.Register("CanMove",
                                       typeof(bool),
                                       typeof(DiagramItem),
                                       new FrameworkPropertyMetadata(true));
        #endregion

        #region CanSelect Property是否可以被选择
        /// <summary>
        /// 是否可以被选择
        /// </summary>
        public bool CanSelect
        {
            get { return (bool)GetValue(CanSelectProperty); }
            set { SetValue(CanSelectProperty, value); }
        }
        /// <summary>
        /// 是否可以被选择
        /// </summary>
        public static readonly DependencyProperty CanSelectProperty =
            DependencyProperty.Register("CanSelect",
                                       typeof(bool),
                                       typeof(DiagramItem),
                                       new FrameworkPropertyMetadata(true));

        #endregion
        #endregion

        #region 显示和隐藏装饰器
        /// <summary>
        /// 隐藏装饰器
        /// </summary>
        protected void HideSelectionAdorner()
        {
            if (SelectionAdorner != null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                    adornerLayer.Remove(SelectionAdorner);
                SelectionAdorner = null;
            }
        }
        /// <summary>
        /// 显示装饰器
        /// </summary>
        protected void ShowSelectionAdorner()
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(this);
            if (adornerLayer != null)
            {
                SelectionAdorner = CreateSelectionAdorner();
                SelectionAdorner.Visibility = Visibility.Visible;
                adornerLayer.Add(SelectionAdorner);
            }
        }

        protected abstract Adorner CreateSelectionAdorner();
        #endregion
    }
}

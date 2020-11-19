using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Aga.Diagrams.Controls
{
    public abstract class PortBase : Control, IPort
    {
        #region Properties
        /// <summary>
        /// 所有端口上的连接
        /// </summary>
        private List<ILink> _links = new List<ILink>();
        /// <summary>
        /// 所有端口上的连接
        /// </summary>
        public ICollection<ILink> Links { get { return _links; } }

        /// <summary>
        /// 所有指入的连接
        /// </summary>
        public IEnumerable<ILink> IncomingLinks
        {
            get { return Links.Where(p => p.Target == this); }
        }
        /// <summary>
        /// 所有指出的连接
        /// </summary>
        public IEnumerable<ILink> OutgoingLinks
        {
            get { return Links.Where(p => p.Source == this); }
        }

        /// <summary>
        /// 端口的中心位置
        /// </summary>
        private Point _center;
        /// <summary>
        /// 端口的中心位置
        /// </summary>
        public Point Center
        {
            get { return _center; }
            protected set
            {
                if (_center != value && !double.IsNaN(value.X) && !double.IsNaN(value.Y))
                {
                    _center = value;
                    foreach (var link in Links)
                        link.UpdatePath();
                }
            }
        }

        #region Sensitivity Property灵敏度，可交互范围
        /// <summary>
        /// 灵敏度，可交互范围
        /// </summary>
        public static readonly DependencyProperty SensitivityProperty =
            DependencyProperty.Register("Sensitivity",
                                       typeof(double),
                                       typeof(PortBase),
                                       new FrameworkPropertyMetadata(5.0));
        /// <summary>
        /// 灵敏度，可交互范围
        /// </summary>
        public double Sensitivity
        {
            get { return (double)GetValue(SensitivityProperty); }
            set { SetValue(SensitivityProperty, value); }
        }
        #endregion

        #region CanAcceptIncomingLinks Property是否可以接受指入
        /// <summary>
        /// 是否可以接受指入
        /// </summary>
        public static readonly DependencyProperty CanAcceptIncomingLinksProperty =
            DependencyProperty.Register("CanAcceptIncomingLinks",
                                       typeof(bool),
                                       typeof(PortBase),
                                       new FrameworkPropertyMetadata(true));
        /// <summary>
        /// 是否可以接受指入
        /// </summary>
        public bool CanAcceptIncomingLinks
        {
            get { return (bool)GetValue(CanAcceptIncomingLinksProperty); }
            set { SetValue(CanAcceptIncomingLinksProperty, value); }
        }


        #endregion

        #region CanAcceptOutgoingLinks Property是否可以接受指出
        /// <summary>
        /// 是否可以接受指出
        /// </summary>
        public static readonly DependencyProperty CanAcceptOutgoingLinksProperty =
            DependencyProperty.Register("CanAcceptOutgoingLinks",
                               typeof(bool),
                               typeof(PortBase),
                               new FrameworkPropertyMetadata(true));
        /// <summary>
        /// 是否可以接受指出
        /// </summary>
        public bool CanAcceptOutgoingLinks
        {
            get { return (bool)GetValue(CanAcceptOutgoingLinksProperty); }
            set { SetValue(CanAcceptOutgoingLinksProperty, value); }
        }



        #endregion

        #region CanCreateLink Property
        /// <summary>
        /// 是否可以创建连接
        /// </summary>
        public static readonly DependencyProperty CanCreateLinkProperty =
            DependencyProperty.Register("CanCreateLink",
                               typeof(bool),
                               typeof(PortBase),
                               new FrameworkPropertyMetadata(false));
        /// <summary>
        /// 是否可以创建连接
        /// </summary>
        public bool CanCreateLink
        {
            get { return (bool)GetValue(CanCreateLinkProperty); }
            set { SetValue(CanCreateLinkProperty, value); }
        }
        #endregion

        #endregion

        protected PortBase()
        {
        }

        /// <summary>
        /// 更新端口位置
        /// </summary>
        public virtual void UpdatePosition()
        {
            var canvas = VisualHelper.FindParent<Canvas>(this);
            if (canvas != null)
                Center = this.TransformToAncestor(canvas).Transform(new Point(this.ActualWidth / 2, this.ActualHeight / 2));
            else
                Center = new Point(Double.NaN, Double.NaN);
        }

        /// <summary>
        /// Calcluate the intersection point of the port bounds and the line between center and target point
        /// 计算 端口边缘 和 端口中心到目标位置连线 的焦点（连接线指入指出端口的位置）
        /// </summary>
        public abstract Point GetEdgePoint(Point target);

        /// <summary>
        /// Returns if the specified point is inside port sensivity area
        /// 返回指定点是否在端口的可交互范围内
        /// </summary>
        public abstract bool IsNear(Point point);

        /// <summary>
        /// 当左键按下时创建连接
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CanCreateLink)
            {
                var view = VisualHelper.FindParent<DiagramView>(this);
                if (view != null)
                {
                    view.LinkTool.BeginDragNewLink(e.GetPosition(view), this);
                    e.Handled = true;
                }
            }
            else
                base.OnMouseLeftButtonDown(e);
        }
    }
}

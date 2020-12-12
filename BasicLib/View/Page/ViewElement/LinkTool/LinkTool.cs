using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;

namespace BasicLib
{
    /// <summary>
    /// 连接工具
    /// </summary>
    public class LinkTool : iLinkTool
    {
        /// <summary>
        /// 图表视图
        /// </summary>
        protected DiagramView View { get; private set; }
        /// <summary>
        /// 图表控制器
        /// </summary>
        protected iDiagramController Controller { get { return null; } }
        /// <summary>
        /// 拖拽开始的位置
        /// </summary>
        protected Point DragStart { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        protected ILink Link { get; set; }
        /// <summary>
        /// 连接端口的类型
        /// </summary>
        protected LinkThumbKind Thumb { get; set; }
        /// <summary>
        /// 初始状态
        /// </summary>
        protected LinkInfo InitialState { get; set; }
        /// <summary>
        /// 连接装饰器
        /// </summary>
        protected LinkAdorner Adorner { get; set; }
        /// <summary>
        /// 是否是一个新的连接
        /// </summary>
        private bool _isNewLink;
        public LinkTool(DiagramView view)
        {
            View = view;
        }
        /// <summary>
        /// 开始拖拽（默认不是新连接）
        /// </summary>
        /// <param name="start"></param>
        /// <param name="link"></param>
        /// <param name="thumb"></param>
        public void BeginDrag(Point start, ILink link, LinkThumbKind thumb)
        {
            BeginDrag(start, link, thumb, false);
        }
        /// <summary>
        /// 开始拖拽（可指定是否为新连接）
        /// </summary>
        /// <param name="start"></param>
        /// <param name="link"></param>
        /// <param name="thumb"></param>
        /// <param name="isNew"></param>
        protected virtual void BeginDrag(Point start, ILink link, LinkThumbKind thumb, bool isNew)
        {
            _isNewLink = isNew;
            DragStart = start;
            Link = link;
            Thumb = thumb;
            InitialState = new LinkInfo(link);
            Adorner = CreateAdorner();
            (View.AllFeature["Drag"] as AddAdornerFeature).SetPublicAdorner("Drag", Adorner);
        }
        /// <summary>
        /// 拖动到指定位置（相对原位置）
        /// </summary>
        /// <param name="vector"></param>
        public virtual void DragTo(Vector vector)
        {
            vector = UpdateVector(vector);
            var point = DragStart + vector;
            var port = View.Children.OfType<iPortNode>().SelectMany(p => p.Ports)
                .Where(p => p.IsNear(point) && CanLinkTo(p))
                .OrderBy(p => GeometryHelper.Length(p.Center, point))
                .FirstOrDefault();

            UpdateLink(point, port);

            Adorner.Port = port;
            Link.UpdatePath();
        }
        /// <summary>
        /// 更新连接
        /// </summary>
        /// <param name="point"></param>
        /// <param name="port"></param>
        protected virtual void UpdateLink(Point point, IPort port)
        {
            if (Thumb == LinkThumbKind.Source)
            {
                Link.Source = port;
                Link.SourcePoint = port == null ? point : (Point?)null;
            }
            else
            {
                Link.Target = port;
                Link.TargetPoint = port == null ? point : (Point?)null;
            }
        }
        /// <summary>
        /// 返回是否可以连接到指定端口
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        protected virtual bool CanLinkTo(IPort port)
        {
            var pb = port as PortBase;
            if (pb != null)
            {
                if (Thumb == LinkThumbKind.Source)
                    return pb.CanAcceptOutgoingLinks;
                else
                    return pb.CanAcceptIncomingLinks;
            }
            else
                return true;
        }
        /// <summary>
        /// 是否可以接受释放
        /// </summary>
        /// <returns></returns>
        public virtual bool CanDrop()
        {
            return Adorner.Port != null;
        }
        /// <summary>
        /// 结束拖动
        /// </summary>
        /// <param name="doCommit"></param>
        public virtual void EndDrag(bool doCommit)
        {
            if (doCommit)
            {
                Controller.UpdateLink(InitialState, Link);
            }
            else
            {
                if (_isNewLink)
                    View.Children.Remove((Control)Link);
                else
                    InitialState.UpdateLink(Link);
            }
            Link.UpdatePath();
            Link = null;
            Adorner = null;
        }
        /// <summary>
        /// 从连接点上拖动出一条新的连接线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="port"></param>
        public virtual void BeginDragNewLink(Point start, IPort port)
        {
            var link = CreateNewLink(port);
            if (link != null && link is Control)
            {
                var thumb = (link.Source != null) ? LinkThumbKind.Target : LinkThumbKind.Source;
                View.Children.Add((Control)link);
                BeginDrag(start, link, thumb, true);
            }
        }
        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        protected virtual ILink CreateNewLink(IPort port)
        {
            var link = new SegmentLink();
            BindNewLinkToPort(port, link);
            return link;
        }
        /// <summary>
        /// 绑定一个新的连接到端口
        /// </summary>
        /// <param name="port"></param>
        /// <param name="link"></param>
        protected virtual void BindNewLinkToPort(IPort port, LinkBase link)
        {
            link.EndCap = true;
            var portBase = port as PortBase;
            if (portBase != null)
            {
                if (portBase.CanAcceptIncomingLinks && !portBase.CanAcceptOutgoingLinks)
                    link.Target = port;
                else
                    link.Source = port;
            }
            else
                link.Source = port;
        }
        /// <summary>
        /// 创建连接装饰器
        /// </summary>
        /// <returns></returns>
        protected virtual LinkAdorner CreateAdorner()
        {
            return new LinkAdorner(View, DragStart) { Cursor = Cursors.Cross };
        }
        /// <summary>
        /// 更新向量
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        protected virtual Vector UpdateVector(Vector vector)
        {
            return vector;
        }
    }
}

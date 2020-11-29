using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BasicLib
{
    class DiagramWithRulerController : DiagramController
    {
        public DiagramWithRulerController(DiagramView view, PageModelBase model) : base(view, model)
        {
        }

        ///// <summary>
        ///// 更新节点
        ///// </summary>
        ///// <param name="node"></param>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //protected override Node UpdateNode(NodeModelBase node, Node item)
        //{
        //    if (item == null)
        //    {
        //        item = new Node();
        //        item.ModelElement = node;
        //        CreatePorts(node, item);
        //        item.Content = CreateContent(node);
        //    }
        //    item.Width = _view.GridCellSize.Width - 20;
        //    item.Height = _view.GridCellSize.Height - 50;
        //    item.CanResize = false;
        //    item.SetValue(Canvas.LeftProperty, node.Column * _view.GridCellSize.Width + 10);
        //    item.SetValue(Canvas.TopProperty, node.Row * _view.GridCellSize.Height + 25);
        //    return item;
        //}

        ///// <summary>
        ///// 在视图层面创建一个连接线
        ///// </summary>
        ///// <param name="link">一个连接的实例</param>
        ///// <returns></returns>
        //private Control CreateLink(LinkModelBase link)
        //{
        //    var item = new OrthogonalLink();
        //    item.ModelElement = link;
        //    item.EndCap = true;
        //    item.Source = FindPort(link.Source, link.SourcePort);
        //    item.Target = FindPort(link.Target, link.TargetPort);

        //    var b = new Binding("Text");
        //    b.Source = link;
        //    item.SetBinding(LinkBase.LabelProperty, b);

        //    return item;
        //}


        ///// <summary>
        ///// 在视图层面创建一个端口
        ///// </summary>
        ///// <param name="node"></param>
        ///// <param name="item"></param>
        //private void CreatePorts(NodeModelBase node, Node item)
        //{
        //    foreach (var kind in node.GetPorts())
        //    {
        //        var port = new Aga.Diagrams.Controls.EllipsePort();
        //        port.Width = 10;
        //        port.Height = 10;
        //        port.Margin = new Thickness(-5);
        //        port.Visibility = Visibility.Visible;
        //        port.VerticalAlignment = ToVerticalAligment(kind);
        //        port.HorizontalAlignment = ToHorizontalAligment(kind);
        //        port.CanAcceptIncomingLinks = kind == PortKinds.Top;
        //        port.CanAcceptOutgoingLinks = !port.CanAcceptIncomingLinks;
        //        port.Tag = kind;
        //        port.Cursor = Cursors.Cross;
        //        port.CanCreateLink = true;
        //        item.Ports.Add(port);
        //    }
        //}

        ///// <summary>
        ///// 查找一个端口
        ///// </summary>
        ///// <param name="node"></param>
        ///// <param name="portKind"></param>
        ///// <returns></returns>
        //private Aga.Diagrams.Controls.IPort FindPort(FlowNode node, PortKinds portKind)
        //{
        //    var inode = _view.Items.FirstOrDefault(p => p.ModelElement == node) as Aga.Diagrams.Controls.INode;
        //    if (inode == null)
        //        return null;
        //    var port = inode.Ports.OfType<FrameworkElement>().FirstOrDefault(
        //        p => p.VerticalAlignment == ToVerticalAligment(portKind)
        //            && p.HorizontalAlignment == ToHorizontalAligment(portKind)
        //        );
        //    return (Aga.Diagrams.Controls.IPort)port;
        //}




        ///// <summary>
        ///// 通过端口类型获取一个端口的纵向布局属性
        ///// </summary>
        ///// <param name="kind"></param>
        ///// <returns></returns>
        //private VerticalAlignment ToVerticalAligment(PortKinds kind)
        //{
        //    if (kind == PortKinds.Top)
        //        return VerticalAlignment.Top;
        //    if (kind == PortKinds.Bottom)
        //        return VerticalAlignment.Bottom;
        //    else
        //        return VerticalAlignment.Center;
        //}

        ///// <summary>
        ///// 通过端口类型获取一个端口的横向布局属性
        ///// </summary>
        ///// <param name="kind"></param>
        ///// <returns></returns>
        //private HorizontalAlignment ToHorizontalAligment(PortKinds kind)
        //{
        //    if (kind == PortKinds.Left)
        //        return HorizontalAlignment.Left;
        //    if (kind == PortKinds.Right)
        //        return HorizontalAlignment.Right;
        //    else
        //        return HorizontalAlignment.Center;
        //}
        protected override Control CreateLink(LinkModelBase link)
        {
            throw new NotImplementedException();
        }

        protected override Node UpdateNode(NodeModelBase node, Node item)
        {
            throw new NotImplementedException();
        }
    }
}

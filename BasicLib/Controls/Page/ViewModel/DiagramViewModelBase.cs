using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BasicLib
{
    abstract class DiagramViewModelBase : AppViewModelBase
    {
        public DiagramViewModelBase(string parameter)
        {
            viewModelName = parameter;
        }

        ///// <summary>
        ///// 节点集合修改
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void NodesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    if (e.NewItems != null)
        //        foreach (var t in e.NewItems.OfType<INotifyPropertyChanged>())
        //            t.PropertyChanged += NodePropertyChanged;

        //    if (e.OldItems != null)
        //        foreach (var t in e.OldItems.OfType<INotifyPropertyChanged>())
        //            t.PropertyChanged -= NodePropertyChanged;
        //    UpdateView();
        //}

        ///// <summary>
        ///// 连接集合修改
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void LinksCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    UpdateView();
        //}

        ///// <summary>
        ///// 节点属性修改
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void NodePropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    var fn = sender as NodeModelBase;
        //    var n = _view.Children.OfType<Node>().FirstOrDefault(p => p.ModelElement == fn);
        //    if (fn != null && n != null)
        //        UpdateNode(fn, n);
        //}



        ///// <summary>
        ///// 更新节点
        ///// </summary>
        ///// <param name="node"></param>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //private Node UpdateNode(NodeModelBase node, Node item)
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
        ///// 在视图层面创建一个新的节点
        ///// </summary>
        ///// <param name="node"></param>
        ///// <returns></returns>
        //public static FrameworkElement CreateContent(NodeModelBase node)
        //{
        //    var textBlock = new TextBlock()
        //    {
        //        VerticalAlignment = VerticalAlignment.Center,
        //        HorizontalAlignment = HorizontalAlignment.Center
        //    };
        //    var b = new Binding("Text");
        //    b.Source = node;
        //    textBlock.SetBinding(TextBlock.TextProperty, b);

        //    if (node.Kind == NodeKinds.Start || node.Kind == NodeKinds.End)
        //    {
        //        var ui = new Border();
        //        ui.CornerRadius = new CornerRadius(15);
        //        ui.BorderBrush = Brushes.Black;
        //        ui.BorderThickness = new Thickness(1);
        //        ui.Background = Brushes.Yellow;
        //        ui.Child = textBlock;
        //        return ui;
        //    }
        //    else if (node.Kind == NodeKinds.Action)
        //    {
        //        var ui = new Border();
        //        ui.BorderBrush = Brushes.Black;
        //        ui.BorderThickness = new Thickness(1);
        //        ui.Background = Brushes.Lime; ;
        //        ui.Child = textBlock;
        //        return ui;
        //    }
        //    else
        //    {
        //        var ui = new Path();
        //        ui.Stroke = Brushes.Black;
        //        ui.StrokeThickness = 1;
        //        ui.Fill = Brushes.Pink;
        //        var converter = new GeometryConverter();
        //        ui.Data = (Geometry)converter.ConvertFrom("M 0,0.25 L 0.5 0 L 1,0.25 L 0.5,0.5 Z");
        //        ui.Stretch = Stretch.Uniform;

        //        var grid = new Grid();
        //        grid.Children.Add(ui);
        //        grid.Children.Add(textBlock);

        //        return grid;
        //    }
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
        //private void CreatePorts(NodeViewModelBase node, Node item)
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

        /// <summary>
        /// 删除当前选择的节点或连接
        /// </summary>
        //private void DeleteSelection()
        //{
        //    using (BeginUpdate())
        //    {
        //        var nodes = _view.Selection.Select(p => p.ModelElement as FlowNode).Where(p => p != null);
        //        var links = _view.Selection.Select(p => p.ModelElement as Link).Where(p => p != null);
        //        _model.Nodes.RemoveRange(p => nodes.Contains(p));
        //        _model.Links.RemoveRange(p => links.Contains(p));
        //        _model.Links.RemoveRange(p => nodes.Contains(p.Source) || nodes.Contains(p.Target));
        //    }
        //}

        ///// <summary>
        ///// 开始更新
        ///// </summary>
        ///// <returns></returns>
        //private IDisposable BeginUpdate()
        //{
        //    _updateScope.IsInProgress = true;
        //    return _updateScope;
        //}


    }
}

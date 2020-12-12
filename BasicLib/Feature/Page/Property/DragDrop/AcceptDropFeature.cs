using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Model_Struct_Builder;

namespace BasicLib
{
    /// <summary>
    /// 拖拽元素（节点）的信息
    /// </summary>
    public struct DragDropElementInfomation
    {
        /// <summary>
        /// 元素的名称
        /// </summary>
        public string name;
        /// <summary>
        /// 元素的类型
        /// </summary>
        public string elementType;
        /// <summary>
        /// 元素的model
        /// </summary>
        public object element;
    }
    /// <summary>
    /// 拖放的信息
    /// </summary>
    public struct DropInfomation
    {
        /// <summary>
        /// 接收拖放的元素
        /// </summary>
        public object AcceptDropObject;
        /// <summary>
        /// 接收拖放的点
        /// </summary>
        public Point point;
    }

    /// <summary>
    /// 使页面可以接受来自某些 指定源 的拖放操作
    /// </summary>
    public class AcceptDropFeature : FeaturePropertyBase
    {
        /// <summary>
        /// 指定的流程模型
        /// </summary>
        NodeDiagramEP model;
        new DiagramView view;

        double posX, posY;
        List<string> AcceptableSources;
        List<string> AcceptableType;

        List<ItemsControlDragHelper> allDragHelper = new List<ItemsControlDragHelper>();

        delegate bool CanDropDetector(object sender, DragEventArgs e);
        List<CanDropDetector> allDetector = new List<CanDropDetector>();

        public override void CreateFeature(FrameworkElement viewElement, string viewName)
        {
            AcceptableSources = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", "AcceptDrop", "Sources");
            AcceptableType = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", "AcceptDrop", "Types");
            Create(viewElement, viewName);
        }
        public override void CreatePropertyFeature(FrameworkElement view, string viewName, string groupName)
        {
            AcceptableSources = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", groupName, "AcceptDrop", "Sources");
            AcceptableType = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", groupName, "AcceptDrop", "Types");
            Create(view, viewName);
        }
        protected override void Create(FrameworkElement viewElement, string viewName)
        {
            view = viewElement.FindName("View") as DiagramView;

            view.DragEnter += OnDragEnter;
            view.DragOver += OnDragOver;
            view.DragLeave += OnDragLeave;
            view.Drop += OnDrop;

            foreach (string s in AcceptableSources)
            {
                var st = FrameController.GetInstence().AllPanel;
                allDragHelper.Add(new ItemsControlDragHelper(FrameController.GetInstence().AllPanel[s].FindName("Collection") as ItemsControl, view));
            }
        }

        public override void ChangeModel(iFrameElement element)
        {
            model = element.ElementProperty[""] as NodeDiagramEP;
        }

        /// <summary>
        /// 开始拖拽
        /// </summary>
        /// <param name="e"></param>
        public void OnDragEnter(object senderm, DragEventArgs e)
        {
        }

        /// <summary>
        /// 在页面上拖拽的过程
        /// </summary>
        /// <param name="e"></param>
        public void OnDragOver(object sender, DragEventArgs e)
        {
            IInputElement tmp = sender as IInputElement;
            e.Effects = DragDropEffects.None;
            var kv = e.Data.GetDataPresent(typeof(DragDropElementInfomation));
            if (e.Data.GetDataPresent(typeof(DragDropElementInfomation)) && AcceptableType.Contains(((DragDropElementInfomation)e.Data.GetData(typeof(DragDropElementInfomation))).elementType))
            {
                var position = e.GetPosition(tmp);
                posX = position.X;
                posY = position.Y;
                e.Effects = e.AllowedEffects;
                foreach (CanDropDetector fun in allDetector)
                {
                    if (fun.Invoke(sender, e) == false)
                    {
                        e.Effects = DragDropEffects.None;
                        break;
                    }
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// 拖拽离开
        /// </summary>
        /// <param name="e"></param>
        public void OnDragLeave(object sender, DragEventArgs e)
        {
        }

        /// <summary>
        /// 拖放
        /// </summary>
        /// <param name="e"></param>
        public void OnDrop(object sender, DragEventArgs e)
        {
            PackageMsgCenter.SendMsg(new PackageMsgVarKv<DropInfomation, DragDropElementInfomation>(
                AllPackageMsg.AcceptDrop,
                new DropInfomation() { AcceptDropObject = view, point = e.GetPosition(view) },
                (DragDropElementInfomation)e.Data.GetData(typeof(DragDropElementInfomation))));
            //var node = new FlowNode((NodeKinds)e.Data.GetData(typeof(NodeKinds)));
            //node.Row = _row;
            //node.Column = _column;
            //model.Nodes.Add(node);
            //e.Handled = true;
        }
    }
}

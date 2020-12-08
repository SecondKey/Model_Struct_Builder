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
    struct DragDropInfomation
    {
        public string name;
        public string elementType;
    }

    /// <summary>
    /// 使页面可以接受来自某些 指定源 的拖放操作
    /// </summary>
    public class AcceptDropFeature : iPanelFeatureProperty
    {
        /// <summary>
        /// 指定的流程模型
        /// </summary>
        DiagramEP model;
        double posX, posY;
        List<string> AcceptableSources;
        List<string> AcceptableType;

        List<ItemsControlDragHelper> allDragHelper = new List<ItemsControlDragHelper>();

        delegate bool CanDropDetector(object sender, DragEventArgs e);
        List<CanDropDetector> allDetector = new List<CanDropDetector>();

        public void CreateFeature(FrameworkElement view, string viewName)
        {
            AcceptableSources = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", "AcceptDrop", "Sources");
            AcceptableType = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", "AcceptDrop", "Types");
            BuildDropFeature(view as Control);
        }

        public void CreatePropertyFeature(FrameworkElement view, string viewName, string groupName)
        {
            AcceptableSources = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", groupName, "AcceptDrop", "Sources");
            AcceptableType = FrameController.GetInstence().MainFrameData.GetOneElementAllContent("Panel", viewName, "Feature", groupName, "AcceptDrop", "Types");
            BuildDropFeature(view as Control);
        }

        void BuildDropFeature(Control view)
        {
            view.DragEnter += OnDragEnter;
            view.DragOver += OnDragOver;
            view.DragLeave += OnDragLeave;
            view.Drop += OnDrop;

            foreach (string s in AcceptableSources)
            {
                var st = FrameController.GetInstence().AllPanel;
                allDragHelper.Add(new ItemsControlDragHelper(FrameController.GetInstence().AllPanel[s], view));
            }
        }

        public void ChangeModel(iFrameElement element)
        {
            model = element.ElementProperty[""] as DiagramEP;
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
            var kv = e.Data.GetDataPresent(typeof(DragDropInfomation));
            if (e.Data.GetDataPresent(typeof(DragDropInfomation)) && AcceptableType.Contains(((DragDropInfomation)e.Data.GetData(typeof(DragDropInfomation))).elementType))
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
            //var node = new FlowNode((NodeKinds)e.Data.GetData(typeof(NodeKinds)));
            //node.Row = _row;
            //node.Column = _column;
            //model.Nodes.Add(node);
            //e.Handled = true;
        }
    }
}

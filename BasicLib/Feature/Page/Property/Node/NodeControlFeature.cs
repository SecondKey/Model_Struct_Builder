using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BasicLib

{
    class NodeControlFeature : FeaturePropertyBase
    {
        NodeDiagramEP model;
        public override void ChangeModel(iFrameElement element)
        {
            model = element.ElementProperty[""] as NodeDiagramEP;
            UpdateView();
        }
        protected override void Create(FrameworkElement viewElement, string viewName)
        {
            this.RegistSelf(AllPackageMsg.AcceptDrop, (msg) => { CreateNode(msg as PackageMsgVarKv<DropInfomation, DragDropElementInfomation>); });
        }

        BoolState updating = new BoolState(false);
        Dictionary<string, DiagramItem> allNodes = new Dictionary<string, DiagramItem>();

        void UpdateView()
        {
            ClearNode();
            foreach (var s in model.allNode.Values)
            {
                CreateNode(s);
            }
            //UpdateUIElement(s);
        }

        void CreateNode(PackageMsgVarKv<DropInfomation, DragDropElementInfomation> msg)
        {
            if (msg.parameter.AcceptDropObject == view)
            {
                if (msg.p1.element != null && msg.p1.element is iNodeModel)
                {
                    CreateNode(msg.p1.element as iNodeModel);
                }
                else
                {
                    CreateNode(msg.p1.elementType, msg.parameter.point);
                }
            }
        }

        void CreateNode(iNodeModel node)
        {
            Console.WriteLine("CreateNode");
        }

        void CreateNode(string elementType, Point p)
        {
            Console.WriteLine("CreateNode");

            string TestNodeName = allNodes.Count.ToString();
            NodeModelBase nodeModel = NodeModelBase.GetNodeModel(TestNodeName, elementType);
            DiagramItem node = NodeViewModelBase.GetNodeViewModel(nodeModel).GetCompleteNode();
            node.Width = 100;
            node.Height = 50;
            node.SetValue(Canvas.LeftProperty, p.X - 50);
            node.SetValue(Canvas.TopProperty, p.Y - 25);
            AddNode(TestNodeName, node);
        }

        void UpdateNode(NodeModelBase node)
        {
            //NodeViewModelBase vm = NodeViewModelBase.GetNodeViewModel(elementType);
            //var node = vm.GetNode();
            //node.Width = 90;
            //node.Height = 45;
        }

        void NodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!updating.Value)
            {
                var node = sender as NodeModelBase;
                UpdateNode(node);
            }
        }

        #region View
        public void AddNode(string itemName, DiagramItem item)
        {
            allNodes.Add(itemName, item);
            (view as Canvas).Children.Add(item);
        }

        public void RemoveNode(string itemName)
        {
            allNodes.Remove(itemName);
            (view as Canvas).Children.Remove(allNodes[itemName]);
        }

        public void ClearNode()
        {
            foreach (var kv in allNodes.Values)
            {
                (view as Canvas).Children.Remove(kv);
            }
            allNodes.Clear();
        }
        #endregion
    }
}

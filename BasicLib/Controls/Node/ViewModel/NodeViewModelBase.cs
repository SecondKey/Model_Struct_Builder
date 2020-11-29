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
    public abstract class NodeViewModelBase : AppViewModelBase
    {
        NodeModelBase model;

        public NodeViewModelBase(string parameter) : base(parameter)
        {
            if (Frame.MainFrameData.HasElement("Node", parameter, "Style"))
            {
                NodeStyle = Frame.MainFrameData.GetAllElementContent("Node", parameter, "Style");
            }
            if (Frame.MainFrameData.HasElement("Node", parameter, "Parameters"))
            {
                NodeParameters = Frame.MainFrameData.GetAllElementContent("Node", parameter, "Parameters");
            }
        }

        public NodeViewModelBase(NodeModelBase node)
        {

        }

        protected Dictionary<string, string> NodeStyle;

        protected Dictionary<string, string> NodeParameters;

        public delegate FrameworkElement GetNodeMethod();

        private GetNodeMethod getNode;
        public GetNodeMethod GetNode
        {
            get
            {
                if (getNode == null)
                {
                    return GetCommonNode;
                }
                return getNode;
            }
            set
            {
                getNode = value;
            }
        }

        public abstract FrameworkElement GetCommonNode();

        public static NodeViewModelBase GetNodeViewModel(string nodeName)
        {
            NodeType type = (NodeType)Enum.Parse(typeof(NodeType), FrameController.GetInstence().MainFrameData.GetContent("Node", nodeName, "Type"));
            switch (type)
            {
                case NodeType.CommonNode:
                    return new CommonNodeViewModel(nodeName);
            }
            return new CommonNodeViewModel(nodeName);
        }

    }
}

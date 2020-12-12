using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    public enum NodeModelType
    {
        FreeDiagramNode,

    }

    /// <summary>
    /// 基础的节点模型
    /// </summary>
    public abstract class NodeModelBase : iNodeModel
    {
        public NodeModelBase(string nodeName, string type)
        {
            NodeName = nodeName;
            nodeType = type;
            viewType = FrameController.GetInstence().MainFrameData.GetContent("Node", type, "ViewType");
            renderType = FrameController.GetInstence().MainFrameData.GetContent("Node", type, "RenderType");
            modelType = FrameController.GetInstence().MainFrameData.GetContent("Node", type, "ModelType");
        }

        public string NodeName { get; set; }

        public string nodeType { get; private set; }

        public string viewType { get; private set; }

        public string renderType { get; private set; }

        public string modelType { get; private set; }

        public void LoadNode(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SaveNode(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public static NodeModelBase GetNodeModel(string nodeName, string type)
        {

            NodeModelType modelType = (NodeModelType)Enum.Parse(typeof(NodeModelType), FrameController.GetInstence().MainFrameData.GetContent("Node", type, "ModelType"));

            switch (modelType)
            {
                case NodeModelType.FreeDiagramNode:
                    return new FreeDiagramNodeModel(nodeName, type);
                default:
                    return null;
            }
        }
    }
}

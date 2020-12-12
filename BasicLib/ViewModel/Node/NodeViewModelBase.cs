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
    public enum ViewType
    {
        CommonNode,
    }

    public enum NodeStyleType
    {
        Color,
        Geometry
    }

    public abstract class NodeViewModelBase : PackageViewModelBase
    {
        protected Dictionary<string, string> NodeStyle;

        public string NodeName { get { return viewModelName; } }

        /// <summary>
        /// 节点上显示的是节点的类型而非元素名（节点没有model或model为空）是使用
        /// </summary>
        /// <param name="nodeType"></param>
        public NodeViewModelBase(string nodeType) : base(nodeType)
        {
            LoadType(nodeType);
        }

        /// <summary>
        /// 节点需要显示元素的信息或内容时使用
        /// </summary>
        /// <param name="model"></param>
        public NodeViewModelBase(iNodeModel model) : base(model as iFrameElement)
        {
            this.model = model as iFrameElement;
            LoadType(model.nodeType);
        }

        void LoadType(string nodeType)
        {
            if (Frame.MainFrameData.HasElement("Node", nodeType, "Style"))
            {
                NodeStyle = Frame.MainFrameData.GetAllElementContent("Node", nodeType, "Style");
            }
            foreach (string feature in Frame.MainFrameData.GetOneElementAllContent("Node", nodeType, "Feature"))
            {
                AllFeature.Add(feature, GetFeature(feature, nodeType));
            }
        }

        #region GetViewModel
        public static NodeViewModelBase GetNodeViewModel(string nodeType)
        {
            ViewType viewType = (ViewType)Enum.Parse(typeof(ViewType), FrameController.GetInstence().MainFrameData.GetContent("Node", nodeType, "ViewType"));
            switch (viewType)
            {
                case ViewType.CommonNode:
                    return new CommonNodeViewModel(nodeType);
            }
            return new CommonNodeViewModel(nodeType);
        }

        public static NodeViewModelBase GetNodeViewModel(NodeModelBase nodeModel)
        {
            ViewType viewType = (ViewType)Enum.Parse(typeof(ViewType), FrameController.GetInstence().MainFrameData.GetContent("Node", nodeModel.nodeType, "ViewType"));
            switch (viewType)
            {
                case ViewType.CommonNode:
                    return new CommonNodeViewModel(nodeModel);
            }
            return new CommonNodeViewModel(nodeModel);
        }
        #endregion

        #region GetNode
        public delegate FrameworkElement GetNodeMethod();
        protected abstract FrameworkElement GetCommonNode();
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

        public abstract DiagramItem GetCompleteNode();

        protected override iFeature GetFeature(string Featurename, string elementName)
        {
            iFeature feature;
            switch (Featurename)
            {
                #region Property
                #region Node
                case " ItemSelected":
                    feature = new ItemSelectedFeature();
                    break;
                #endregion

                #region General
                case "AddAdorner":
                    feature = new AddAdornerFeature();
                    break;
                #endregion
                #endregion


                #region Group
                case "CommonNodeGroup":
                    feature = new CommonNodeGroup();
                    break;
                #endregion
                default:
                    return null;
            }
            return feature;
        }
        #endregion
    }
}

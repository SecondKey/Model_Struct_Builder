using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model_Struct_Builder;

namespace BasicLib
{
    class NodeListViewModel : WindowViewModelBase
    {
        private List<NodeViewModelBase> nodeList = new List<NodeViewModelBase>();
        public List<NodeViewModelBase> NodeList
        {
            get { return nodeList; }
        }

        public NodeListViewModel(string windowName) : base(windowName)
        {
            foreach (var t in Frame.MainFrameData.GetOneElementAllContent("Parameters", windowName))
            {
                nodeList.Add(NodeViewModelBase.GetNodeViewModel(t));
            }
        }
    }
}

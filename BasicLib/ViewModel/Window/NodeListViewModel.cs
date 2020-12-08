using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model_Struct_Builder;

namespace BasicLib
{
    class NodeListViewModel : PackageViewModelBase
    {
        public NodeListViewModel(FrameworkElement view, string name) : base(view, name)
        {
            foreach (var t in Frame.MainFrameData.GetOneElementAllContent("Parameters", name))
            {
                nodeList.Add(NodeViewModelBase.GetNodeViewModel(t));
            }
        }

        private List<NodeViewModelBase> nodeList = new List<NodeViewModelBase>();
        public List<NodeViewModelBase> NodeList
        {
            get { return nodeList; }
        }
    }
}

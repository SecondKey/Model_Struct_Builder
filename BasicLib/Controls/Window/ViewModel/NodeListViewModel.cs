using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Struct_Builder;

namespace BasicLib
{
    class NodeListViewModel : AppViewModelBase, iPackageViewModel
    {
        public NodeListViewModel(string parameter)
        {
            viewModelName = parameter;
            foreach (var t in Frame.MainFrameData.GetOneElementAllContent("Parameters", parameter))
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

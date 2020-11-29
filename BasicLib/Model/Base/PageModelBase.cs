using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace BasicLib
{
    public class PageModelBase : ObservableObject
    {

        /// <summary>
        /// 一个图中所有的节点
        /// </summary>
        private ObservableCollection<NodeModelBase> _nodes = new ObservableCollection<NodeModelBase>();
        /// <summary>
        /// 一个图中所有的节点
        /// </summary>
        internal ObservableCollection<NodeModelBase> Nodes
        {
            get { return _nodes; }
        }

        /// <summary>
        /// 一个图中所有的连接
        /// </summary>
        private ObservableCollection<LinkModelBase> _links = new ObservableCollection<LinkModelBase>();
        /// <summary>
        /// 一个图中所有的连接
        /// </summary>
        internal ObservableCollection<LinkModelBase> Links
        {
            get { return _links; }
        }
    }
}

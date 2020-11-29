using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    interface iDiagramPage
    {
        /// <summary>
        /// 一个图中所有的节点
        /// </summary>
        ObservableCollection<NodeModelBase> Nodes { get; }
        /// <summary>
        /// 一个图中所有的连接
        /// </summary>
        ObservableCollection<LinkModelBase> Links { get; }
    }
}

using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    /// <summary>
    /// 基础的节点模型
    /// </summary>
    public class NodeModelBase : iNodeModel
    {
        public NodeModelBase(string name)
        {
            NodeName = name;
        }

        public virtual string NodeName { get; }

        public void LoadNode(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SaveNode(params string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Struct_Builder;

namespace BasicLib
{
    /// <summary>
    /// 基础的连接模型
    /// </summary>
    class LinkModelBase : iLinkModel
    {
        public LinkModelBase(iNodeModel source, iNodeModel target)
        {
            Source = source;
            Target = target;
        }

        #region iLinkModel Members
        public iNodeModel Source { get; private set; }
        public iNodeModel Target { get; private set; }
        public string LinkName { get; private set; }

        public void LoadLink(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SaveLink(params string[] parameters)
        {
            throw new NotImplementedException();
        }
        #endregion 
    }
}

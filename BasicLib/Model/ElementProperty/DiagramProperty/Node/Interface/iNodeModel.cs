using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    /// <summary>
    /// 图中的一个节点
    /// </summary>
    public interface iNodeModel
    {
        /// <summary>
        /// 节点的名称，在一个图中节点名称是唯一的
        /// </summary>
        string NodeName { get; }

        /// <summary>
        /// 加载该节点
        /// </summary>
        void LoadNode(params string[] parameters);
        /// <summary>
        /// 保存该节点
        /// </summary>
        /// <param name="parameters"></param>
        void SaveNode(params string[] parameters);
    }
}

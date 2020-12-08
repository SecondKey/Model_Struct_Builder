using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    /// <summary>
    /// 图表上的连接线
    /// </summary>
    public interface iLinkModel
    {
        /// <summary>
        /// 原节点
        /// </summary>
        iNodeModel Source { get; }
        /// <summary>
        /// 目标节点
        /// </summary
        iNodeModel Target { get; }
        /// <summary>
        /// 连接的名称
        /// </summary>
        string LinkName { get; }

        /// <summary>
        /// 加载连接信息
        /// </summary>
        /// <param name="parameters"></param>
        void LoadLink(params string[] parameters);
        /// <summary>
        /// 保存连接信息
        /// </summary>
        /// <param name="parameters"></param>
        void SaveLink(params string[] parameters);
    }
}

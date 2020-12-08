using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    /// <summary>
    /// 表示该节点是一个可以在图中活动的节点
    /// </summary>
    interface iPositionNode
    {
        /// <summary>
        /// 获取节点在图中的位置
        /// </summary>
        /// <returns></returns>
        object GetPosition();
        /// <summary>
        /// 设置节点的位置
        /// </summary>
        /// <param name="pos"></param>
        void SetPosition(object pos);
    }
}

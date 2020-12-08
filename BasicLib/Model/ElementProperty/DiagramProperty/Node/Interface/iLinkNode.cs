using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    /// <summary>
    /// 表示该节点直接引用了其他的项
    /// 双击该节点会跳转到引用的项（需要实现）
    /// </summary>
    interface iReferenceNode
    {
        /// <summary>
        /// 是否进行了引用
        /// </summary>
        bool IsReference { get; }
        /// <summary>
        /// 引用的项
        /// </summary>
        iFrameElement ReferenceElement { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
    /// <summary>
    /// 节点接口
    /// </summary>
    public interface iNode
    {
        Dictionary<string, object> nodePropertys { get; set; }

        void UpdatePosition();
    }
}

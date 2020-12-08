using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib.Model.ElementProperty.DiagramProperty.Link.Interface
{
    /// <summary>
    /// 表示该连接线会在图表上显示信息
    /// </summary>
    interface iShowInfoLink
    {
        /// <summary>
        /// //要显示的信息
        /// </summary>
        string ShowText { get; set; }
    }
}

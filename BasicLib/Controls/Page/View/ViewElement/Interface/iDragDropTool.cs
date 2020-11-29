using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BasicLib
{
    /// <summary>
    /// 拖放接口（拖动开始，拖动结束，拖动离开，放下）
    /// 负责从外部拖入拖出
    /// </summary>
    public interface iDragDropTool
    {
        /// <summary>
        /// 拖入
        /// </summary>
        /// <param name="e"></param>
        void OnDragEnter(System.Windows.DragEventArgs e);
        /// <summary>
        /// 拖动结束
        /// </summary>
        /// <param name="e"></param>
        void OnDragOver(System.Windows.DragEventArgs e);
        /// <summary>
        /// 拖出
        /// </summary>
        /// <param name="e"></param>
        void OnDragLeave(System.Windows.DragEventArgs e);
        /// <summary>
        /// 放下
        /// </summary>
        /// <param name="e"></param>
        void OnDrop(System.Windows.DragEventArgs e);
    }
}

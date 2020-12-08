using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace BasicLib
{
    /// <summary>
    /// 输入接口（鼠标按下，鼠标移动，鼠标抬起，预定义（Preview）按键按下）
    /// </summary>
    public interface iInputTool
    {
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="e"></param>
        void OnMouseDown(MouseButtonEventArgs e);
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="e"></param>
        void OnMouseMove(MouseEventArgs e);
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="e"></param>
        void OnMouseUp(MouseButtonEventArgs e);
        /// <summary>
        /// 按键按下的隧道路由
        /// </summary>
        /// <param name="e"></param>
        void OnPreviewKeyDown(KeyEventArgs e);
    }
}

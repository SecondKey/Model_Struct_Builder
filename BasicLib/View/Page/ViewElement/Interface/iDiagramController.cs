using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
    /// <summary>
    /// 图表核心控制器接口
    /// 主要负责图的更新和显示
    /// </summary>
    public interface iDiagramController
    {
        Dictionary<DiagramElementType, List<DiagramItem>> diagramItems { get; set; }



        /// <summary>
        /// Is called when user move/resize an item
        /// 当用户移动/调整项目大小时调用
        /// </summary>
        /// <param name="items">Selected items 选择项目</param>
        /// <param name="bounds">New item bounds新的item范围</param>
        void UpdateItemsBounds(DiagramItem[] items, Rect[] bounds);

        /// <summary>
        /// Is called when user create a link between items
        /// 当用户创建项目之间的链接或拖动时调用
        /// </summary>
        /// <param name="initialState">the state of the link before user action用户操作前链接的状态</param>
        /// <param name="link">Link in the current state链接处于当前状态</param>
        void UpdateLink(LinkInfo initialState, ILink link);

        /// <summary>
        /// 接收到命令后执行的操作
        /// 在DiagramView中注册命令
        /// 并在DiagramView接收到命令后调用
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        void ExecuteCommand(System.Windows.Input.ICommand command, object parameter);

        /// <summary>
        /// 是否可以执行操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        bool CanExecuteCommand(System.Windows.Input.ICommand command, object parameter);
    }
}

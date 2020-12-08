using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 连接线工具接口（开始拖动，拖拽创建一个新的连接，拖动到，是否可以接受拖放，结束拖拽）
	/// </summary>
	public interface iLinkTool
	{
		/// <summary>
		/// 开始拖动
		/// </summary>
		void BeginDrag(Point start, ILink link, LinkThumbKind thumb);
		/// <summary>
		/// 拖拽创建一个新的连接
		/// </summary>
		/// <param name="start"></param>
		/// <param name="port"></param>
		void BeginDragNewLink(Point start, IPort port);
		/// <summary>
		/// 拖动到
		/// </summary>
		/// <param name="vector"></param>
		void DragTo(Vector vector);
		/// <summary>
		/// 是否可以接受拖放
		/// </summary>
		/// <returns></returns>
		bool CanDrop();
		/// <summary>
		/// 结束拖拽
		/// </summary>
		/// <param name="doCommit"></param>
		void EndDrag(bool doCommit);
	}
}

using System;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 移动调整工具接口（开始拖拽，拖拽到，可以接受拖放，结束拖拽）
	/// 节点已经在图上的时候进行拖动
	/// </summary>
	public interface iMoveResizeTool
	{
		/// <summary>
		/// 开始拖拽
		/// </summary>
		/// <param name="start"></param>
		/// <param name="item"></param>
		/// <param name="kind"></param>
		void BeginDrag(Point start, DiagramItem item, DragThumbKinds kind);
		/// <summary>
		/// 拖拽到
		/// </summary>
		/// <param name="vector"></param>
		void DragTo(Vector vector);
		/// <summary>
		/// 可以拖拽
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

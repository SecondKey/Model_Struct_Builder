using System;
using System.Windows;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 移动调整工具接口（开始拖拽，拖拽到，可以接受拖放，结束拖拽）
	/// </summary>
	public interface IMoveResizeTool
	{
		void BeginDrag(Point start, DiagramItem item, DragThumbKinds kind);
		void DragTo(Vector vector);
		bool CanDrop();
		void EndDrag(bool doCommit);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 拖放接口（拖动开始，拖动结束，拖动离开，放下）
	/// </summary>
	public interface IDragDropTool
	{
		/// <summary>
		/// 拖动开始
		/// </summary>
		/// <param name="e"></param>
		void OnDragEnter(System.Windows.DragEventArgs e);
		/// <summary>
		/// 拖动结束
		/// </summary>
		/// <param name="e"></param>
		void OnDragOver(System.Windows.DragEventArgs e);
		/// <summary>
		/// 拖动离开
		/// </summary>
		/// <param name="e"></param>
		void OnDragLeave(System.Windows.DragEventArgs e);
		/// <summary>
		/// 拖放
		/// </summary>
		/// <param name="e"></param>
		void OnDrop(System.Windows.DragEventArgs e);
	}
}

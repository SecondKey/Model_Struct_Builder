using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Adorners
{
	/// <summary>
	/// 移动调整装饰器
	/// </summary>
	public class MoveResizeAdorner : DragAdorner
	{
		public MoveResizeAdorner(DiagramView view, Point start)
			: base(view, start)
		{
		}
		/// <summary>
		/// 执行拖动操作
		/// </summary>
		/// <returns></returns>
		protected override bool DoDrag()
		{
			View.DragTool.DragTo(End - Start);
			return View.DragTool.CanDrop();
		}
		/// <summary>
		/// 当拖动结束
		/// </summary>
		protected override void EndDrag()
		{
			View.DragTool.EndDrag(DoCommit);
		}
	}
}

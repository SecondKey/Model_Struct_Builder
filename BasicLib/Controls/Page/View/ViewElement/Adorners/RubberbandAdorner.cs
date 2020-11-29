using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace BasicLib
{
	/// <summary>
	/// 选择框装饰器，鼠标左键框选元素时创建
	/// </summary>
	class RubberbandAdorner : DragAdorner
	{
		/// <summary>
		/// 绘制选择框笔刷
		/// </summary>
		private Pen _pen;

		/// <summary>
		/// 设置默认笔刷
		/// </summary>
		/// <param name="view"></param>
		/// <param name="start"></param>
		public RubberbandAdorner(DiagramView view, Point start)
			: base(view, start)
		{
			_pen = new Pen(Brushes.Black, 1);
		}
		/// <summary>
		/// 进行拖拽操作
		/// </summary>
		/// <returns></returns>
		protected override bool DoDrag()
		{
			InvalidateVisual();
			return true;
		}
		/// <summary>
		/// 结束拖拽
		/// </summary>
		protected override void EndDrag()
		{
			if (DoCommit)
			{
				var rect = new Rect(Start, End);
				var items = View.Items.Where(p => p.CanSelect && rect.Contains(p.Bounds));
				View.Selection.SetRange(items);
			}
		}
		/// <summary>
		/// 实时渲染选框
		/// </summary>
		/// <param name="dc"></param>
		protected override void OnRender(DrawingContext dc)
		{
			dc.DrawRectangle(Brushes.Transparent, _pen, new Rect(Start, End));
		}
	}
}

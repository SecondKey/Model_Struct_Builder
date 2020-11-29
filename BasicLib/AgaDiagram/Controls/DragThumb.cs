using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls.Primitives;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;

namespace BasicLib
{
	/// <summary>
	/// 拖动的端口
	/// </summary>
	public class DragThumb: Control
	{
		/// <summary>
		/// 拖动端口的样式
		/// </summary>
		public DragThumbKinds Kind { get; set; }

		/// <summary>
		/// 鼠标按下的点
		/// </summary>
		protected Point? MouseDownPoint { get; set; }

		/// <summary>
		/// 当鼠标按下时操作
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				var item = this.DataContext as DiagramItem;
				var view = VisualHelper.FindParent<DiagramView>(item);
				if (item != null && view != null)
				{
					MouseDownPoint = e.GetPosition(view);
					view.DragTool.BeginDrag(MouseDownPoint.Value, item, this.Kind);
					e.Handled = true;
				}
			}
		}
	}
}

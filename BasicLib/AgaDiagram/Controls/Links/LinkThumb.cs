using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace BasicLib
{
	/// <summary>
	/// 连接的端口
	/// </summary>
	public class LinkThumb: Control
	{
		/// <summary>
		/// 连接的端口类型（起始端口/结束端口）
		/// </summary>
		public LinkThumbKind Kind { get; set; }
		/// <summary>
		/// 鼠标点击的位置
		/// </summary>
		protected Point? MouseDownPoint { get; set; }

		/// <summary>
		/// 鼠标点击事件
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
			{
				var link = this.DataContext as LinkBase;
				var view = VisualHelper.FindParent<DiagramView>(link);
				if (link != null && view != null)
				{
					MouseDownPoint = e.GetPosition(view);
					view.LinkTool.BeginDrag(MouseDownPoint.Value, link, this.Kind);
					e.Handled = true;
				}
			}
		}
	}
}

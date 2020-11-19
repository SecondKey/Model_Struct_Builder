using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Aga.Diagrams.Adorners;
using Aga.Diagrams.Controls;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 输入工具类
	/// </summary>
	public class InputTool : IInputTool
	{
		/// <summary>
		/// 对应视图界面
		/// </summary>
		protected DiagramView View { get; private set; }
		/// <summary>
		/// 对应控制器
		/// </summary>
		protected IDiagramController Controller { get { return View.Controller; } }
		/// <summary>
		/// 鼠标按下位置
		/// </summary>
		protected Point? MouseDownPoint { get; set; }
		/// <summary>
		/// 鼠标按下的元素
		/// </summary>
		protected DiagramItem MouseDownItem { get; set; }

		public InputTool(DiagramView view)
		{
			View = view;
		}
		/// <summary>
		/// 当鼠标按下时没修改按下元素和按下位置
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnMouseDown(MouseButtonEventArgs e)
		{
			MouseDownItem = (e.OriginalSource as DependencyObject).FindParent<DiagramItem>();
			MouseDownPoint = e.GetPosition(View);
			e.Handled = true;
		}

		/// <summary>
		/// 当鼠标移动时触发
		/// 如果选中元素，执行拖动方法
		/// 如果没有选中元素。创建一个选择框
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnMouseMove(MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed && MouseDownPoint.HasValue)
			{
				if (MouseDownItem != null)
				{
					View.DragTool.BeginDrag(MouseDownPoint.Value, MouseDownItem, DragThumbKinds.Center);
				}
				else
				{
					View.Selection.Clear();
					View.DragAdorner = CreateRubberbandAdorner();
				}
				MouseDownItem = null;
				MouseDownPoint = null;
			}
			e.Handled = true;
		}
		/// <summary>
		/// 当鼠标抬起时
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnMouseUp(MouseButtonEventArgs e)
		{
			if (MouseDownPoint == null)
				return;

			var item = (e.Source as DependencyObject).FindParent<DiagramItem>();
			SelectItem(item);
			e.Handled = true;
		}
		/// <summary>
		/// 当按键按下（Preview）
		/// </summary>
		/// <param name="e"></param>
		public virtual void OnPreviewKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				e.Handled = true;
				if (View.DragAdorner != null && View.DragAdorner.IsMouseCaptured)
					View.DragAdorner.ReleaseMouseCapture();
				else
					View.Selection.Clear();
			}
		}
		/// <summary>
		/// 选择元素（可多选）
		/// </summary>
		/// <param name="item"></param>
		protected virtual void SelectItem(DiagramItem item)
		{
			var sel = View.Selection;
			if (Keyboard.Modifiers == ModifierKeys.Control)
			{
				if (item != null && item.CanSelect)
				{
					if (item.IsSelected)
						sel.Remove(item);
					else
						sel.Add(item);
				}
			}
			else if (Keyboard.Modifiers == ModifierKeys.Shift)
			{
				if (item != null && item.CanSelect)
					sel.Add(item);
			}
			else
			{
				if (item != null && item.CanSelect)
					sel.Set(item);
				else
					sel.Clear();
			}
		}
		/// <summary>
		/// 创建选框
		/// </summary>
		/// <returns></returns>
		protected virtual Adorner CreateRubberbandAdorner()
		{
			return new RubberbandAdorner(View, MouseDownPoint.Value);
		}
	}
}

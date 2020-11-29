using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace BasicLib
{
	/// <summary>
	/// 选择装饰器
	/// </summary>
	public class SelectionAdorner : Adorner
	{
		/// <summary>
		/// 显示视觉库
		/// </summary>
		private VisualCollection _visuals;
		/// <summary>
		/// 被选中的控件
		/// </summary>
		private Control _control;

		/// <summary>
		/// 装饰器的数量
		/// </summary>
		protected override int VisualChildrenCount
		{
			get { return _visuals.Count; }
		}

		public SelectionAdorner(DiagramItem item, Control control)
			: base(item)
		{
			_control = control;
			_control.DataContext = item;
			_visuals = new VisualCollection(this);
			_visuals.Add(_control);
		}
		/// <summary>
		/// 定位元素并确定大小
		/// </summary>
		/// <param name="finalSize"></param>
		/// <returns></returns>
		protected override Size ArrangeOverride(Size finalSize)
		{
			_control.Arrange(new Rect(finalSize));
			return finalSize;
		}

		/// <summary>
		/// 获取指定的装饰器
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		protected override Visual GetVisualChild(int index)
		{
			return _visuals[index];
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 方形端口
	/// </summary>
	public class RectPort : PortBase
	{
		static RectPort()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(RectPort), new FrameworkPropertyMetadata(typeof(RectPort)));
		}
		/// <summary>
		/// 获取边缘位置，连接的指入和指出
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public override Point GetEdgePoint(Point target)
		{
			var rect = new Rect(Center.X - ActualWidth / 2, Center.Y - ActualHeight / 2, ActualWidth, ActualHeight);
			return GeometryHelper.RectLineIntersection(rect, target);
		}
		/// <summary>
		/// 判断是否临近
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public override bool IsNear(Point point)
		{
			var rect = new Rect(Center.X - ActualWidth / 2, Center.Y - ActualHeight / 2, ActualWidth, ActualHeight);
			rect.Inflate(Sensitivity, Sensitivity);
			return GeometryHelper.RectContains(rect, point);
		}
	}
}

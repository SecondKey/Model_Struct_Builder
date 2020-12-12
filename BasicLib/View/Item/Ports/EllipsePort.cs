using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 椭圆端口
	/// </summary>
	public class EllipsePort: PortBase
	{
		static EllipsePort()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(EllipsePort), new FrameworkPropertyMetadata(typeof(EllipsePort)));
		}

		/// <summary>
		/// 获取椭圆边上的一点
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public override Point GetEdgePoint(Point target)
		{
			var a = ActualWidth / 2;
			var b = ActualHeight / 2;
			var p = new Point(target.X - Center.X, target.Y - Center.Y);
			p = GeometryHelper.EllipseLineIntersection(a, b, p);
			return new Point(p.X + Center.X, p.Y + Center.Y);
		}
		/// <summary>
		/// 返回指定位置是否在端口可交互范围
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public override bool IsNear(Point point)
		{
			var a = ActualWidth / 2 + Sensitivity;
			var b = ActualHeight / 2 + Sensitivity;
			return GeometryHelper.EllipseContains(Center, a, b, point);
		}
	}
}

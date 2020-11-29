using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 连接的端口
	/// </summary>
	public interface IPort
	{
		/// <summary>
		/// 所有端口上的连接
		/// </summary>
		ICollection<ILink> Links { get; }
		/// <summary>
		/// 端口中心位置
		/// </summary>
		Point Center { get; }

		/// <summary>
		/// 判断指定位置是否在端口的可交互位置
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		bool IsNear(Point point);
		/// <summary>
		/// 获取边缘位置，该位置为连接指入和指出的位置
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		Point GetEdgePoint(Point target);
		/// <summary>
		/// 更新位置
		/// </summary>
		void UpdatePosition();
	}
}

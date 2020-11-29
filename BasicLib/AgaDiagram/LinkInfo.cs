using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 连接信息
	/// </summary>
	public class LinkInfo
	{
		/// <summary>
		/// 原端口
		/// </summary>
		public IPort Source { get; set; }
		/// <summary>
		/// 目标端口
		/// </summary>
		public IPort Target { get; set; }
		/// <summary>
		/// 原端口位置
		/// </summary>
		public Point? SourcePoint { get; set; }
		/// <summary>
		/// 目标端口位置
		/// </summary>
		public Point? TargetPoint { get; set; }

		/// <summary>
		/// 连接信息
		/// 通过传入一个连接来获取所有信息
		/// </summary>
		public LinkInfo(ILink link)
		{
			Source = link.Source;
			Target = link.Target;
			SourcePoint = link.SourcePoint;
			TargetPoint = link.TargetPoint;
		}
		/// <summary>
		/// 更新连接，选取一个新的连接
		/// </summary>
		/// <param name="link"></param>
		public void UpdateLink(ILink link)
		{
			link.Source = Source;
			link.Target = Target;
			link.SourcePoint = SourcePoint;
			link.TargetPoint = TargetPoint;
		}
	}
}

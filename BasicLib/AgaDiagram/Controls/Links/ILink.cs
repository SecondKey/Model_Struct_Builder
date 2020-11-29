using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 连接的接口
	/// </summary>
	public interface ILink
	{
		/// <summary>
		/// 原端口
		/// </summary>
		IPort Source { get; set; }
		/// <summary>
		/// 目标端口
		/// </summary>
		IPort Target { get; set; }
		/// <summary>
		/// 原位置（可空）
		/// </summary>
		Point? SourcePoint { get; set; }
		/// <summary>
		/// 目标位置（可空）
		/// </summary>
		Point? TargetPoint { get; set; }
		/// <summary>
		/// 更新位置
		/// </summary>
		void UpdatePath();
	}
}

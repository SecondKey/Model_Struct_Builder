using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 重新连接控制器
	/// </summary>
	public class RelinkControl : Control
	{
		static RelinkControl()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(RelinkControl), new FrameworkPropertyMetadata(typeof(RelinkControl)));
		}
	}
}

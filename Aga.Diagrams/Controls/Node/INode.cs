using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// 节点接口
	/// </summary>
	public interface INode
	{
		IEnumerable<IPort> Ports { get; }
	}
}

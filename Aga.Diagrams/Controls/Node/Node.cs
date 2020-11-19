using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Aga.Diagrams.Adorners;

namespace Aga.Diagrams.Controls
{
	/// <summary>
	/// 节点基类
	/// </summary>
	public class Node : DiagramItem, INode
	{
		static Node()
		{
			FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
				typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
		}

		#region Properties

		#region Content Property
		/// <summary>
		/// 节点内容
		/// </summary>
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content",
									   typeof(object),
									   typeof(Node));
		/// <summary>
		/// 节点内容
		/// </summary>
		public object Content 
		{
			get { return (bool)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
		#endregion

		#region CanResize Property
	    /// <summary>
		/// 是否可以修改
		/// </summary>
		public static readonly DependencyProperty CanResizeProperty =
			DependencyProperty.Register("CanResize",
							   typeof(bool),
							   typeof(Node),
							   new FrameworkPropertyMetadata(true));
		/// <summary>
		/// 是否可以修改
		/// </summary>
		public bool CanResize
		{
			get { return (bool)GetValue(CanResizeProperty); }
			set { SetValue(CanResizeProperty, value); }
		}
		#endregion
		/// <summary>
		/// 所有的端口
		/// </summary>
		private List<IPort> _ports = new List<IPort>();
		/// <summary>
		/// 所有的端口
		/// </summary>
		public ICollection<IPort> Ports { get { return _ports; } }

		/// <summary>
		/// 返回一个节点的范围
		/// </summary>
		public override Rect Bounds
		{
			get
			{
				//var itemRect = VisualTreeHelper.GetDescendantBounds(item);
				//return item.TransformToAncestor(this).TransformBounds(itemRect);
				var x = Canvas.GetLeft(this);
				var y = Canvas.GetTop(this);
				return new Rect(x, y, ActualWidth, ActualHeight);
			}
		}

		#endregion

		public Node()
		{
		}

		/// <summary>
		/// 更新位置
		/// </summary>
		public void UpdatePosition()
		{
			foreach (var p in Ports)
				p.UpdatePosition();
		}
		/// <summary>
		/// 创建一个选择装饰器
		/// </summary>
		/// <returns></returns>
		protected override Adorner CreateSelectionAdorner()
		{
			return new SelectionAdorner(this, new SelectionFrame());
		}

		#region INode Members

		IEnumerable<IPort> INode.Ports
		{
			get { return _ports; }
		}

		#endregion
	}
}

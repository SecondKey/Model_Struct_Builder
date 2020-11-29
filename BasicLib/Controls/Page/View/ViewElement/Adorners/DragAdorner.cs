using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace BasicLib
{
	/// <summary>
	/// 拖懂装饰器抽象类
	/// 除选择装饰器外所有装饰器的基类
	/// </summary>
	public abstract class DragAdorner: Adorner
	{
		/// <summary>
		/// 图表视图
		/// </summary>
		public DiagramView View { get; private set; }
		/// <summary>
		/// 进行提交
		/// </summary>
		protected bool DoCommit { get; set; }
		/// <summary>
		/// 拖放是否可以被接受
		/// </summary>
		private bool CanDrop { get; set; }
		/// <summary>
		/// 开始的点
		/// </summary>
		protected Point Start { get; set; }
		/// <summary>
		/// 结束的点
		/// </summary>
		protected Point End { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="view">装饰器需要绑定到View，同时还有持有View对象</param>
		/// <param name="start"></param>
		protected DragAdorner(DiagramView view, Point start): base(view)
		{
			View = view;
			End = Start = start;
			this.Loaded += OnLoaded;
		}
		/// <summary>
		/// 加载时尝试捕获鼠标
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			DoCommit = false;
			CaptureMouse();
		}
		/// <summary>
		/// 当鼠标移动时
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
		{
			End = e.GetPosition(View);
			CanDrop = DoDrag();
			Mouse.OverrideCursor = CanDrop ? Cursor : Cursors.No;
		}
		/// <summary>
		/// 当鼠标抬起时
		/// 如果当前对象在捕捉鼠标，判断是否可以接受拖放并释放对鼠标的捕捉
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
		{
			if (this.IsMouseCaptured)
			{
				DoCommit = CanDrop;
				this.ReleaseMouseCapture();
			}
		}

		/// <summary>
		/// 当失去鼠标捕捉时清空对象装饰器
		/// 重置鼠标
		/// 结束拖放操作
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLostMouseCapture(MouseEventArgs e)
		{
			View.DragAdorner = null;
			Mouse.OverrideCursor = null;
			EndDrag();
		}

		/// <summary>
		/// Returns true if drop is possible at this location
		/// 如果可以接受拖放就返回True
		/// </summary>
		protected abstract bool DoDrag();
		protected abstract void EndDrag();
	}
}

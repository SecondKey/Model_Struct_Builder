using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Aga.Diagrams.Controls;
using System.Windows.Documents;
using System.Windows.Controls;
using Aga.Diagrams.Adorners;

namespace Aga.Diagrams.Tools
{
	/// <summary>
	/// 移动调整 工具类
	/// </summary>
	public class MoveResizeTool : IMoveResizeTool
	{
		/// <summary>
		/// 拖拽端口的样式
		/// </summary>
		public DragThumbKinds DragKind { get; protected set; }
		/// <summary>
		/// 初始范围
		/// </summary>
		public Rect[] InitialBounds { get; protected set; }
		/// <summary>
		/// 图表中的元素
		/// </summary>
		public DiagramItem[] DragItems { get; protected set; }
		//public bool SnapToGrid { get; set; }
		/// <summary>
		/// 移动界面的单元格
		/// </summary>
		public Size MoveGridCell { get; set; }
		/// <summary>
		/// 调整界面的单元格
		/// </summary>
		public Size ResizeGridCell { get; set; }

		/// <summary>
		/// 指定的视图
		/// </summary>
		protected DiagramView View { get; private set; }
		/// <summary>
		/// 指定的控制器
		/// </summary>
		protected IDiagramController Controller { get { return View.Controller; } }
		/// <summary>
		/// 初始点
		/// </summary>
		protected Point Start { get; set; }

		public MoveResizeTool(DiagramView view)
		{
			View = view;
		}

		/// <summary>
		/// 开始拖拽
		/// </summary>
		/// <param name="start"></param>
		/// <param name="item"></param>
		/// <param name="kind"></param>
		public virtual void BeginDrag(Point start, DiagramItem item, DragThumbKinds kind)
		{
			Start = start;
			DragKind = kind;
			if (kind == DragThumbKinds.Center)
			{
				if (!item.CanMove || !IsMovable(item))
					return;
				if (!View.Selection.Contains(item))
					View.Selection.Set(item);
				DragItems = View.Selection.Where(p => p.CanMove && IsMovable(p)).ToArray();
			}
			else
			{
				DragItems = new DiagramItem[] { item };
			}
			InitialBounds = DragItems.Select(p => p.Bounds).ToArray();
			View.DragAdorner = CreateAdorner();
		}

		/// <summary>
		/// 返回对应的元素是否允许拖动
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected bool IsMovable(DiagramItem item)
		{
			return !(item is LinkBase);
		}

		/// <summary>
		/// 拖动到某一位置
		/// </summary>
		/// <param name="vector"></param>
		public virtual void DragTo(Vector vector)
		{
			vector = UpdateVector(vector);
			for (int i = 0; i < DragItems.Length; i++)
			{
				var item = DragItems[i];
				var rect = InitialBounds[i];
				if (DragKind == DragThumbKinds.Center)
				{
					Canvas.SetLeft(item, rect.X + vector.X);
					Canvas.SetTop(item, rect.Y + vector.Y);
				}
				else
				{
					if ((DragKind & DragThumbKinds.Left) != DragThumbKinds.None)
					{
						item.Width = Math.Max(item.MinWidth, rect.Width - vector.X);
						Canvas.SetLeft(item, Math.Min(rect.X + vector.X, rect.Right - item.MinWidth));
					}
					if ((DragKind & DragThumbKinds.Top) != DragThumbKinds.None)
					{
						item.Height = Math.Max(item.MinHeight, rect.Height - vector.Y);
						Canvas.SetTop(item, Math.Min(rect.Y + vector.Y, rect.Bottom - item.MinHeight));
					}
					if ((DragKind & DragThumbKinds.Right) != DragThumbKinds.None)
					{
						item.Width = Math.Max(0, rect.Width + vector.X);
					}
					if ((DragKind & DragThumbKinds.Bottom) != DragThumbKinds.None)
					{
						item.Height = Math.Max(0, rect.Height + vector.Y);
					}
				}
			}
		}

		/// <summary>
		/// 是否可以接受拖放
		/// </summary>
		/// <returns></returns>
		public virtual bool CanDrop()
		{
			return true;
		}
		/// <summary>
		/// 结束拖拽
		/// </summary>
		/// <param name="doCommit"></param>
		public virtual void EndDrag(bool doCommit)
		{
			if (doCommit)
			{
				var bounds = DragItems.Select(p => p.Bounds).ToArray();
				Controller.UpdateItemsBounds(DragItems, bounds);
			}
			else
			{
				RestoreBounds();
			}
			DragItems = null;
			InitialBounds = null;
		}

		/// <summary>
		/// 创建移动调整装饰器
		/// </summary>
		/// <returns></returns>
		protected virtual Adorner CreateAdorner()
		{
			return new MoveResizeAdorner(View, Start) { Cursor = GetCursor() };
		}

		/// <summary>
		/// 获取光标样式（上下拉动，左右拉动，全向拉动（斜角））
		/// </summary>
		/// <returns></returns>
		protected Cursor GetCursor()
		{
			switch (DragKind)
			{
				case DragThumbKinds.Center:
					return Cursors.SizeAll;
				case DragThumbKinds.Bottom:
				case DragThumbKinds.Top:
					return Cursors.SizeNS;
				case DragThumbKinds.Left:
				case DragThumbKinds.Right:
					return Cursors.SizeWE;
				case DragThumbKinds.TopLeft:
				case DragThumbKinds.BottomRight:
					return Cursors.SizeNWSE;
				case DragThumbKinds.TopRight:
				case DragThumbKinds.BottomLeft:
					return Cursors.SizeNESW;
			}
			return null;
		}

		/// <summary>
		/// 调整向量以匹配单元格
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		protected virtual Vector UpdateVector(Vector vector)
		{
			Size cell;
			if (DragKind == DragThumbKinds.Center)
				cell = MoveGridCell;
			else
				cell = ResizeGridCell;

			if (cell.Width > 0 && cell.Height > 0)
			{
				var x = Math.Round(vector.X / cell.Width) * cell.Width;
				var y = Math.Round(vector.Y / cell.Height) * cell.Height;
				return new Vector(x, y);
			}
			else
				return vector;
		}

		/// <summary>
		/// 恢复范围
		/// </summary>
		protected virtual void RestoreBounds()
		{
			for (int i = 0; i < DragItems.Length; i++)
			{
				var item = DragItems[i];
				var rect = InitialBounds[i];
				Canvas.SetLeft(item, rect.X);
				Canvas.SetTop(item, rect.Y);
				item.Width = rect.Width;
				item.Height = rect.Height;
			}
		}
	}
}

using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace BasicLib
{
    /// <summary>
    /// 鼠标点击拖动的位置
    /// </summary>
    [Flags]
    public enum DragThumbKinds
    {
        None = 0,
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        Center = 16,
        TopLeft = Top | Left,
        TopRight = Top | Right,
        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right
    }

    class ItemMoveFeature : FeaturePropertyBase
    {

        public ItemMoveFeature()
        {
            FeatureEvents = new Dictionary<string, FeatureEvent>()
            {
                { "SelectedItem",Selected },
                { "DeselectItem",Deselect}
            };
        }

        void Selected(object[] parameters)
        {
            view.AllFeature.DoFeatureEvent("AddIndependentAdorner", "Move", CreateSelectionAdorner());
        }

        void Deselect(object[] parameters)
        {
            view.AllFeature.DoFeatureEvent("RemoveIndependentAdorner", "Move", null);
        }

        /// <summary>
        /// 创建装饰器
        /// </summary>
        /// <returns></returns>
        protected Adorner CreateSelectionAdorner()
        {
            return new ControlAdorner(view as DiagramItem, new MoveAdornerElement());
        }









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


        /// <summary>
        /// 按照单元格大小移动元素
        /// </summary>
        public Size MoveGridCell { get; set; }

        Point start;

        /// <summary>
        /// 开始拖拽
        /// </summary>
        /// <param name="start"></param>
        /// <param name="item"></param>
        /// <param name="kind"></param>
        public virtual void BeginDrag(Point start, DiagramItem item, DragThumbKinds kind)
        {
            //this.start = start;
            //DragKind = kind;
            //if (kind == DragThumbKinds.Center)
            //{
            //    if (!item.CanMove || !IsMovable(item))
            //        return;
            //    if (!view.Selection.Contains(item))
            //        view.Selection.Set(item);
            //    DragItems = view.Selection.Where(p => p.CanMove && IsMovable(p)).ToArray();
            //}
            //else
            //{
            //    DragItems = new DiagramItem[] { item };
            //}
            //InitialBounds = DragItems.Select(p => p.Bounds).ToArray();
            //view..DragAdorner = CreateAdorner();
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
        /// 创建移动调整装饰器
        /// </summary>
        /// <returns></returns>
        protected virtual Adorner CreateAdorner()
        {
            return new MoveResizeAdorner(view as DiagramView, start) { Cursor = GetCursor() };
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
                //Controller.UpdateItemsBounds(DragItems, bounds);
            }
            else
            {
                RestoreBounds();
            }
            DragItems = null;
            InitialBounds = null;
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
            //Size cell;
            //if (DragKind == DragThumbKinds.Center)
            //    cell = MoveGridCell;
            //else
            //cell = ResizeGridCell;

            //if (cell.Width > 0 && cell.Height > 0)
            //{
            //    var x = Math.Round(vector.X / cell.Width) * cell.Width;
            //    var y = Math.Round(vector.Y / cell.Height) * cell.Height;
            //    return new Vector(x, y);
            //}
            //else
            //    return vector;
            return new Vector();
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

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
    class MouseSelectFeature : FeaturePropertyBase
    {

        protected override void Create(FrameworkElement viewElement, string viewName)
        {
            (view as DiagramView).MouseDown += OnMouseDown;
            (view as DiagramView).MouseMove += OnMouseMove;
            (view as DiagramView).MouseUp += OnMouseUp;
        }
        #region MouseOperation 
        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        protected Point? MouseDownPoint { get; set; }
        /// <summary>
        /// 鼠标按下的元素
        /// </summary>
        protected DiagramItem MouseDownItem { get; set; }

        /// <summary>
        /// 当鼠标按下时没修改按下元素和按下位置
        /// </summary>
        /// <param name="e"></param>
        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            IInputElement element = sender as IInputElement;
            MouseDownItem = (e.OriginalSource as DependencyObject).FindParent<DiagramItem>();
            MouseDownPoint = e.GetPosition(element);
            e.Handled = true;
        }

        /// <summary>
        /// 当鼠标移动时触发
        /// 如果选中元素，执行拖动方法
        /// 如果没有选中元素。创建一个选择框
        /// </summary>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && MouseDownPoint.HasValue)
            {
                if (MouseDownItem == null)
                {
                    (view.AllFeature["AddAdorner"] as AddAdornerFeature).SetPublicAdorner("Drag", CreateRubberbandAdorner((view as DiagramView)));
                }
                MouseDownItem = null;
                MouseDownPoint = null;
            }
            e.Handled = true;
        }
        /// <summary>
        /// 当鼠标抬起时
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MouseDownPoint == null)
                return;
            SelectItem((e.Source as DependencyObject).FindParent<DiagramItem>());
            e.Handled = true;
        }

        /// <summary>
        /// 选择元素（可多选）
        /// </summary>
        /// <param name="item"></param>
        protected virtual void SelectItem(DiagramItem item)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (item != null)
                {
                    if (primaryItem != item && allItem.Contains(item))
                        RemoveSelection(item);
                    else
                        AddSelection(item);
                }
            }
            else if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                if (item != null)
                    AddSelection(item);
            }
            else
            {
                if (item != null)
                    SetRange(new DiagramItem[] { item });
                else
                    DoClear();
            }
        }
        /// <summary>
        /// 创建选框
        /// </summary>
        /// <returns></returns>
        protected virtual Adorner CreateRubberbandAdorner(DiagramView view)
        {
            return new RubberbandAdorner(view, MouseDownPoint.Value);
        }
        #endregion

        #region Selection 
        /// <summary>
        /// 当前选择的节点
        /// </summary>
        private DiagramItem primaryItem;
        public DiagramItem PrimaryItem
        {
            get { return primaryItem; }
        }

        /// <summary>
        /// 所有选中的节点（多选，框选节点）
        /// </summary>
        private List<DiagramItem> allItem = new List<DiagramItem>();
        public List<DiagramItem> AllItem
        {
            get { return allItem; }
        }

        /// <summary>
        /// 节点总数
        /// </summary>
        public int Count
        {
            get { return allItem.Count; }
        }
        /// <summary>
        /// 是否包含某个节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(DiagramItem item)
        {
            return allItem.Contains(item);
        }


        /// <summary>
        /// 添加一个节点
        /// </summary>
        /// <param name="item"></param>
        public void AddSelection(DiagramItem item)
        {
            if (!allItem.Contains(item))
            {
                bool isPrimary = Count == 0;
                allItem.Add(item);
                if (isPrimary)
                {
                    primaryItem = item;
                }
            }
        }

        /// <summary>
        /// 移除一个节点
        /// </summary>
        /// <param name="item"></param>
        public void RemoveSelection(DiagramItem item)
        {
            if (allItem.Contains(item))
            {
                allItem.Remove(item);
            }
        }

        /// <summary>
        /// 选中事件，可以选中一个或多个
        /// </summary>
        /// <param name="items"></param>
        public void SetRange(IEnumerable<DiagramItem> items)
        {
            DoClear();//清除所有
            bool isPrimary = true;//第一个节点是主要节点
            foreach (var item in items)
            {
                allItem.Add(item);//将节点添加到字典中;//设为被选中
                if (isPrimary)//是否为主节点
                {
                    primaryItem = item;
                    item.AllFeature.DoFeatureEvent("SelectedItem", true);
                    isPrimary = false;
                }
                else
                {
                    item.AllFeature.DoFeatureEvent("SelectedItem");
                }
            }
        }

        /// <summary>
        /// 清除所有选择（内部方法）
        /// </summary>
        private void DoClear()
        {
            foreach (DiagramItem item in allItem)
            {
                item.AllFeature.DoFeatureEvent("DeselectItem");
            }
            allItem.Clear();
            primaryItem = null;
        }

        #endregion

        #region old 
        ///// <summary>
        ///// 当按键按下（Preview）
        ///// </summary>
        ///// <param name="e"></param>
        //public virtual void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    if (e.Key == Key.Escape)
        //    {
        //        e.Handled = true;
        //        if (View.DragAdorner != null && View.DragAdorner.IsMouseCaptured)
        //            View.DragAdorner.ReleaseMouseCapture();
        //        else
        //            View.Selection.Clear();
        //    }
        //}


        //bool isPrimary = true;//第一个节点是主要节点
        //item.IsSelected = true;//设为被选中
        //        if (isPrimary)//是否为主节点
        //        {
        //            _primary = item;
        //            item.IsPrimarySelection = true;
        //            isPrimary = false;
        //        }

        //    foreach (var item in Items)
        //item.IsSelected = false;
        #endregion
    }
}

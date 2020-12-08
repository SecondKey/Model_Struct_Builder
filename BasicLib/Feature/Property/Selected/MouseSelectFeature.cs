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
    class MouseSelectFeature : iPanelFeatureProperty
    {
        public void ChangeModel(iFrameElement element) { }

        public void CreateFeature(FrameworkElement view, string viewName)
        {
            Control tmpWindow = view as Control;
            tmpWindow.MouseDown += OnMouseDown;
            tmpWindow.MouseMove += OnMouseMove;
            tmpWindow.MouseUp += OnMouseUp;
        }

        public void CreatePropertyFeature(FrameworkElement view, string viewName, string groupName)
        {
            CreateFeature(view, viewName);
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
            UIElement element = (sender as FrameworkElement).FindName("View") as UIElement;
            if (e.LeftButton == MouseButtonState.Pressed && MouseDownPoint.HasValue)
            {
                if (MouseDownItem == null)
                {
                    AdornerLayer.GetAdornerLayer(element).Add(CreateRubberbandAdorner(element));
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

            var item = ((e.Source as DependencyObject).FindParent<DiagramItem>().DataContext as PackageViewModelBase).Model;
            SelectItem(item);
            e.Handled = true;
        }

        /// <summary>
        /// 选择元素（可多选）
        /// </summary>
        /// <param name="item"></param>
        protected virtual void SelectItem(iFrameElement item)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (item != null)
                {
                    if (primary != item && allElement.Contains(item))
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
                    SetRange(new iFrameElement[] { item });
                else
                    DoClear();
            }
        }
        /// <summary>
        /// 创建选框
        /// </summary>
        /// <returns></returns>
        protected virtual Adorner CreateRubberbandAdorner(UIElement view)
        {
            return new RubberbandAdorner(view, MouseDownPoint.Value);
        }
        #endregion



        #region Selection 
        /// <summary>
        /// 当前选择的节点
        /// </summary>
        private iFrameElement primary;
        public iFrameElement Primary
        {
            get { return primary; }
        }

        /// <summary>
        /// 所有选中的节点（多选，框选节点）
        /// </summary>
        private List<iFrameElement> allElement = new List<iFrameElement>();
        public List<iFrameElement> AllElement
        {
            get { return allElement; }
        }

        /// <summary>
        /// 节点总数
        /// </summary>
        public int Count
        {
            get { return allElement.Count; }
        }
        /// <summary>
        /// 是否包含某个节点
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(iFrameElement item)
        {
            return allElement.Contains(item);
        }


        /// <summary>
        /// 添加一个节点
        /// </summary>
        /// <param name="item"></param>
        public void AddSelection(iFrameElement item)
        {
            if (!allElement.Contains(item))
            {
                bool isPrimary = Count == 0;
                allElement.Add(item);
                if (isPrimary)
                {
                    primary = item;
                }
            }
        }

        /// <summary>
        /// 移除一个节点
        /// </summary>
        /// <param name="item"></param>
        public void RemoveSelection(iFrameElement item)
        {
            if (allElement.Contains(item))
            {
                allElement.Remove(item);
            }
        }


        /// <summary>
        /// 选中事件，可以选中一个或多个
        /// </summary>
        /// <param name="items"></param>
        public void SetRange(IEnumerable<iFrameElement> items)
        {
            DoClear();//清除所有
            foreach (var item in items)
            {
                allElement.Add(item);//将节点添加到字典中
            }
        }



        /// <summary>
        /// 清除所有选择（内部方法）
        /// </summary>
        private void DoClear()
        {
            allElement.Clear();
            primary = null;
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

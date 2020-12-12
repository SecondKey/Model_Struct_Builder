using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace BasicLib
{
    public class ControlAdorner : Adorner
    {
        private Control control;

        private VisualCollection visuals;

        /// <summary>
        /// 装饰器的数量
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }

        public ControlAdorner(DiagramItem item, Control control)
            : base(item)
        {
            this.control = control;
            control.DataContext = item;
            visuals = new VisualCollection(this);
            visuals.Add(control);
        }

        /// <summary>
        /// 定位元素并确定大小
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            control.Arrange(new Rect(finalSize));
            return finalSize;
        }

        /// <summary>
        /// 获取指定的装饰器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
    }
}

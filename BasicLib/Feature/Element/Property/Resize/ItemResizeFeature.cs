using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace BasicLib
{
    class ItemResizeFeature : FeaturePropertyBase
    {
        public ItemResizeFeature()
        {
            FeatureEvents = new Dictionary<string, FeatureEvent>()
            {
                { "SelectedItem",Selected },
                { "DeselectItem",Deselect}
            };
        }



        /// <summary>
        /// 按照单元格大小调整元素
        /// </summary>
        public Size ResizeGridCell { get; set; }

        void Selected(object[] parameters)
        {
            view.AllFeature.DoFeatureEvent("AddIndependentAdorner", "Resize", CreateSelectionAdorner());
        }

        void Deselect(object[] parameters)
        {
            view.AllFeature.DoFeatureEvent("RemoveIndependentAdorner", "Resize", null);
        }

        /// <summary>
        /// 创建装饰器
        /// </summary>
        /// <returns></returns>
        protected Adorner CreateSelectionAdorner()
        {
            return new ControlAdorner(view as DiagramItem, new ResizeAdornerElement());
        }
    }
}

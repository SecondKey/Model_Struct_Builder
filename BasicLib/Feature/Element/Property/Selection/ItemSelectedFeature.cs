using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace BasicLib
{
    public class ItemSelectedFeature : FeaturePropertyBase
    {
        public ItemSelectedFeature()
        {
            FeatureEvents = new Dictionary<string, FeatureEvent>()
            {
                { "SelectedItem",Selected },
                { "DeselectItem",Deselect}
            };
        }

        bool isSelected = false;
        bool isMain = false;

        public bool IsSelected { get { return isSelected; } set { isSelected = value; } }
        public bool IsMain { get { return isMain; } set { isMain = value; } }

        public void Selected(object[] parameters)
        {
            isSelected = true;
            if (parameters.Length > 0 && (bool)parameters[0])
            {
                this.isMain = true;
            }
            view.AllFeature.DoFeatureEvent("AddIndependentAdorner", "Selected", CreateSelectionAdorner());
        }

        public void Deselect(object[] parameters)
        {
            isSelected = false;
            isMain = false;
            view.AllFeature.DoFeatureEvent("RemoveIndependentAdorner", "Selected");
        }

        /// <summary>
        /// 创建装饰器
        /// </summary>
        /// <returns></returns>
        protected Adorner CreateSelectionAdorner()
        {
            return new ControlAdorner(view as DiagramItem, new SelectionAdornerElement());
            //if (view is iNode)
            //{
            //    return new SelectedAdorner(view as DiagramItem, new SelectionFrame());
            //}
            //else if (view is ILink)
            //{
            //    return new SelectedAdorner(view as DiagramItem, new RelinkControl());
            //}
            //else
            //{
            //    return null;
            //}
        }

    }
}

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
    class AddAdornerFeature : FeaturePropertyBase
    {
        public AddAdornerFeature()
        {
            FeatureEvents = new Dictionary<string, FeatureEvent>()
            {
                { "AddPublicAdorner",SetPublicAdorner },
                { "AddIndependentAdorner",AddIndependentAdorner},
                { "RemoveIndependentAdorner",RemoveIndependentAdorner}
            };
        }

        #region public
        /// <summary>
        /// 公共装饰器
        /// </summary>
        private Dictionary<string, Adorner> publicAdorner = new Dictionary<string, Adorner>();

        public void SetPublicAdorner(params object[] parameters)
        {
            string adornerName = (string)parameters[0];
            Adorner adorner = (Adorner)parameters[1];
            var adornerLayer = AdornerLayer.GetAdornerLayer(view as FrameworkElement);
            if (!publicAdorner.ContainsKey(adornerName))
            {
                publicAdorner.Add(adornerName, adorner);
                if (adorner != null)
                {
                    adornerLayer.Add(adorner);
                }
            }
            else if (publicAdorner[adornerName] != adorner)
            {
                if (publicAdorner[adornerName] != null)
                {
                    adornerLayer.Remove(publicAdorner[adornerName]);
                }
                if (adorner != null)
                {
                    adornerLayer.Add(adorner);
                }
                publicAdorner[adornerName] = adorner;
            }
        }

        public Adorner GetPublicAdorner(string adornerName)
        {
            if (publicAdorner.ContainsKey(adornerName))
            {
                return publicAdorner[adornerName];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region independent
        private Dictionary<string, Adorner> independentAdorner = new Dictionary<string, Adorner>();
        public void AddIndependentAdorner(params object[] parameters)
        {
            string adornerName = (string)parameters[0];
            Adorner adorner = (Adorner)parameters[1];
            var adornerLayer = AdornerLayer.GetAdornerLayer(view as FrameworkElement);
            if (!independentAdorner.ContainsKey(adornerName))
            {
                independentAdorner.Add(adornerName, adorner);
                adornerLayer.Add(adorner);
            }
        }

        public void RemoveIndependentAdorner(params object[] parameters)
        {
            string adornerName = (string)parameters[0];
            var adornerLayer = AdornerLayer.GetAdornerLayer(view as FrameworkElement);
            if (independentAdorner.ContainsKey(adornerName))
            {
                adornerLayer.Remove(independentAdorner[adornerName]);
                independentAdorner.Remove(adornerName);
            }
        }

        public Adorner GetIndependentAdorner(string adornerName)
        {
            return independentAdorner[adornerName];
        }
        #endregion

    }
}

using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    public abstract class FeatureBase : iFeature
    {
        protected iView view;

        public virtual void ChangeModel(iFrameElement element) { }

        public virtual void CreateFeature(FrameworkElement viewElement, string viewName)
        {
            view = viewElement.FindChild<iView>();
            Create(viewElement, viewName);
        }
        protected virtual void Create(FrameworkElement viewElement, string viewName) { }

        protected delegate void FeatureEvent(params object[] parameters);
        protected Dictionary<string, FeatureEvent> FeatureEvents = new Dictionary<string, FeatureEvent>();
        public bool DoFeatureEvent(string token, params object[] parameters)
        {
            if (FeatureEvents.ContainsKey(token))
            {
                FeatureEvents[token].Invoke(parameters);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

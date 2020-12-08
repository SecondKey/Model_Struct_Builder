using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 被废弃
    /// </summary>
    public interface iPackageController
    {
        #region Parameters
        Dictionary<string, Control> AllPanel { get; }
        Dictionary<string, iFrameElement> AllElement { get; set; }
        #endregion


        void RegistPackagePanel(string elementName, Control viewElement);
        Control GetPanel(string panelName);

    }
}

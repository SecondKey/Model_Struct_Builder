using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 用于快速访问Controller
    /// </summary>
    public class AppViewModelBase : ViewModelBase
    {
        public string viewModelName;
        public AppViewModelBase()
        {

        }

        public AppController App
        {
            get { return AppController.GetInstence(); }
        }

        public FrameController Frame
        {
            get { return FrameController.GetInstence(); }
        }

        public void ViewFunction(string t)
        {
            MsgCenter.SendMsg(new MsgVarKv<string, string>(AllAppMsg.ViewModelToView, viewModelName, t));
        }
    }
}

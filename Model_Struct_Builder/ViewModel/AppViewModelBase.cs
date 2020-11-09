using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 用于快速访问Controller
    /// </summary>
    public class AppViewModelBase : ViewModelBase
    {
        public AppController App
        {
            get { return AppController.GetInstence(); }
        }

        public FrameController Frame
        {
            get { return FrameController.GetInstence(); }
        }
    }
}

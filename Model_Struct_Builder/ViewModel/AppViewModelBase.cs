using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
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

        public Dictionary<string, string> AppDataText
        {
            get { return AppController.GetInstence().AppDataText; }
        }

        public Dictionary<string, string> FrameDataText
        {
            get { return FrameController.GetInstence().FrameDataText; }
        }
    }
}

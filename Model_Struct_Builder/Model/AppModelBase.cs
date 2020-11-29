using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Model_Struct_Builder
{
    public class AppModelBase : ObservableObject
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model_Struct_Builder;

namespace BasicLib
{
    interface iPanelFeatureProperty : iPanelFeature
    {
        void CreatePropertyFeature(FrameworkElement view, string viewName,string groupName);
    }
}

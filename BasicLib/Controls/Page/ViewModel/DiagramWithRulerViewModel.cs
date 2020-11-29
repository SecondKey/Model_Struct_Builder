using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace BasicLib
{
    class DiagramWithRulerViewModel : DiagramViewModelBase
    {
        public DiagramWithRulerViewModel(string parameter) : base(parameter)
        {
            viewModelName = parameter;
        }

    }
}

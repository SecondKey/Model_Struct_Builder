using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Struct_Builder;

namespace BasicLib
{
    class StandardDiagramViewModel : DiagramViewModelBase
    {
        public StandardDiagramViewModel(string parameter) : base(parameter)
        {
            viewModelName = parameter;
        }
    }
}

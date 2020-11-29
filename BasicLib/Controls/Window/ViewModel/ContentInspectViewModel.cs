using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Struct_Builder;

namespace BasicLib
{
    class ContentInspectViewModel : AppViewModelBase
    {
        public ContentInspectViewModel(string parameter)
        {
            viewModelName = parameter;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    public interface iFrameElement
    {
        int SerialNum { get; set; }
        string ElementName { get; set; }
        Dictionary<string, iElementProperty> ElementProperty { get; }

        void SaveElement(params string[] parameters);
        void LoadElement(params string[] parameters);
    }
}

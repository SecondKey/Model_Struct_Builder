using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    public interface iElementProperty
    {
        void LoadValue(params string[] parameters);
        void SaveValue(params string[] parameters);
    }
}

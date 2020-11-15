using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    public class RWXml : RXml, iWData
    {
        public RWXml(string path, string name) : base(path, name)
        {
        }

        public void AddContent(params string[] parameters)
        {

        }

        public void AddProperty(params string[] parameters)
        {

        }

        public void SetContent(params string[] paramters)
        {

        }

        public void SetProperty(params string[] parameters)
        {

        }
    }
}

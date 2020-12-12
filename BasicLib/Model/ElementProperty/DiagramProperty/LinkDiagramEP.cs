using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib.Model.ElementProperty.DiagramProperty
{
    class LinkDiagramEP : iElementProperty
    {
        public Dictionary<string, iLinkModel> allLink = new Dictionary<string, iLinkModel>();

        public void LoadValue(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SaveValue(params string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}

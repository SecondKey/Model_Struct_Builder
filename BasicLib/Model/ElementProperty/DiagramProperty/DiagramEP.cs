using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Struct_Builder;

namespace BasicLib
{
    public class DiagramEP : iElementProperty
    {
        public Dictionary<string, iNodeModel> allNode = new Dictionary<string, iNodeModel>();
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

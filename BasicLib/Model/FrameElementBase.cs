using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib.Model
{
    class FrameElementBase : iFrameElement
    {
        #region iFrameElement members
        public int SerialNum { get; set; }
        public string ElementName { get; set; }

        public Dictionary<string, iElementProperty> ElementProperty => throw new NotImplementedException();

        public void LoadElement(params string[] parameters)
        {
            throw new NotImplementedException();
        }

        public void SaveElement(params string[] parameters)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

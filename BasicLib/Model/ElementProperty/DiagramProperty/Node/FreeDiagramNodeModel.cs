using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    /// <summary>
    /// 自由图表节点，在图表中位置任意
    /// </summary>
    public class FreeDiagramNodeModel : NodeModelBase, iReferenceNode, iPositionNode
    {
        public FreeDiagramNodeModel(string nodeName, string type) : base(nodeName, type)
        {

        }

        #region iReferenceNode Members
        public bool IsReference { get { return ReferenceElement != null; } }
        public iFrameElement ReferenceElement { get; set; }
        #endregion

        #region iPositionNode Members
        private float posX;
        private float posY;

        public object GetPosition()
        {
            return new float[] { posX, posY };
        }

        public void SetPosition(object pos)
        {
            posX = (pos as float[])[0];
            posY = (pos as float[])[1];
        }
        #endregion
    }
}

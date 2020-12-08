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
    public class FreeDiagramNode : NodeModelBase, iReferenceNode, iPositionNode
    {
        public FreeDiagramNode(string name, float posX, float posY) : base(name)
        {
            this.posX = posX;
            this.posY = posY;
        }

        public FreeDiagramNode(string name, float posX, float posY, iFrameElement element) : base(name)
        {
            this.posX = posX;
            this.posY = posY;
            IsReference = true;
            ReferenceElement = element;
        }

        #region iReferenceNode Members
        public bool IsReference { get; }
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

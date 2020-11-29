using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_Struct_Builder;

namespace BasicLib
{
    class LinkModelBase : AppModelBase
    {
        /// <summary>
        /// 原节点
        /// </summary>
        [Browsable(false)]
        public NodeModelBase Source { get; private set; }
        /// <summary>
        /// 目标节点
        /// </summary>
        [Browsable(false)]
        public NodeModelBase Target { get; private set; }

        /// <summary>
        /// 连接的信息
        /// </summary>
        private string _text;
        /// <summary>
        /// 连接的信息
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public LinkModelBase(NodeModelBase source, NodeModelBase target)
        {
            Source = source;
            Target = target;
        }
    }
}

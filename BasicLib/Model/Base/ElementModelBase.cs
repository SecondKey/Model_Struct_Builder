using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using Model_Struct_Builder;

namespace BasicLib
{
    public enum NodeType
    {
        CommonNode,
    }

    public enum NodeStyleType
    {
        Color,
        Geometry
    }

    public enum NodeParameterType
    {

    }
    public abstract class NodeModelBase : AppModelBase, iPackageElement
    {
        public NodeModelBase(string parameter)
        {
            ElementName = parameter;
        }


        string elementName;

        public string ElementName
        {
            get { return elementName; }
            set
            {
                elementName = value;
            }
        }

        /// <summary>
        /// 所在列
        /// </summary>
        private int _column;
        /// <summary>
        /// 所在列
        /// </summary>
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                RaisePropertyChanged(() => Column);
            }
        }
        /// <summary>
        /// 所在行
        /// </summary>
        private int _row;
        /// <summary>
        /// 所在行
        /// </summary>
        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                RaisePropertyChanged(() => Row);
            }
        }

    }
}

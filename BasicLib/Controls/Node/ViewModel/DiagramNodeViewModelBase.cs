using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BasicLib
{
    abstract class DiagramNodeViewModelBase : NodeViewModelBase
    {
        public DiagramNodeViewModelBase(string parameter) : base(parameter)
        {

        }

        public abstract override FrameworkElement GetCommonNode();
    }
}

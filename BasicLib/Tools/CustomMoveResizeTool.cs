using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLib
{
    class CustomMoveResizeTool 
        //: MoveResizeTool
    {
        //private PageModelBase _model;

        //public CustomMoveResizeTool(DiagramView view, PageModelBase model)
        //    : base(view)
        //{
        //    _model = model;
        //}

        //public override bool CanDrop()
        //{
        //    foreach (var item in DragItems)
        //    {
        //        var column = (int)(item.Bounds.X / View.GridCellSize.Width);
        //        var row = (int)(item.Bounds.Y / View.GridCellSize.Height);
        //        if (_model.Nodes.Where(p => !IsDragged(p) && p.Row == row && p.Column == column).Count() != 0)
        //            return false;
        //    }
        //    return true;
        //}

        //private bool IsDragged(NodeModelBase node)
        //{
        //    return DragItems.Where(p => p.ModelElement == node).Count() > 0;
        //}

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace BasicLib
{
    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class NodeBase : DiagramItem, iNode
    {
        static NodeBase()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NodeBase), new FrameworkPropertyMetadata(typeof(NodeBase)));
        }


        #region Position
        /// <summary>
        /// 返回一个节点的范围
        /// </summary>
        public override Rect Bounds
        {
            get
            {
                var x = Canvas.GetLeft(this);
                var y = Canvas.GetTop(this);
                return new Rect(x, y, ActualWidth, ActualHeight);
            }
        }
        #endregion

        #region INode Members
        public Dictionary<string, object> nodePropertys
        {
            get;
            set;
        }
        #endregion

        #region Content Property
        ///// <summary>
        ///// 节点内容
        ///// </summary>
        //public static readonly DependencyProperty ContentProperty =
        //    DependencyProperty.Register("Content",
        //                               typeof(object),
        //                               typeof(Node));
        ///// <summary>
        ///// 节点内容
        ///// </summary>
        //public object Content
        //{
        //    get { return (bool)GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}
        #endregion

        #region Old
        ///// <summary>
        ///// 更新位置
        ///// </summary>
        //public void UpdatePosition()
        //{
        //    foreach (var p in Ports)
        //        p.UpdatePosition();
        //}
        ///// <summary>
        ///// 所有的端口
        ///// </summary>
        //private List<IPort> _ports = new List<IPort>();
        ///// <summary>
        ///// 所有的端口
        ///// </summary>
        //public List<IPort> Ports
        //{
        //    get { return _ports; }
        //}
        #endregion
    }
}

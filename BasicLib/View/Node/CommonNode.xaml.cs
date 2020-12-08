using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicLib
{
    /// <summary>
    /// CommonNode.xaml 的交互逻辑
    /// </summary>
    public partial class CommonNode : DiagramItem
    {
        public CommonNode()
        {
            InitializeComponent();
        }
        static CommonNode()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
        }

        //#region Content Property
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
        //#endregion

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

        #region DiagramItemMember
        /// <summary>
        /// 创建一个选择装饰器
        /// </summary>
        /// <returns></returns>
        protected override Adorner CreateSelectionAdorner()
        {
            return new SelectionAdorner(this, new SelectionFrame());
        }
        #endregion

        #region INode Members
        public Dictionary<string, object> nodePropertys
        {
            get;
            set;
        }
        #endregion

        #region iPortNode Members
        /// <summary>
        /// 所有的端口
        /// </summary>
        private List<IPort> _ports = new List<IPort>();
        /// <summary>
        /// 所有的端口
        /// </summary>
        public IEnumerable<IPort> Ports
        {
            get { return _ports; }
        }

        /// <summary>
        /// 更新位置
        /// </summary>
        public void UpdatePosition()
        {
            foreach (var p in Ports)
                p.UpdatePosition();
        }
        #endregion
    }
}

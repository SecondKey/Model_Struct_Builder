using Model_Struct_Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BasicLib
{
    abstract class DiagramController : iDiagramController
    {
        private DiagramView _view;
        private PageModelBase _model;

        public DiagramController(DiagramView view, PageModelBase model)
        {
            _view = view;
            _model = model;
            _model.Nodes.CollectionChanged += NodesCollectionChanged;
            _model.Links.CollectionChanged += LinksCollectionChanged;
            _updateScope = new UpdateScope(this);

            foreach (var t in _model.Nodes)
                t.PropertyChanged += NodePropertyChanged;

            UpdateView();
        }

        #region 图的操作

        /// <summary>
        /// 在视图层面创建一个连接线
        /// </summary>
        /// <param name="link">一个连接的实例</param>
        /// <returns></returns>
        protected abstract Control CreateLink(LinkModelBase link);


        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract Node UpdateNode(NodeModelBase node, Node item);

        /// <summary>
        /// 删除当前选择的节点或连接
        /// </summary>
        private void DeleteSelection()
        {
            using (BeginUpdate())
            {
                var nodes = _view.Selection.Select(p => p.ModelElement as NodeModelBase).Where(p => p != null);
                var links = _view.Selection.Select(p => p.ModelElement as LinkModelBase).Where(p => p != null);
                _model.Nodes.RemoveRange(p => nodes.Contains(p));
                _model.Links.RemoveRange(p => links.Contains(p));
                _model.Links.RemoveRange(p => nodes.Contains(p.Source) || nodes.Contains(p.Target));
            }
        }

        #endregion 

        #region 图的更新
        private UpdateScope _updateScope;

        public Dictionary<DiagramElementType, List<DiagramItem>> diagramItems { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private class UpdateScope : IDisposable
        {
            private DiagramController _parent;
            public bool IsInProgress { get; set; }

            public UpdateScope(DiagramController parent)
            {
                _parent = parent;
            }

            public void Dispose()
            {
                IsInProgress = false;
                _parent.UpdateView();
            }
        }
        /// <summary>
        /// 开始更新
        /// </summary>
        /// <returns></returns>
        private IDisposable BeginUpdate()
        {
            _updateScope.IsInProgress = true;
            return _updateScope;
        }
        /// <summary>
        /// 更新视图
        /// </summary>
        private void UpdateView()
        {
            if (!_updateScope.IsInProgress)
            {
                _view.Children.Clear();

                foreach (var node in _model.Nodes)
                    _view.Children.Add(UpdateNode(node, null));

                foreach (var link in _model.Links)
                    _view.Children.Add(CreateLink(link));
            }
        }

        #endregion

        #region Callback
        /// <summary>
        /// 图中任意节点属性修改，包括位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NodePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var fn = sender as NodeModelBase;
            var n = _view.Children.OfType<Node>().FirstOrDefault(p => p.ModelElement == fn);
            if (fn != null && n != null)
                UpdateNode(fn, n);
        }

        /// <summary>
        /// 节点集合修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void NodesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (var t in e.NewItems.OfType<INotifyPropertyChanged>())
                    t.PropertyChanged += NodePropertyChanged;

            if (e.OldItems != null)
                foreach (var t in e.OldItems.OfType<INotifyPropertyChanged>())
                    t.PropertyChanged -= NodePropertyChanged;
            UpdateView();
        }

        /// <summary>
        /// 连接集合修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void LinksCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateView();
        }
        #endregion

        #region 实现IDiagramController接口函数 Members
        public void UpdateItemsBounds(DiagramItem[] items, Rect[] bounds)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var node = items[i].ModelElement as NodeModelBase;
                if (node != null)
                {
                    node.Column = (int)(bounds[i].X / _view.GridCellSize.Width);
                    node.Row = (int)(bounds[i].Y / _view.GridCellSize.Height);
                }
            }
        }

        public void UpdateLink(LinkInfo initialState, ILink link)
        {
            using (BeginUpdate())
            {
                var sourcePort = link.Source as PortBase;
                var source = VisualHelper.FindParent<Node>(sourcePort);
                var targetPort = link.Target as PortBase;
                var target = VisualHelper.FindParent<Node>(targetPort);

                _model.Links.Remove((link as LinkBase).ModelElement as LinkModelBase);
                _model.Links.Add(new LinkModelBase((NodeModelBase)source.ModelElement, (NodeModelBase)target.ModelElement));
            }
        }

        public virtual void ExecuteCommand(System.Windows.Input.ICommand command, object parameter)
        {
            if (command == ApplicationCommands.Delete)
                DeleteSelection();
        }

        public virtual bool CanExecuteCommand(System.Windows.Input.ICommand command, object parameter)
        {
            if (command == ApplicationCommands.Delete)
                return true;
            else
                return false;
        }
        #endregion
    }
}

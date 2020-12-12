using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows;

namespace BasicLib
{
	/// <summary>
	/// 选择器
	/// </summary>
	public class Selection : INotifyPropertyChanged, IEnumerable<DiagramItem>
	{
		internal Selection()
		{
		}

		/// <summary>
		/// 当前选择的节点
		/// </summary>
		private DiagramItem _primary;
		public DiagramItem Primary
		{
			get { return _primary; }
		}
		
		/// <summary>
		/// 所有选中的节点（多选，框选节点）
		/// </summary>
		private Dictionary<DiagramItem, object> _items = new Dictionary<DiagramItem, object>();
		public IEnumerable<DiagramItem> Items
		{
			get { return _items.Keys; }
		}
		/// <summary>
		/// 节点总数
		/// </summary>
		public int Count
		{
			get { return _items.Count; }
		}
		/// <summary>
		/// 是否包含某个节点
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(DiagramItem item)
		{
			return _items.ContainsKey(item);
		}
		/// <summary>
		/// 添加一个节点
		/// </summary>
		/// <param name="item"></param>
		public void Add(DiagramItem item)
		{
			if (!_items.ContainsKey(item))
			{
				bool isPrimary = Count == 0;
				_items.Add(item, null);
				//item.IsSelected = true;
				//item.IsPrimarySelection = isPrimary;
				if (isPrimary)
				{
					_primary = item;
					OnPropertyChanged("Primary");
				}
				OnPropertyChanged("Items");
			}
		}
		/// <summary>
		/// 移除一个节点
		/// </summary>
		/// <param name="item"></param>
		public void Remove(DiagramItem item)
		{
			if (_items.ContainsKey(item))
			{
				//item.IsSelected = false;
				_items.Remove(item);
			}
			if (_primary == item)
			{
				_primary = _items.Keys.FirstOrDefault();
				if (_primary != null)
					//_primary.IsPrimarySelection = true;
				OnPropertyChanged("Primary");
			}
			OnPropertyChanged("Items");
		}

		/// <summary>
		/// 单击选中事件，只选中一个
		/// </summary>
		/// <param name="item"></param>
		public void Set(DiagramItem item)
		{
			SetRange(new DiagramItem[] { item });
		}

		/// <summary>
		/// 选中事件，可以选中一个或多个
		/// </summary>
		/// <param name="items"></param>
		public void SetRange(IEnumerable<DiagramItem> items)
		{
			DoClear();//清除所有
			bool isPrimary = true;//第一个节点是主要节点
			foreach (var item in items)
			{
				_items.Add(item, null);//将节点添加到字典中
				//item.IsSelected = true;//设为被选中
				if (isPrimary)//是否为主节点
				{
					_primary = item;
					//item.IsPrimarySelection = true;
					isPrimary = false;
				}
			}
			OnPropertyChanged("Primary");
			OnPropertyChanged("Items");
		}

		/// <summary>
		/// 清除所有选择（外部调用），并进行通知
		/// </summary>
		public void Clear()
		{
			DoClear();
			OnPropertyChanged("Primary");
			OnPropertyChanged("Items");
		}

		/// <summary>
		/// 清除所有选择（内部方法）
		/// </summary>
		private void DoClear()
		{
			foreach (var item in Items)
				//item.IsSelected = false;
			_items.Clear();
			_primary = null;
		}

        #region 接口成员函数
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		#endregion

		#region IEnumerable<object> Members

		IEnumerator<DiagramItem> IEnumerable<DiagramItem>.GetEnumerator()
		{
			return Items.GetEnumerator();
		}

		#endregion

		#endregion
	}
}

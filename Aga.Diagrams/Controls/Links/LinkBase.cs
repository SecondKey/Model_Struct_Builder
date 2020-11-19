using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Aga.Diagrams.Adorners;
using System.Windows.Documents;

namespace Aga.Diagrams.Controls
{
	public abstract class LinkBase : DiagramItem, ILink, INotifyPropertyChanged
	{
		#region Properties

		#region CanRelink Property是否可以重新连接
		/// <summary>
		/// 是否可以重新连接
		/// </summary>
		public static readonly DependencyProperty CanRelinkProperty =
			DependencyProperty.Register("CanRelink",
							   typeof(bool),
							   typeof(LinkBase),
							   new FrameworkPropertyMetadata(true));
		/// <summary>
		/// 是否可以重新连接
		/// </summary>
		public bool CanRelink
		{
			get { return (bool)GetValue(CanRelinkProperty); }
			set { SetValue(CanRelinkProperty, value); }
		}
		#endregion
		/// <summary>
		/// 原端口
		/// </summary>
		private IPort _source;
		/// <summary>
		/// 原端口
		/// </summary>
		public IPort Source
		{
			get { return _source; }
			set
			{
				if (_source != null)
					_source.Links.Remove(this);
				_source = value;
				if (_source != null)
					_source.Links.Add(this);
			}
		}

		/// <summary>
		/// 目标端口
		/// </summary>
		private IPort _target;
		/// <summary>
		/// 目标端口
		/// </summary>
		public IPort Target
		{
			get { return _target; }
			set
			{
				if (_target != null)
					_target.Links.Remove(this);
				_target = value;
				if (_target != null)
					_target.Links.Add(this);
			}
		}

		/// <summary>
		/// 原端口位置
		/// </summary>
		public Point? SourcePoint { get; set; }
		/// <summary>
		/// 目标位置
		/// </summary>
		public Point? TargetPoint { get; set; }

        #region Cap
        private bool _startCap;
		public bool StartCap
		{
			get { return _startCap; }
			set
			{
				_startCap = value;
				OnPropertyChanged("StartCap");
			}
		}

		private bool _endCap;
		public bool EndCap
		{
			get { return _endCap; }
			set
			{
				_endCap = value;
				OnPropertyChanged("EndCap");
			}
		}
        #endregion

		/// <summary>
		/// 连接的画刷
		/// </summary>
        private Brush _brush = new SolidColorBrush(Colors.Black);
		/// <summary>
		/// 连接的画刷
		/// </summary>
		public Brush Brush
		{
			get { return _brush; }
			set { _brush = value; }
		}

		/// <summary>
		/// 开始位置
		/// </summary>
		private Point _startPoint;
		/// <summary>
		/// 开始位置
		/// </summary>
		public Point StartPoint
		{
			get { return _startPoint; }
			protected set
			{
				_startPoint = value;
				OnPropertyChanged("StartPoint");
			}
		}

		/// <summary>
		/// 结束位置
		/// </summary>
		private Point _endPoint;
		/// <summary>
		/// 结束位置
		/// </summary>
		public Point EndPoint
		{
			get { return _endPoint; }
			protected set
			{
				_endPoint = value;
				OnPropertyChanged("EndPoint");
			}
		}


		private double _startCapAngle;
		public double StartCapAngle
		{
			get { return _startCapAngle; }
			protected set
			{
				_startCapAngle = value;
				OnPropertyChanged("StartCapAngle");
			}
		}

		private double _endCapAngle;
		public double EndCapAngle
		{
			get { return _endCapAngle; }
			protected set
			{
				_endCapAngle = value;
				OnPropertyChanged("EndCapAngle");
			}
		}

		/// <summary>
		/// 路径图形
		/// </summary>
		private PathGeometry _pathGeomtry;
		/// <summary>
		/// 路径图形
		/// </summary>
		public PathGeometry PathGeometry
		{
			get { return _pathGeomtry; }
			protected set
			{
				_pathGeomtry = value;
				OnPropertyChanged("PathGeometry");
			}
		}

		#region Label Property连接标签

		/// <summary>
		/// 连接标签的位置
		/// </summary>
		private Point _labelPosition;
		/// <summary>
		/// 连接标签的位置
		/// </summary>
		public Point LabelPosition
		{
			get { return _labelPosition; }
			set
			{
				_labelPosition = value;
				OnPropertyChanged("LabelPosition");
			}
		}
		public static readonly DependencyProperty LabelProperty =
			DependencyProperty.Register("Label", typeof(string), typeof(LinkBase));

		/// <summary>
		/// 标签文本
		/// </summary>
		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		#endregion

		/// <summary>
		/// 获取从开始点到结束点的方形范围
		/// </summary>
		public override Rect Bounds
		{
			get
			{
				var x = Math.Min(StartPoint.X, EndPoint.X);
				var y = Math.Min(StartPoint.Y, EndPoint.Y);
				var mx = Math.Max(StartPoint.X, EndPoint.X);
				var my = Math.Max(StartPoint.Y, EndPoint.Y);
				return new Rect(x, y, mx - x, my - y);
			}
		}

		#endregion

		protected LinkBase()
		{
			UpdatePath();
		}

		/// <summary>
		/// 创建选择装饰器
		/// </summary>
		/// <returns></returns>
		protected override Adorner CreateSelectionAdorner()
		{
			return new SelectionAdorner(this, new RelinkControl());
		}

		public abstract void UpdatePath();

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, new PropertyChangedEventArgs(name));
		}
		#endregion
	}
}

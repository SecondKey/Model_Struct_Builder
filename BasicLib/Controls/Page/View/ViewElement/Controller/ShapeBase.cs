using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BasicLib
{
    /// <summary>
    /// 形状基类
    /// </summary>
    class ShapeBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 形状的位置
        /// </summary>
        private Point _location;
        public Point Location
        {
            get { return _location; }
            set
            {
                _location = value;
                OnPropertyChanged("Location");
            }
        }

        /// <summary>
        /// 形状大小
        /// </summary>
        private Size _size;
        public Size Size
        {
            get { return _size; }
            set
            {
                _size = value;
                OnPropertyChanged("Size");
            }
        }

        /// <summary>
        /// 形状的连接
        /// </summary>
        private List<ShapeBase> _links = new List<ShapeBase>();
        [Browsable(false)]
        public List<ShapeBase> Links
        {
            get { return _links; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }

    class RectangleShape : ShapeBase
    {
    }

    class EllipseShape : ShapeBase
    {
    }
}



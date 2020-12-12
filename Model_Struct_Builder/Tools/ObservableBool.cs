using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    public class ObservableBool : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableBool(bool b)
        {
            property = b;
        }

        private bool property;
        public bool Property
        {
            get
            {
                return property;
            }
            set
            {
                property = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Property"));
                }
            }
        }
    }
}

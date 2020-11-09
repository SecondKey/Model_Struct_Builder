using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 允许发送set消息的属性
    /// </summary>
    /// <typeparam name="T">参数的类型</typeparam>
    public class MsgProperty<T>
    {
        AllAppMsg msg;
        public MsgProperty(AllAppMsg msg)
        {
            this.msg = msg;
        }

        public MsgProperty(AllAppMsg msg, T property)
        {
            this.msg = msg;
            this.property = property;
        }

        T property;
        public T SenderProperty
        {
            get { return property; }
            set
            {
                property = value;
                MsgCenter.SendMsg(new MsgVar<T>(msg, property));
            }
        }
        public T Property
        {
            get { return property; }
            set
            {
                property = value;
            }
        }
    }

    /// <summary>
    /// 允许发送set消息的key，value属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Y"></typeparam>
    public class MsgKVProperty<T, Y> : ObservableObject
    {
        AllAppMsg msg;
        public MsgKVProperty(AllAppMsg msg)
        {
            this.msg = msg;
        }

        public MsgKVProperty(AllAppMsg msg, T p1, Y p2)
        {
            this.msg = msg;
            this.p1 = p1;
            this.p2 = p2;
        }

        T p1;
        public T SenderP1Property
        {
            get { return p1; }
            set
            {
                p1 = value;
                RaisePropertyChanged(() => SenderP1Property);
                MsgCenter.SendMsg(new MsgVar<KeyValuePair<T, Y>>(msg, new KeyValuePair<T, Y>(p1, p2)));
            }
        }
        public T P1Property
        {
            get { return p1; }
            set
            {
                p1 = value;
                RaisePropertyChanged(() => SenderP1Property);
            }
        }

        Y p2;
        public Y SenderP2Property
        {
            get { return p2; }
            set
            {
                p2 = value;
                RaisePropertyChanged(() => SenderP2Property);
                MsgCenter.SendMsg(new MsgVar<KeyValuePair<T, Y>>(msg, new KeyValuePair<T, Y>(p1, p2)));
            }
        }
        public Y P2Property
        {
            get { return p2; }
            set
            {
                p2 = value;
                RaisePropertyChanged(() => SenderP2Property);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using Model_Struct_Builder;

namespace BasicLib
{
    public enum AllPackageMsg
    {
        AcceptDrop,//一个AcceptDropFeature接受了一个拖放操作 DiagramView 页面,DragDropInfomation 拖拽信息
    }

    public static class PackageMsgCenter
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public static void SendMsg(PackageMsgBase tmp)
        {
            Messenger.Default.Send(tmp, tmp.msg);
        }

        /// <summary>
        /// 注册一个单独的消息到一个单独的操作
        /// </summary>
        /// <param name="target">"this"</param>
        /// <param name="msg">对应接收的消息</param>
        /// <param name="action">受到消息后执行方法</param>
        public static void RegistSelf(this object target, AllPackageMsg msg, Action<PackageMsgBase> action)
        {
            Messenger.Default.Register(target, msg, action);
        }

        /// <summary>
        /// 批量注册多个消息和操作
        /// </summary>
        /// <param name="target">"this"</param>
        /// <param name="list">消息列表，以及每个消息对应的操作列表</param>
        public static void RegistSelf(this object target, Dictionary<AllPackageMsg, Action<PackageMsgBase>[]> list)
        {
            foreach (var kv in list)
            {
                foreach (var kv1 in kv.Value)
                {
                    Messenger.Default.Register(target, kv.Key, kv1);
                }
            }
        }

        /// <summary>
        /// 注销一个消息
        /// </summary>
        /// <param name="target">"this"</param>
        /// <param name="msg">消息</param>
        public static void UnRegistSelf(this object target, AllPackageMsg msg)
        {
            Messenger.Default.Unregister<PackageMsgBase>(target, msg);
        }
    }

    public class PackageMsgBase
    {
        public AllPackageMsg msg;

        public PackageMsgBase(AllPackageMsg msg)
        {
            this.msg = msg;
        }
    }

    /// <summary>
    /// 带有一个参数的消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PackageMsgVar<T> : PackageMsgBase
    {
        public T parameter;

        public PackageMsgVar(AllPackageMsg msg, T parameter) : base(msg)
        {
            this.parameter = parameter;
        }
    }

    /// <summary>
    /// 带有两个参数的消息
    /// </summary>
    public class PackageMsgVarKv<T, Y> : PackageMsgVar<T>
    {
        public Y p1;

        public PackageMsgVarKv(AllPackageMsg msg, T parameter, Y p1) : base(msg, parameter)
        {
            this.parameter = parameter;
            this.p1 = p1;
        }
    }
}

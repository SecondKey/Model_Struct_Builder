using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{

    #region Msg
    public enum AllAppMsg
    {
        #region Debug
        ShowDebugText,
        #endregion

        #region App
        LoadApp,//加载应用程序数据
        AppLoadComplete,//加载应用程序数据完成

        ChangeLanguage,//App切换了语言

        #endregion

        #region Frame
        #region Layout
        ShowHideWindow,//显示或隐藏窗口

        SaveLayout,//保存布局，string 布局文件夹完整路径
        LoadLayout,//读取布局，string 布局文件夹完整路径
        #endregion

        LoadFrame,//加载以一个框架 string 框架名称
        FrameLoadComplete,//框架加载完成
        PanelStructLoadComplete,//页面结构加载完成
        PanelLoadConplete,//页面加载完成

        PanelCreateOver,//页面创建完成
        #endregion

        #region Project

        CreateProject,
        LoadProject,
        ProjectLoadComplete,
        #endregion

        #region ElementOperation
        DeleteOptions,

        OnElementValueChange,
        #endregion

        #region UserControl
        ChoiseEveneElement,
        Undo,
        Redo
        #endregion
    }
    #endregion

    /// <summary>
    /// 消息中心
    /// </summary>
    class MsgCenter
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        public static void SendMsg(MsgBase tmp)
        {
            Messenger.Default.Send<MsgBase>(tmp, tmp.msg);
        }

        /// <summary>
        /// 注册一个单独的消息到一个单独的操作
        /// </summary>
        /// <param name="target">"this"</param>
        /// <param name="msg">对应接收的消息</param>
        /// <param name="action">受到消息后执行方法</param>
        public static void RegistSelf(object target, AllAppMsg msg, Action<MsgBase> action)
        {
            Messenger.Default.Register(target, msg, action);
        }

        /// <summary>
        /// 批量注册多个消息和操作
        /// </summary>
        /// <param name="target">"this"</param>
        /// <param name="list">消息列表，以及每个消息对应的操作列表</param>
        public static void RegistSelf(object target, Dictionary<AllAppMsg, Action<MsgBase>[]> list)
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
        public static void UnRegistSelf(object target, AllAppMsg msg)
        {
            Messenger.Default.Unregister<MsgBase>(target, msg);
        }
    }

    /// <summary>
    /// 空消息
    /// </summary>
    public class MsgBase
    {
        public AllAppMsg msg;

        public MsgBase(AllAppMsg msg)
        {
            this.msg = msg;
        }
    }

    /// <summary>
    /// 带有一个参数的消息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsgVar<T> : MsgBase
    {
        public T parameter;

        public MsgVar(AllAppMsg msg, T parameter) : base(msg)
        {
            this.parameter = parameter;
        }
    }

    /// <summary>
    /// 带有两个参数的消息
    /// </summary>
    public class msgVarKv<T, Y> : MsgBase
    {
        public T p1;
        public Y p2;

        public msgVarKv(AllAppMsg msg, T p1, Y p2) : base(msg)
        {
            this.p1 = p1;
            this.p2 = p2;
        }
    }
}

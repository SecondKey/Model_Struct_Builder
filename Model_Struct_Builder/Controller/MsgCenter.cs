using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
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
        LoadFrame,//加载以一个框架 string 框架名称
        FrameLoadComplete,//框架加载完成

        ShowHideWindow,//显示或隐藏窗口

        PanelStructLoadComplete,//页面结构加载完成
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


    class MsgCenter
    {
        public static void SendMsg(MsgBase tmp)
        {
            Messenger.Default.Send<MsgBase>(tmp, tmp.msg);
        }

        public static void RegistSelf(object target, AllAppMsg msg, Action<MsgBase> action)
        {
            Messenger.Default.Register(target, msg, action);
        }

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
    }

    public class MsgBase
    {
        public AllAppMsg msg;

        public MsgBase(AllAppMsg msg)
        {
            this.msg = msg;
        }
    }

    public class MsgString : MsgBase
    {
        public string parameter;

        public MsgString(AllAppMsg msg, string parameter) : base(msg)
        {
            this.parameter = parameter;
        }
    }

    public class MsgVar<T> : MsgBase
    {
        public T parameter;

        public MsgVar(AllAppMsg msg, T parameter) : base(msg)
        {
            this.parameter = parameter;
        }
    }

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

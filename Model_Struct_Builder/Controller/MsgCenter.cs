using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model_Struct_Builder
{

    #region Msg
    public enum AllAppMsg
    {
        #region Debug
        ShowDebugText,
        #endregion

        #region App

        #region Load
        LoadApp,//加载应用程序数据
        AppLoadComplete,//加载应用程序数据完成
        #endregion

        #region Settings
        ChangeLanguage,//App切换了语言
        #endregion

        #region ToolsAndOthers
        TestVMTVMsg,
        ViewModelToView,//viewModel通过消息控制view执行方法，详见MsgCenter_VMTV
        #endregion 
        #endregion

        #region Frame
        #region Layout
        AutoVisible,//自动显示或隐藏窗口
        SaveUserVisible,
        LoadUserVisible,
        ShowHideWindow,//显示或隐藏窗口

        SaveLayout,//保存布局，string 布局文件夹完整路径
        LoadLayout,//读取布局，string 布局文件夹完整路径
        #endregion

        LoadFrame,//加载以一个框架 string 框架名称
        FrameLoadComplete,//框架加载完成
        MenuLoadComplete,//菜单加载完成
        AllPanelStructLoadComplete,//页面结构加载完成

        //PanelCreateComplete,//一个页面加载完成
        //PanelLoadChild,//页面开始加载自己的内容
        //PanelChildLoadComplete,//一个页面加载完成 string，加载完成页面的名字


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

        #region VMTV
        public static void ViewRegistVMTV(object target, Dictionary<string, Action> actionDic)
        {
            Messenger.Default.Register(target, AllAppMsg.ViewModelToView, new Action<MsgBase>((m) =>
            {
                MsgVarKv<string, string> tmpMsg = (MsgVarKv<string, string>)m;
                FrameworkElement element = target as FrameworkElement;
                AppViewModelBase vm = element.DataContext as AppViewModelBase;
                if (tmpMsg.parameter == vm.viewModelName)
                {
                    actionDic[tmpMsg.p1].Invoke();
                }
            }), true);
        }
        #endregion
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
    public class MsgVarKv<T, Y> : MsgVar<T>
    {
        public Y p1;

        public MsgVarKv(AllAppMsg msg, T parameter, Y p1) : base(msg, parameter)
        {
            this.parameter = parameter;
            this.p1 = p1;
        }
    }
}

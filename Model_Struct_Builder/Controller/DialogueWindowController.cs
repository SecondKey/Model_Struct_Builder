using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 对话窗口的控制器，用于生成应用程序内部的所有对话窗口
    /// </summary>
    class DialogueWindowController
    {
        #region 单例
        private static DialogueWindowController instence;
        public DialogueWindowController() { }
        public static DialogueWindowController GetInstence()
        {
            if (instence == null)
            {
                instence = new DialogueWindowController();
            }
            return instence;
        }
        #endregion

        #region Layout
        /// <summary>
        /// 显示 保存布局 的对话窗口
        /// </summary>
        public static void ShowSaveLayoutWindow()
        {
            List<string> tmp = FileFolder.GetAllFileName(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout");//在布局文件夹中查找所有布局
            tmp.Remove("Common");//移除 默认布局，默认布局禁止用户修改
            tmp.Remove("Last");//移除 上次退出时的布局，该布局禁止用户修改
            ShowDialogue(new List<FormStruct>
            {
                new FormStruct
                {
                    name="布局：",
                    type=FormItemType.InputDropDown,
                    parameters=tmp,
                }
            }, AllAppMsg.SaveLayout);//显示对话框
        }
        /// <summary>
        /// 显示 加载布局 的对话窗口
        /// </summary>
        public static void ShowLoadLayoutWindow()
        {
            ShowDialogue(new List<FormStruct>
            {
                new FormStruct
                {
                    name="布局：",
                    type=FormItemType.DropDown,
                    parameters=FileFolder.GetAllFileName(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout")
                }
            }, AllAppMsg.LoadLayout);//显示对话框
        }
        #endregion

        #region Show
        /// <summary>
        /// 根据给定的表单信息和回调消息构建一个表单对话框
        /// </summary>
        /// <param name="formInfo">表单项的信息</param>
        /// <param name="callbackMsg">回调消息</param>
        public static void ShowDialogue(List<FormStruct> formInfo, AllAppMsg callbackMsg)
        {
            DialogueWindowViewModel vm = new DialogueWindowViewModel(formInfo, callbackMsg);
            DialogueWindow window = new DialogueWindow();
            window.DataContext = vm;
            window.ShowDialog();
        }
        /// <summary>
        /// 根据给定的表单信息和回调消息构建一个表单窗口
        /// </summary>
        /// <param name="formInfo">表单项的信息</param>
        /// <param name="callbackMsg">回调消息</param>
        public static void ShowWindow(List<FormStruct> formInfo, AllAppMsg callbackMsg)
        {
            DialogueWindowViewModel vm = new DialogueWindowViewModel(formInfo, callbackMsg);
            DialogueWindow window = new DialogueWindow();
            window.DataContext = vm;
            window.Show();
        }
        #endregion
    }
}

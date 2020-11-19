using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
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
        public static void ShowSaveLayoutWindow()
        {
            List<string> tmp = FileFolder.GetAllFileName(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout");
            tmp.Remove("Common");
            tmp.Remove("Last");
            DialogueWindowViewModel vm = new DialogueWindowViewModel(new List<FormStruct>
            {
                new FormStruct
                {
                    name="布局：",
                    type=FormItemType.InputDropDown,
                    parameters=tmp,
                }
            }, AllAppMsg.SaveLayout);
            DialogueWindow window = new DialogueWindow();
            window.DataContext = vm;
            window.ShowDialog();
        }

        public static void ShowLoadLayoutWindow()
        {
            DialogueWindowViewModel vm = new DialogueWindowViewModel(new List<FormStruct>
            {
                new FormStruct
                {
                    name="布局：",
                    type=FormItemType.DropDown,
                    parameters=FileFolder.GetAllFileName(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout")
                }
            }, AllAppMsg.LoadLayout);
            DialogueWindow window = new DialogueWindow();
            window.DataContext = vm;
            window.ShowDialog();
        }
        #endregion
    }
}

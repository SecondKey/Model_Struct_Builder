using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 空的 AvalonDock_MVVM 的 DockingManager
    /// </summary>
    public partial class EmptyDockingManager : DockingManager
    {
        public EmptyDockingManager()
        {
            InitializeComponent();
            MsgCenter.RegistSelf(this, AllAppMsg.SaveLayout, SaveLayout<MsgBase>);
            MsgCenter.RegistSelf(this, AllAppMsg.LoadLayout, LoadLayout<MsgBase>);
        }

        /// <summary>
        /// 保存当前DockingManager布局
        /// </summary>
        public void SaveLayout<T>(MsgBase msg)
        {
            DockingManagerViewModel m = this.DataContext as DockingManagerViewModel;//获取当前VM，用于获取name
            XmlLayoutSerializer serializer = new XmlLayoutSerializer(this);//创建序列化器
            serializer.Serialize(
                FileFolder.CreateFolder(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout", AppController.GetInstence().Layout) + m.viewModelName + ".xml");//根据路径储存布局
        }

        /// <summary>
        /// 加载当前DockingManager布局
        /// </summary>
        public void LoadLayout<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            DockingManagerViewModel m = this.DataContext as DockingManagerViewModel;//获取当前VM，用于获取name

            if (tmpMsg.parameter == m.viewModelName)
            {
                XmlLayoutSerializer serializer = new XmlLayoutSerializer(this);//创建序列化器
                serializer.Deserialize(FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout", AppController.GetInstence().Layout) + m.viewModelName + ".xml");
            }
        }

        #region OtherLoadSaveMethods
        //using (var stream = new StreamReader(
        //    FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout", tmpMsg.parameter) +
        //    m.name + ".xml"))//根据路径加载布局
        //{
        //    serializer.Deserialize(stream);
        //}

        //using (var stream = new StreamWriter(
        //    FileFolder.CreateFolder(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout", tmpMsg.parameter) +
        //    m.name + ".xml"))//根据路径创建StreamWriter
        //{
        //    serializer.Serialize(stream);//储存布局
        //}
        #endregion
    }
}

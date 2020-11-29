using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Model_Struct_Builder.RAD
{
    /// <summary>
    /// DLL包加载器
    /// </summary>
    class FramePackage
    {
        /// <summary>
        /// 指定的包
        /// </summary>
        Assembly targetDll;
        /// <summary>
        /// 包名，后有.dll
        /// </summary>
        string name;
        /// <summary>
        /// 该包的控制器
        /// </summary>
        public iPackageController Controller { get; set; }

        public FramePackage(string name)
        {
            this.name = name;
            if (File.Exists(AppController.GetInstence().appPath + "Package/" + name + ".dll"))
            {
                targetDll = Assembly.LoadFile(AppController.GetInstence().appPath + "Package/" + name + ".dll");
            }
            else if (File.Exists(AppController.GetInstence().appPath + "Frame/" + FrameController.GetInstence().frameName + "/Package/" + name + ".dll"))
            {
                targetDll = Assembly.LoadFile(AppController.GetInstence().appPath + "Frame/" + FrameController.GetInstence().frameName + "/Package/" + name + ".dll");
            }
            else
            {
                targetDll = Assembly.LoadFile("D:/OfficialProject/Model_Struct_Builder/BasicLib/bin/Debug/BasicLib.dll");
            }
        }

        /// <summary>
        /// 包中的一个组件
        /// </summary>
        /// <param name="elementName">组件名</param>
        public Control GetElement(string elementName)
        {
            Type t = targetDll.GetType(name + "." + elementName);//获取组件的类型
            return Activator.CreateInstance(t) as Control;//根据类型实例化对象
        }

        public AppViewModelBase GetElement(string elementName, string parameter)
        {
            Type t = targetDll.GetType(name + "." + elementName);//获取组件的类型
            return Activator.CreateInstance(t, parameter) as AppViewModelBase;//根据类型实例化对象
        }

        public iPackageController GetController(string elementName, string parameter)
        {
            Type t = targetDll.GetType(name + "." + elementName);//获取组件的类型
            return Activator.CreateInstance(t, parameter) as iPackageController;//根据类型实例化对象
        }
    }
}

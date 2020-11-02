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

        public FramePackage(string name)
        {
            this.name = name;
            if (Directory.Exists(AppController.GetInstence().appPath + "Package/" + name + ".dll"))
            {
                targetDll = Assembly.LoadFile(AppController.GetInstence().appPath + "Package/" + name + ".dll");
            }
            else if (Directory.Exists(AppController.GetInstence().appPath + "Frame/" + FrameController.GetInstence().frameName + "/Package/" + name + ".dll"))
            {
                targetDll = Assembly.LoadFile(AppController.GetInstence().appPath + "Frame/" + FrameController.GetInstence().frameName + "/Package/" + name + ".dll");
            }
            else
            {
                targetDll = Assembly.LoadFile("D:/OfficialProject/Model_Struct_Builder/BasicLib/bin/Debug/BasicLib.dll");
            }
        }

        public Control GetElement(string elementName)
        {
            Type[] allT = targetDll.GetTypes();
            foreach (Type test in allT)
            {
                Console.WriteLine(test.FullName);
            }

            Type t = targetDll.GetType(name + "." + elementName);
            return (Control)Activator.CreateInstance(t);
        }
    }
}

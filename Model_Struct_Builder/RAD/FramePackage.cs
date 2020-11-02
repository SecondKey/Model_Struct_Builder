using System;
using System.Collections.Generic;
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
        /// 包的完整名字
        /// </summary>
        string fullName;
        /// <summary>
        /// 包的路径
        /// </summary>
        string path;
        /// <summary>
        /// 包名，后有.dll
        /// </summary>
        string name;

        public FramePackage(string path, string name)
        {
            fullName = path + "/" + name;
            this.path = path;
            this.name = name;
            targetDll = Assembly.LoadFile(path + "/" + name);
        }

        public Control GetElement(string elementName)
        {
            Type t = targetDll.GetType(elementName);
            return (Control)Activator.CreateInstance(t);
        }
    }
}

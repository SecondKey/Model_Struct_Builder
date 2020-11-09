using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 处理文件和文件夹相关操作的工具类
    /// </summary>
    class FileFolder
    {
        /// <summary>
        /// 连接一些字符串成为地址
        /// </summary>
        public static string LinkPath(params string[] path)
        {
            string s = "";
            foreach (string t in path)
            {
                s += t + "/";
            }
            return s;
        }

        /// <summary>
        /// 连接字符串成为地址并创建对应的文件夹
        /// </summary>
        public static string CreateFolder(params string[] path)
        {
            string s = "";
            foreach (string t in path)
            {
                s += t + "/";
                if (!Directory.Exists(s))
                {
                    Directory.CreateDirectory(s);
                    Console.WriteLine(t);
                }
            }
            return s;
        }

        //public static string CreateFile(string fileName, params string[] path)
        //{
        //    string s = "";

        //    foreach (string t in path)
        //    {

        //    }
        //    return s;
        //}
    }
}

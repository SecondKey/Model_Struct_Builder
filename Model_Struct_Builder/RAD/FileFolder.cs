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
                }
            }
            return s;
        }
        public static string CreateFile(string fileName, params string[] path)
        {
            string s = "";
            foreach (string t in path)
            {
                s += t + "/";
                if (!Directory.Exists(s))
                {
                    Directory.CreateDirectory(s);
                }
            }
            s += fileName;
            if (!File.Exists(s))
            {
                File.Create(s);
            }
            return s;
        }

        public static bool HasFolder(params string[] path)
        {
            string s = "";
            foreach (string p in path)
            {
                s += p + "/";
            }
            return Directory.Exists(s);
        }

        public static bool HasFile(string fileName, params string[] path)
        {
            string s = "";
            foreach (string p in path)
            {
                s += p + "/";
            }
            s += fileName;
            Console.WriteLine(s);
            Console.WriteLine(File.Exists(s));
            return File.Exists(s);
        }

        public static List<string> GetAllFileName(params string[] path)
        {
            List<string> tmp = new List<string>();
            string s = "";
            foreach (string p in path)
            {
                s += p + "/";
            }
            DirectoryInfo root = new DirectoryInfo(s);
            foreach (FileInfo f in root.GetFiles())
            {
                tmp.Add(Path.GetFileNameWithoutExtension(f.Name));
            }
            return tmp;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    public class ToolsCenter
    {
        public static string FormattingString(string t)
        {
            return t.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
        }

        /// <summary>
        /// 将一个或一些数据连接到一个数组后面
        /// </summary>
        /// <typeparam name="T">数组的数据类型</typeparam>
        /// <param name="array">原始的数组</param>
        /// <param name="other">需要连接的数据</param>
        /// <returns></returns>
        public static T[] ConnectArray<T>(T[] array, params T[] other)
        {
            T[] tmp = new T[array.Length + other.Length];
            array.CopyTo(tmp, 0);
            other.CopyTo(tmp, array.Length);
            return tmp;
        }
    }
}

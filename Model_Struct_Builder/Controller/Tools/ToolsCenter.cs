using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 杂七杂八的工具类
    /// </summary>
    public static class ToolsCenter
    {
        /// <summary>
        /// 删除一个字符串中所有的制表符等
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string FormattingString(this string t)
        {
            return t.Replace("\n", "").Replace("\t", "").Replace("\r", "");
        }

        /// <summary>
        /// 将一个或一些数据连接到一个数组后面
        /// </summary>
        /// <typeparam name="T">数组的数据类型</typeparam>
        /// <param name="array">原始的数组</param>
        /// <param name="other">需要连接的数据</param>
        /// <returns></returns>
        public static T[] ConnectArray<T>(this T[] array, params T[] other)
        {
            T[] tmp = new T[array.Length + other.Length];
            array.CopyTo(tmp, 0);
            other.CopyTo(tmp, array.Length);
            return tmp;
        }

        public static void RemoveRange<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            var arr = source.Where(p => predicate(p)).ToArray();
            foreach (var t in arr)
                source.Remove(t);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
            return t.Replace("\n", "").Replace("\t", "").Replace("\r", "").Replace(" ", "").Replace("--", " ");
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

        /// <summary>
        /// 从ICollection中移除一个范围
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        public static void RemoveRange<T>(this ICollection<T> source, Func<T, bool> predicate)
        {
            var arr = source.Where(p => predicate(p)).ToArray();
            foreach (var t in arr)
                source.Remove(t);
        }

        /// <summary>
        /// 查找对应父物体的一个指定类型的子物体
        /// </summary>
        /// <typeparam name="T">查找的类型</typeparam>
        /// <param name="reference">父物体</param>
        /// <param name="self">是否检查父物体本身</param>
        /// <returns></returns>
        public static T FindVisibleChild<T>(this DependencyObject reference, bool self = true) where T : class
        {
            if (reference is T && self)
            {
                return reference as T;
            }
            T foundChild = default(T);
            if (reference != null)
            {
                int childrenCount = VisualTreeHelper.GetChildrenCount(reference);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(reference, i);
                    if (child is T)
                    {
                        foundChild = child as T;
                        break;
                    }
                    else
                    {
                        foundChild = FindChild<T>(child);
                    }
                }
            }
            return foundChild;
        }

        /// <summary>
        /// 查找对应父物体的一个指定类型的子物体
        /// </summary>
        /// <typeparam name="T">查找的类型</typeparam>
        /// <param name="reference">父物体</param>
        /// <param name="self">是否检查父物体本身</param>
        /// <returns></returns>
        public static T FindChild<T>(this object reference, bool self = true) where T : class
        {
            if (reference is T && self)
            {
                return reference as T;
            }
            T foundChild = default(T);
            if (reference != null && reference is DependencyObject)
            {
                foreach (var child in LogicalTreeHelper.GetChildren(reference as DependencyObject))
                {
                    if (child is T)
                    {
                        foundChild = child as T;
                        break;
                    }
                    else
                    {
                        foundChild = FindChild<T>(child);
                    }
                }
            }
            return foundChild;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 用于访问只读文件的接口
    /// </summary>
    public interface iRData
    {
        #region Data
        /// <summary>
        /// 获取一个元素的属性
        /// </summary>
        string GetProperty(params string[] parameters);

        /// <summary>
        /// 获取一个元素的内容
        /// </summary>
        string GetContent(params string[] parameters);

        /// <summary>
        /// 获取一个元素中所有的属性
        /// </summary>
        Dictionary<string, string> GetOneElementsAllProperty(params string[] parameters);

        /// <summary>
        /// 获取所有元素的内容
        /// </summary>
        Dictionary<string, string> GetAllElementContent(params string[] parameters);

        /// <summary>
        /// 获取一个元素中所有的内容
        /// </summary>
        List<string> GetOneElementsAllContent(params string[] parameters);

        /// <summary>
        /// 获取一个元素中所有元素的全部属性
        /// </summary>
        Dictionary<string, Dictionary<string, string>> GetDoubleLayerElements(params string[] parameters);

        #endregion

        #region Num
        /// <summary>
        /// 查询一个元素中包含多少属性
        /// </summary>
        int GetPropertyNum(params string[] parameters);

        /// <summary>
        /// 查询一个元素中包含多少内容
        /// </summary>
        int GetContentNum(params string[] parameters);
        #endregion

        #region Check
        /// <summary>
        /// 检查是否包含指定的结构序列
        /// </summary>
        bool HasElement(params string[] parameters);

        /// <summary>
        /// 检查一个元素是否包含指定的属性
        /// </summary>
        bool HasProperty(params string[] parameters);
        #endregion
    }
}

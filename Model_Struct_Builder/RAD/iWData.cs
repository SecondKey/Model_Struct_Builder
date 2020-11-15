using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    interface iWData
    {
        /// <summary>
        /// 为一个元素添加属性
        /// </summary>
        void AddProperty(params string[] parameters);
        /// <summary>
        /// 为一个元素添加内容
        /// </summary>
        void AddContent(params string[] parameters);
        /// <summary>
        /// 设置/修改一个元素的属性
        /// </summary>
        void SetProperty(params string[] parameters);
        /// <summary>
        /// 设置/修改一个元素的内容
        /// </summary>
        void SetContent(params string[] paramters);
    }
}

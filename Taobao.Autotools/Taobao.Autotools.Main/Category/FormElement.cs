using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;

namespace Taobao.Autotools.Main.Category
{
    /// <summary>
    /// 表单元素
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FormElement
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public object ElementType { set; get; }

    }


}

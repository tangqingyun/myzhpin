using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taobao.Autotools.Main.Category
{
    public class HtmlElement<T> where T : class
    {
        /// <summary>
        /// 元素id
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 类型
        /// </summary>
        public T ElementType { set; get; }

    }
}

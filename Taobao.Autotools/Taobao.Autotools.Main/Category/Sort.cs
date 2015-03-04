using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taobao.Autotools.Main.Category
{
    /// <summary>
    /// 商品所属分类实体
    /// </summary>
    [Serializable]
    public class Sort
    {
        /// <summary>
        /// 中文名
        /// </summary>
        public string Chinese { set; get; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string English { set; get; }
    }
}

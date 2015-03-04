using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taobao.Autotools.Models
{
    public class Spec
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { set; get; }
        /// <summary>
        /// 复选框Id前缀
        /// </summary>
        public string CheckboxPrefix { set; get; }
        /// <summary>
        /// input元素前缀
        /// </summary>
        public string InputPrefix { set; get; }
    }
}

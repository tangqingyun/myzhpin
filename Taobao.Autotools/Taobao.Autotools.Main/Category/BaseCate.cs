using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basement.Framework.IExplorer;
using mshtml;
using Taobao.Autotools.Models;

namespace Taobao.Autotools.Main.Category
{
    public class BaseCate
    {
        protected HTMLDocument doc = null;
        public BaseCate(string url)
        {
            doc = IExplorerExt.GetHTMLDocumentByLocationURL(url);
        }
        /// <summary>
        /// 表单元素集合
        /// </summary>
        public List<FormElement> Elements = new List<FormElement>();
        /// <summary>
        /// 执行数据绑定
        /// </summary>
        public virtual void Execute(Goods goods, string xmlfile = "")
        {

        }
        protected T GetElementById<T>(string id)
        {
            IHTMLElement huohao = doc.getElementById(id);
            T ele = (T)huohao;
            return ele;
        }

    }
}

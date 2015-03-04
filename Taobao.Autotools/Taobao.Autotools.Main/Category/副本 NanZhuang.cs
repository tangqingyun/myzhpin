using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basement.Framework.IExplorer;
using mshtml;
using Taobao.Autotools.Models;

namespace Taobao.Autotools.Main.Category
{
    /// <summary>
    /// 男装
    /// </summary>
    public class NanZhuang10000 : BaseCate
    {
        public NanZhuang10000(string url)
            : base(url) { }


        #region == 属性

        /// <summary> 
        /// 宝贝标题
        /// </summary>
        public HTMLInputElement 宝贝标题
        {
            get
            {
                var title = GetElementById<HTMLInputElement>("TitleID");
                return title;
            }
        }
        /// <summary>
        /// 宝贝卖点
        /// </summary>
        public HTMLTextAreaElement 宝贝卖点
        {
            get
            {
                var element = GetElementById<HTMLTextAreaElement>("SubheadingID");
                return element;
            }
        }
        /// <summary>
        /// 一口价
        /// </summary>
        public HTMLInputElement 一口价
        {
            get
            {
                var element = base.GetElementById<HTMLInputElement>("buynow");
                return element;
            }
        }
        /// <summary>
        /// 货号
        /// </summary>
        public HTMLInputElement 货号
        {
            get
            {

                var element = base.GetElementById<HTMLInputElement>("prop_13021751");
                return element;
            }
        }
        /// <summary>
        /// 品牌
        /// </summary>
        public HTMLSelectElement 品牌
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_20000");
                return element;
            }
        }
        /// <summary>
        /// 款式
        /// </summary>
        public HTMLSelectElement 款式
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122276315");
                return element;
            }
        }
        /// <summary>
        /// 版型
        /// </summary>
        public HTMLSelectElement 版型
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122216586");
                return element;
            }
        }
        /// <summary>
        /// 领型
        /// </summary>
        public HTMLSelectElement 领型
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_20663");
                return element;
            }
        }
        /// <summary>
        /// 风格
        /// </summary>
        public HTMLSelectElement 风格
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_20608");
                return element;
            }
        }
        /// <summary>
        /// 面料分类
        /// </summary>
        public HTMLSelectElement 面料分类
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_20551");
                return element;
            }
        }
        /// <summary>
        /// 袖型
        /// </summary>
        public HTMLSelectElement 袖型
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_2917380");
                return element;
            }
        }
        /// <summary>
        /// 图案
        /// </summary>
        public HTMLSelectElement 图案
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_20603");
                return element;
            }
        }
        /// <summary>
        /// 材质
        /// </summary>
        public HTMLSelectElement 材质
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_20021");
                return element;
            }
        }
        /// <summary>
        /// 适用季节
        /// </summary>
        public HTMLSelectElement 适用季节
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122216345");
                return element;
            }
        }
        /// <summary>
        /// 适用场景
        /// </summary>
        public HTMLSelectElement 适用场景
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122216515");
                return element;
            }
        }
        /// <summary>
        /// 适用对象
        /// </summary>
        public HTMLSelectElement 适用对象
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122216608");
                return element;
            }
        }
        /// <summary>
        /// 衣门襟
        /// </summary>
        public HTMLSelectElement 衣门襟
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_31611");
                return element;
            }
        }
        /// <summary>
        /// 服装口袋样式
        /// </summary>
        public HTMLSelectElement 服装口袋样式
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122276336");
                return element;
            }
        }
        /// <summary>
        /// 服装口袋样式
        /// </summary>
        public HTMLSelectElement 服装款式细节
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122216588");
                return element;
            }
        }
        /// <summary>
        /// 服装口袋样式
        /// </summary>
        public HTMLSelectElement 基础风格
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_42722636");
                return element;
            }
        }
        /// <summary>
        /// 细分风格
        /// </summary>
        public HTMLSelectElement 细分风格
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_42718685");
                return element;
            }
        }
        /// <summary>
        /// 服饰工艺
        /// </summary>
        public HTMLSelectElement 服饰工艺
        {
            get
            {
                HTMLSelectElement element = base.GetElementById<HTMLSelectElement>("prop_122216589");
                return element;
            }
        }
        /// <summary>
        /// 宝贝规格
        /// </summary>
        public Dictionary<string, Dictionary<HTMLInputElement, HTMLInputElement>> 宝贝规格
        {
            get
            {
                Dictionary<string, Dictionary<HTMLInputElement, HTMLInputElement>> hdic = new Dictionary<string, Dictionary<HTMLInputElement, HTMLInputElement>>();
                hdic.Add("颜色", GetColor());
                hdic.Add("尺寸", GetSize());
                return hdic;
            }
        }

        #endregion

        #region == 方法
        /// <summary>
        /// 获得颜色
        /// </summary>
        /// <returns></returns>
        private Dictionary<HTMLInputElement, HTMLInputElement> GetColor()
        {

            Dictionary<HTMLInputElement, HTMLInputElement> dic = new Dictionary<HTMLInputElement, HTMLInputElement>();
            string[] arr = { "3232483", "3232484", "3232481", "90554", "28332", "30156", "60092", "3232482", "3232478", "3232479", "28340", "28320", "3232480", "80882", "28329", "28326", "28335", "130164", "28338", "132069", "107121", "28327", "28324", "28341" };
            for (int i = 0; i < arr.Length; i++)
            {
                var cbox = GetElementById<HTMLInputElement>("prop_1627207-" + arr[i]);
                var ipt = GetElementById<HTMLInputElement>("J_Alias_1627207-" + arr[i]);
                dic.Add(cbox, ipt);
            }
            return dic;
        }
        /// <summary>
        /// 获得尺寸
        /// </summary>
        /// <returns></returns>
        private Dictionary<HTMLInputElement, HTMLInputElement> GetSize()
        {

            Dictionary<HTMLInputElement, HTMLInputElement> dic = new Dictionary<HTMLInputElement, HTMLInputElement>();
            string[] arr = { "25051812", "3271528", "3271530", "3271531", "3267945", "3271533", "3271537", "3271540", "3271542", "3267950", "3271544", "28383", "86135221", "86135247" };
            for (int i = 0; i < arr.Length; i++)
            {
                var cbox = GetElementById<HTMLInputElement>("prop_20509-" + arr[i]);
                var ipt = GetElementById<HTMLInputElement>("J_Alias_20509-" + arr[i]);
                dic.Add(cbox, ipt);
            }
            return dic;
        }
        /// <summary>
        /// 执行
        /// </summary>
        public override void Execute(Goods model, string xmlfile = "")
        {

            AddElementToList();

            宝贝标题.value = model.title;
            一口价.value = model.price;

            #region == 宝贝属性参数绑定

            Dictionary<string, string> dic = model.attrlist;
            foreach (var itm in dic)
            {
                var ddc = Elements.Where(m => m.Name == itm.Key).SingleOrDefault();
                if (ddc == null)
                {
                    continue;
                }
                if (ddc.ElementType is HTMLInputElement)
                {
                    HTMLInputElement input = (HTMLInputElement)ddc.ElementType;
                    input.value = itm.Value;
                }
                else if (ddc.ElementType is HTMLSelectElement)
                {
                    HTMLSelectElement select = (HTMLSelectElement)ddc.ElementType;
                    IExplorerExt.SelecedtOptionByText(select, itm.Value);
                }

            }
            #endregion

            #region == 宝贝规格参数绑定
            List<PropertyData> ptlist = model.propertys;
            foreach (var key in 宝贝规格.Keys)
            {
                string ky = key;
                if (key == "尺寸")
                {
                    ky = "尺码";
                }
                PropertyData property = ptlist.Where(m => m.PropertyName.Contains(ky)).SingleOrDefault();
                Dictionary<HTMLInputElement, HTMLInputElement> d1 = 宝贝规格[key];
                List<string> values = property.ValueList;
                int i = 0;
                foreach (var itm in d1)
                {
                    if (i < values.Count)
                    {
                        itm.Key.click();
                        itm.Value.value = values[i];
                    }
                    else
                    {
                        break;
                    }
                    i++;
                }
            }
            #endregion

        }

        private void AddElementToList()
        {
            Elements.Add(new FormElement { Name = "宝贝标题", ElementType = 宝贝标题 });
            Elements.Add(new FormElement { Name = "宝贝卖点", ElementType = 宝贝卖点 });
            Elements.Add(new FormElement { Name = "一口价", ElementType = 一口价 });
            Elements.Add(new FormElement { Name = "货号", ElementType = 货号 });
            Elements.Add(new FormElement { Name = "品牌", ElementType = 品牌 });
            Elements.Add(new FormElement { Name = "款式", ElementType = 款式 });
            Elements.Add(new FormElement { Name = "版型", ElementType = 版型 });
            Elements.Add(new FormElement { Name = "领型", ElementType = 领型 });
            Elements.Add(new FormElement { Name = "风格", ElementType = 风格 });
            Elements.Add(new FormElement { Name = "袖型", ElementType = 袖型 });
            Elements.Add(new FormElement { Name = "图案", ElementType = 图案 });
            Elements.Add(new FormElement { Name = "材质", ElementType = 材质 });
            Elements.Add(new FormElement { Name = "适用季节", ElementType = 适用季节 });
            Elements.Add(new FormElement { Name = "适用对象", ElementType = 适用对象 });
            Elements.Add(new FormElement { Name = "衣门襟", ElementType = 衣门襟 });
            Elements.Add(new FormElement { Name = "服装口袋样式", ElementType = 服装口袋样式 });
            Elements.Add(new FormElement { Name = "服装款式细节", ElementType = 服装款式细节 });
            Elements.Add(new FormElement { Name = "基础风格", ElementType = 基础风格 });
            Elements.Add(new FormElement { Name = "细分风格", ElementType = 细分风格 });
            Elements.Add(new FormElement { Name = "服饰工艺", ElementType = 服饰工艺 });

        }
        #endregion

    }


}

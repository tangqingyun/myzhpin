using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Basement.Framework.IO;
using HtmlAgilityPack;

namespace Taobao.Autotools.Models
{
    public class TaobaoGoods_Entity : Goods
    {


        public TaobaoGoods_Entity(string goodurl)
            : base(goodurl)
        {

                #if DEBUG

                #endif
        }
        /// <summary>
        /// 获取商品属性li
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, string> GetAttrList()
        {
            HtmlNodeCollection attributes_list = doc.DocumentNode.SelectNodes("//ul[@class='attributes-list']/li");
            Dictionary<string, string> attrlist = new Dictionary<string, string>();
            foreach (HtmlNode node in attributes_list)
            {
                string[] arr = node.InnerHtml.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                attrlist.Add(arr[0], arr[1]);
            }
            return attrlist;
        }
        /// <summary>
        /// 获取商品属性sku数据
        /// </summary>
        /// <returns></returns>
        protected override List<PropertyData> GetPropertys()
        {
            HtmlNodeCollection data_property = doc.DocumentNode.SelectNodes("//ul[@data-property]");
            List<PropertyData> dataproperty = new List<PropertyData>();
            foreach (HtmlNode node in data_property)
            {
                PropertyData property = new PropertyData();
                Match m1 = Regex.Match(node.OuterHtml, string.Format("<ul data-property=\"{0}\"", @"[\s\S]*?"));
                if (!string.IsNullOrWhiteSpace(m1.Value))
                {
                    property.PropertyName = m1.Value.Replace("\"", "").Replace("<ul data-property=", "");
                }
                string InnerHtml = node.InnerHtml;
                MatchCollection matchs = Regex.Matches(InnerHtml, @"<span>[\s\S]*?</span>");
                List<string> values = new List<string>();
                foreach (Match match in matchs)
                {
                    values.Add(match.Value.Replace("<span>", "").Replace("</span>", ""));
                }
                property.ValueList = values;
                dataproperty.Add(property);
            }
            return dataproperty;
        }

        protected override List<string> GetMainImgList()
        {
            return null;
        }

        protected override List<string> GetDetailImgList()
        {
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basement.Framework.IO;
using HtmlAgilityPack;

namespace Taobao.Autotools.Models
{
    public class Goods
    {
        #region 属性
        /// <summary>
        /// 地址
        /// </summary>
        public string goodsurl { set; get; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { set; get; }
        /// <summary>
        /// 属性list
        /// </summary>
        public Dictionary<string, string> attrlist { set; get; }
        /// <summary>
        /// sku属性
        /// </summary>
        public List<PropertyData> propertys { set; get; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { set; get; }
        /// <summary>
        /// 主图list
        /// </summary>
        public List<string> mainimg_list { set; get; }
        /// <summary>
        /// 商品详细图片list
        /// </summary>
        public List<string> detailimg_list { set; get; }
        /// <summary>
        /// 价格
        /// </summary>
        public string price { set; get; }
        #endregion

        protected HtmlDocument doc = new HtmlDocument();
        public Goods(string goodurl)
        {
            this.goodsurl = goodsurl;
            doc = new HtmlWeb().Load(goodurl);
            this.title = GetGoodsTitle();
            this.attrlist = GetAttrList();
            this.propertys = GetPropertys();
            this.price = GetPrice();
        }
        /// <summary>
        /// 获取商品标题
        /// </summary>
        /// <returns></returns>
        protected virtual string GetGoodsTitle()
        {
            HtmlNode main_title = doc.DocumentNode.SelectSingleNode("//h3[@class='tb-main-title']");
            string goodsName = string.Empty;
            if (main_title != null)
            {
                goodsName = main_title.InnerText.Trim();
            }
            return goodsName;
        }
        /// <summary>
        /// 获取价格
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPrice()
        {
            HtmlNode str_price = doc.DocumentNode.SelectSingleNode("//strong[@id='J_StrPrice']");
            if (str_price != null)
                return str_price.InnerText.Replace("&yen;","");
            else
                return string.Empty;
        }

        protected virtual Dictionary<string, string> GetAttrList()
        {
            return null;
        }

        protected virtual List<PropertyData> GetPropertys()
        {
            return null;
        }

        protected virtual List<string> GetMainImgList()
        {
            return null;
        }

        protected virtual List<string> GetDetailImgList()
        {
            return null;
        }

    }
}


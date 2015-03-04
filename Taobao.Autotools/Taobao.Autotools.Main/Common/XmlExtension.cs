using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Basement.Framework.Utility;
using HtmlAgilityPack;
using Taobao.Autotools.Models;

namespace Taobao.Autotools.Main.Common
{
    public class XmlExtension
    {
        protected HtmlAgilityPack.HtmlDocument doc;
        public XmlExtension(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            doc = htmlWeb.Load(url);
        }
        /// <summary>
        /// 宝贝规格
        /// </summary>
        private List<XElement> GetSpec()
        {

            List<XElement> specList = new List<XElement>();//宝贝规格
            HtmlNodeCollection data_pid = doc.DocumentNode.SelectNodes("//div[@data-pid]");
            foreach (HtmlNode node in data_pid)
            {
                string pid = node.GetAttributeValue("data-pid", "");//data-pid="1627207"
                string caption = node.GetAttributeValue("data-caption", "");//data-caption="颜色分类"
                HtmlNodeCollection checkboxs = doc.DocumentNode.SelectNodes("//input[@name='cp_" + pid + "']");
                Spec spec = new Spec();
                StringBuilder idsb = new StringBuilder();
                foreach (HtmlNode ckb in checkboxs)
                {
                    string strId = ckb.Id;
                    string idnumber = strId.Split('-')[1];
                    idsb.Append(idnumber + ",");
                }
                XElement xel = new XElement("spec",
                                    new XElement("id", idsb.ToString().TrimEnd(',')),
                                    new XElement("checkbox", "prop_" + pid),
                                    new XElement("input", "J_Alias_" + pid)
                                );
                xel.SetAttributeValue("name", "");
                xel.SetAttributeValue("title", caption);
                specList.Add(xel);
            }
            return specList;
        }
        /// <summary>
        /// 宝贝属性
        /// </summary>
        private List<XElement> GetAttribute()
        {

            List<XElement> ptyList = new List<XElement>();//宝贝属性
            XElement TitleID = new XElement("element");
            TitleID.SetAttributeValue("name", "宝贝标题");
            TitleID.SetAttributeValue("id", "TitleID");
            TitleID.SetAttributeValue("type", "HTMLInputElement");

            XElement SubheadingID = new XElement("element");
            SubheadingID.SetAttributeValue("name", "宝贝卖点");
            SubheadingID.SetAttributeValue("id", "SubheadingID");
            SubheadingID.SetAttributeValue("type", "HTMLTextAreaElement");

            XElement buynow = new XElement("element");
            buynow.SetAttributeValue("name", "一口价");
            buynow.SetAttributeValue("id", "buynow");
            buynow.SetAttributeValue("type", "HTMLInputElement");

            XElement brank = new XElement("element");
            brank.SetAttributeValue("name", "品牌");
            brank.SetAttributeValue("id", "prop_20000");
            brank.SetAttributeValue("type", "HTMLSelectElement");

            ptyList.Add(TitleID);
            ptyList.Add(SubheadingID);
            ptyList.Add(buynow);
            ptyList.Add(brank);

            HtmlNodeCollection spu_li = doc.DocumentNode.SelectNodes("//li[@class='J_spu-property']");
            List<Element> Elements = new List<Element>();
            foreach (HtmlNode node in spu_li)
            {
                string[] arr = node.Id.Split('_');//spu_122216351
                string idno = arr[1];
                Match match = Regex.Match(node.OuterHtml, string.Format("<label class=\"label-title\">{0}</label>", @"[\s\S]*?"));
                string title = string.Empty;
                if (match != null)
                {
                    title = HtmlTool.RemoveHtml(match.Value).Replace("：", "");
                }
                HtmlNodeCollection nn = doc.DocumentNode.SelectNodes("//li[@class='J_spu-property']/ul[@class='J_ul-single ul-select']");
                string type = string.Empty;
                if (node.OuterHtml.ToLower().Contains("<input") && !node.OuterHtml.ToLower().Contains("type=\"hidden\""))
                {
                    type = "HTMLInputElement";
                }
                else if (node.OuterHtml.ToLower().Contains("<select"))
                {
                    type = "HTMLSelectElement";
                }
                else if (node.OuterHtml.ToLower().Contains("<textarea"))
                {
                    type = "HTMLTextAreaElement";
                }
                XElement xel = new XElement("element");
                xel.SetAttributeValue("name", title);
                xel.SetAttributeValue("id", "prop_" + idno);
                xel.SetAttributeValue("type", type);
                ptyList.Add(xel);
            }
            return ptyList;
        }
        /// <summary>
        /// 生成xml配置文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool SaveXml(string filename)
        {
            if (File.Exists(filename))
            {
                return true;
            }
            XElement xml = new XElement("root", GetAttribute(), new XElement("specs", GetSpec()));
            xml.Save(filename);
            return true;
        }

    }
}

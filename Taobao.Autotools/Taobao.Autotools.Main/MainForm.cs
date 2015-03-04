using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Basement.Framework.Common;
using Basement.Framework.IExplorer;
using Basement.Framework.Utility;
using Basement.Framework.Xml;
using HtmlAgilityPack;
using mshtml;
using Taobao.Autotools.Main.Category;
using Taobao.Autotools.Main.Common;
using Taobao.Autotools.Models;

namespace Taobao.Autotools.Main
{
    public partial class MainForm : Form
    {
        public static string URL = @"http://localhost:8082/edit.html";
        public static string URL1 = @"http://www.baidu.com";
        const string line = "======================================================";
        public MainForm()
        {
            InitializeComponent();
            List<Sort> list = SortXml.SortList;
        }

        private void tb_Search_Click(object sender, EventArgs e)
        {
            try
            {

                string filename = @"F:\qingyun.tang\My.Test.Projects\zhaopin\Taobao.Autotools\Taobao.Autotools.Main\a.xml";
                XmlExtension xmle = new XmlExtension(URL);
                xmle.SaveXml(filename);

                #region == 显示商品参数
                string goodsUrl = "http://localhost:8082/goods1.html"; //this.tb_Url.Text;
                Goods model = new TaobaoGoods_Entity(goodsUrl);
                StringBuilder str = new StringBuilder();
                str.AppendLine("标题：" + model.title);
                str.AppendLine("价格：" + model.price);
                foreach (PropertyData itm in model.propertys)
                {
                    str.AppendLine(line);
                    str.AppendLine(itm.PropertyName + "：");
                    foreach (string value in itm.ValueList)
                    {
                        str.AppendLine(value);
                    }
                }
                str.AppendLine(line);
                foreach (var itm in model.attrlist)
                {
                    str.AppendLine(itm.Key + "：" + itm.Value);
                }
                tb_Content.Text = str.ToString();
                #endregion

                #region == 填写表单元素
                Type tp = Type.GetType("Taobao.Autotools.Main.Category.NanZhuang");
                Type[] tps = new Type[1];
                tps[0] = typeof(string);
                ConstructorInfo ct = tp.GetConstructor(tps);
                object[] obj = new object[1];
                obj[0] = URL;
                BaseCate zzz = (BaseCate)ct.Invoke(obj);//调用带参数的构造器
                zzz.Execute(model);
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}

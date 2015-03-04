using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Basement.Framework.Common;

namespace Taobao.Autotools.Main.Category
{
    public class SortXml
    {
        private static List<Sort> _SortList = new List<Sort>();
        public static List<Sort> SortList
        {
            get
            {
                if (_SortList.Count > 0)
                {
                    return _SortList;
                }
                string xmlpath = SysBaseHandle.BIN_DIR + @"\Configs\category.xml";
                XElement root = XElement.Load(xmlpath);
                IEnumerable<XElement> sortList = (from el in root.Elements("Sort") select el);
                foreach (XElement itm in sortList)
                {

                    Sort sort = new Sort
                    {
                        Chinese = itm.Element("Chinese").Value,
                        English = itm.Element("English").Value
                    };
                    _SortList.Add(sort);
                }
                return _SortList;
            }
        }

    }
}

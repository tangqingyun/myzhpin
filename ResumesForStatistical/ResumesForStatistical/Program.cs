using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using Basement.Framework.Net;
using Basement.Framework.Utility;
using MongoDB.Bson;
using Newtonsoft.Json;
using Zhaopin.Campus.MongoHelper;


namespace ResumesForStatistical
{
    class Program
    {
        static string path = System.AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        static string[] arrcity;
        static MongoDBHelper _dbHelper = new MongoDBHelper(ConfigurationManager.AppSettings["mongodb"].ToString(), ConfigurationManager.AppSettings["dbName"].ToString());
        static void Main(string[] args)
        {
            arrcity = ConfigurationManager.AppSettings["cityid"].ToString().Split(',');
            //Console.WriteLine("处理开始，请耐心等待.....");
            // TestResumesForStatistical();
            // GetZlcpPrivacyInfomation();
            // demo();

            Exam();

            Console.WriteLine("处理完成");
            Console.ReadKey();

        }

        private static void Exam()
        {
            long m_CID = 103024858;//103024715;//简历ID
            long m_RID = 12015;//12015;//公司ID
            long m_JID = 100002956;//100002912;//职位ID
            int m_PID = 0;//阶段ID
            string m_RDF = "cpxtxz";
            // string JNM = System.Web.HttpUtility.UrlEncode("运维工程师");//职位名
            string JNM = System.Web.HttpUtility.UrlEncode("bcd");//职位名
            //MD5: RID + CID + JID + PID + RDF
            string strkey = m_CID.ToString() + m_RID.ToString() + m_JID.ToString() + m_PID.ToString() + m_RDF;

            string kkey = FormsAuthentication.HashPasswordForStoringInConfigFile(strkey, "MD5");

            string strs = m_CID.ToString() + m_RID.ToString() + "z2l0c1p3ekey";
            string keystrs = FormsAuthentication.HashPasswordForStoringInConfigFile(strs, "MD5");

            //String PTID, String CID, String TPID, String RID, String JID, String JNM, String InterfaceKey, String Key, String PID

            byte[] result = Encoding.UTF8.GetBytes(strkey);
            var md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string Text = BitConverter.ToString(output).Replace("-", "");

            StringBuilder sb = new StringBuilder();
            sb.Append("http://172.17.1.64/Assessment/ParternerTestLogin.aspx?");
            sb.Append("PTID=XZ&");
            sb.Append("CID=" + m_CID + "&");//简历ID
            sb.Append("TPID=238&");     //试卷ID
            sb.Append("RID=" + m_RID + "&");//公司ID
            sb.Append("JID=" + m_JID + "&");//职位ID
            sb.Append("JNM=" + JNM + "&");  //职位名
            sb.Append("CMPID=100&");
            sb.Append("RDF=" + m_RDF + "&");
            sb.Append("Key=" + kkey + "");
            try
            {
                string html = HttpUtility.SendGet(sb.ToString());
                Console.WriteLine(sb.ToString());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void TestResumesForStatistical()
        {
            try
            {
                Dictionary<string, int> dic = new Dictionary<string, int>();
                long total = 0;
                for (int i = 0; i < arrcity.Length; i++)
                {
                    BsonDocument SearchModelDoc = new BsonDocument();
                    SearchModelDoc.Add("person_info.nowlocated_city_id", arrcity[i]);
                    SearchModelDoc.Add("person_info.hukou_city_id", arrcity[i]);
                    SearchModelDoc.Add("job_desired.desired_city_id", new BsonRegularExpression(new Regex("" + arrcity[i] + "")));
                    List<BsonDocument> bdoc = _dbHelper.QueryRecords("zhaopin.newcampusmodel", SearchModelDoc);
                    var groupResult = bdoc.GroupBy(m => m.GetElement("campus_id"));
                    //dic.Add(arrcity[i], groupResult.Count());
                    total += groupResult.Count();
                    string result = "城市" + arrcity[i] + "：统计数量：" + groupResult.Count();
                    Console.WriteLine(result);
                    Write(result + "\r\n");
                }
                //int total = 0;
                //foreach (var item in dic)
                //{
                //    total = total + item.Value;
                //    Console.WriteLine("城市" + item.Key + "：统计数量：" + item.Value);
                //}
                Console.WriteLine("总条数：" + total);
                Write("总条数：" + total);
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常：" + ex.Message);
            }

        }

        static string url = "http://campusrd.zhaopin.com/Evaluating/ZlcpPrivacyInfomation";
        static void GetZlcpPrivacyInfomation()
        {
            BsonDocument SearchModelDoc = new BsonDocument();
            List<BsonDocument> bdoc = _dbHelper.QueryRecords("zhaopin.resume_87773", null);
            List<Person> list = new List<Person>();
            foreach (var itm in bdoc)
            {
               
                BsonElement campus_id = itm.GetElement("campus_id");
                BsonElement root_company_id = itm.GetElement("root_company_id");
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string cid=campus_id.Value.ToString();
                string rid = "87773";
                string strs = cid + rid + "z2l0c1p3ekey";
                string keystrs = FormsAuthentication.HashPasswordForStoringInConfigFile(strs, "MD5");

                dic.Add("cid", cid);
                dic.Add("rid", rid);
                dic.Add("key", keystrs);

                string json = HttpUtility.SendPost(url, dic);
                Person person = JsonHelper.ConvertToObject<Person>(json);
                if (person.Validate())
                {
                    person.JsonString = json;
                    Write(json + "\r\n\r\n");
                }
            }
            Console.WriteLine(bdoc.Count);
        }

        static void Write(string text)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(text);
            sw.Close();
            fs.Close();
        }

        static void ImportZlcpScore() {

            string cid = ""; //简历编号
            string jid = ""; //职位编号
            string pid = ""; //阶段编号
            string scs = ""; //阶段分数和分数
            string key = ""; //密钥
            string report = ""; //测评报告

            //string Md5Values = cid + jid + pid + DataEncyptKey.ZlcpEncyptKey;

        }

        public static void demo()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CMPID", "24102471");
            string result = HttpUtility.SendPost("http://easservice.zhaopin.com/interface/Campusrd/Companysalespaper.ashx", dic);
            var obj = JsonConvert.DeserializeObject<List<JobCategorySalepaper>>(result);

            #region MyRegion
            //var config = GetSetingInfoById(rootCompanyId);
            //NameValueCollection para = new NameValueCollection();
            //para.Add("CMPID", config.CMPCompanyId.ToString());
            //string result = SendHttpPost("http://172.17.1.64/Assessment/ParternerTestLogin.aspx?ptid=xz", para);
            // string result = SendHttpPost("http://easservice.zhaopin.com/interface/Campusrd/Companysalespaper.ashx", para);
            //  string result = "[{\"Child\":null,\"Count\":0,\"ID\":1,\"Mark\":null,\"Name\":\"人岗匹配试卷\",\"Point\":0,\"Time\":0},{\"Child\":[{\"Child\":[{\"Child\":null,\"Count\":66,\"ID\":43,\"Mark\":\"该测验测查了个体在日常生活中\",\"Name\":\"MBTI测验\",\"Point\":0,\"Time\":15}],\"Count\":0,\"ID\":660,\"Mark\":null,\"Name\":\"性格\",\"Point\":0,\"Time\":0}],\"Count\":0,\"ID\":2,\"Mark\":null,\"Name\":\"经典量表试卷\",\"Point\":0,\"Time\":0},{\"Child\":null,\"Count\":0,\"ID\":680,\"Mark\":null,\"Name\":\"自定义试卷\",\"Point\":0,\"Time\":0},{\"Child\":null,\"Count\":0,\"ID\":681,\"Mark\":null,\"Name\":\"专属定制试卷\",\"Point\":0,\"Time\":0}]";
            //return obj ?? new List<JobCategorySalepaper>(); 
            #endregion

        }

    }

   
    public class JobCategorySalepaper
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<JobCategorySalepaper> Child { get; set; }
        public int Time { get; set; }
        public int Point { get; set; }
        public int Count { get; set; }
        public string Mark { get; set; }
    }

    public class Person
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Email { set; get; }
        public string Age { set; get; }
        public string Birthday { set; get; }
        public string HUKOU { set; get; }
        public string Location { set; get; }
        public string College { set; get; }
        public string Degree { set; get; }
        public string Major { set; get; }
        public string JsonString { set; get; }
        public bool Validate()
        {
            if (!string.IsNullOrWhiteSpace(ID) &&
                !string.IsNullOrWhiteSpace(Name) &&
                !string.IsNullOrWhiteSpace(Gender) &&
                !string.IsNullOrWhiteSpace(Email)
                )
            {
                return true;
            }
            return false;
        }

    }


}

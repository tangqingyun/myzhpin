using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Basement.Framework.IO;
using Basement.Framework.Net;
using Basement.Framework.Utility;
using HtmlAgilityPack;

namespace wangxiao.kjcity.com
{
    class Program
    {
        const string LOGIN_URL = "http://wangxiao.kjcity.com/lib/loginHandler.ashx";
        const string MYKE_URL = "http://wangxiao.kjcity.com/UserManage/personal.html";
        const string DOMAIN = "http://wangxiao.kjcity.com/";
        static string DBUG_DIR = System.Environment.CurrentDirectory.Replace("Debug", "");
        static CookieCollection cookies;
        static List<Course> CourseList = new List<Course>();

        static void Main(string[] args)
        {
           //Login();
           // MyCourse();
           // GetLessonInfo();
           Console.ReadKey();
        }

        static void Login()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("type", "1");
            dic.Add("name", "343195957@qq.com");
            dic.Add("pass", "15078302815");
            string loginHtml = string.Empty;
            if (cookies == null)
            {
                loginHtml = HttpUtility.SendPost(LOGIN_URL, dic, out cookies);
            }
        }

        /// <summary>
        /// 我的课程
        /// </summary>
        static void MyCourse()
        {
            //string content = HttpUtility.SendGet(MYKE_URL, null, null, null, cookies);
            HtmlDocument doc = new HtmlDocument();
            string html = FileExtension.ReadText(@"F:\mygit\zhaopin\wangxiao.kjcity.com\wangxiao.kjcity.com\personal.html", Encoding.UTF8);
            doc.LoadHtml(html);
            HtmlNode ulNode = doc.DocumentNode.SelectSingleNode("//ul[@class='learn_Bf']");
            HtmlNodeCollection h5s = ulNode.SelectNodes("//h5");
            foreach (HtmlNode node in h5s)
            {
                string href = DOMAIN + node.ChildNodes[1].Attributes["href"].Value;
                string text = TrimStr(node.ChildNodes[1].InnerText);
                Course model = new Course();
                model.Name = text;
                model.Address = href;
                model.ChapterList = new List<Chapter>();
                GetChapterList(href, model);
                CourseList.Add(model);
            }
        }
        /// <summary>
        /// 获取课程章节
        /// </summary>
        /// <param name="url"></param>
        static void GetChapterList(string url, Course model)
        {
            //string chapters_play_html = HttpUtility.SendGet(url, null, null, null, cookies);
            HtmlDocument doc = new HtmlDocument();
            string html = FileExtension.ReadText(@"F:\mygit\zhaopin\wangxiao.kjcity.com\wangxiao.kjcity.com\chapters_play.html", Encoding.UTF8);
            doc.LoadHtml(html);
            HtmlNodeCollection dls = doc.DocumentNode.SelectNodes("//dl[@class='panel_Cf']");

            int i = 0;
            foreach (HtmlNode node in dls)
            {
                Chapter chapter = new Chapter();
                chapter.Lessons = new List<Lesson>();
                HtmlNodeCollection colls = node.ChildNodes;
                foreach (HtmlNode col in colls)
                {
                    if (col.Name == "dt")
                    {
                        chapter.Name = TrimStr(col.InnerText);
                    }
                    if (col.Name == "dd")
                    {
                        string coltext = TrimStr(col.InnerText);
                        string href = DOMAIN + col.ChildNodes[1].Attributes["href"].Value;
                        chapter.Lessons.Add(new Lesson { LessonName = coltext, LessonAdr = href });
                    }
                }
                i++;
                model.ChapterList.Add(chapter);
            }

        }

        static void GetLessonInfo()
        {

            foreach (var itm in CourseList)
            {
                string course_dir = DBUG_DIR + itm.Name;
                if (!Directory.Exists(course_dir))
                {
                    Directory.CreateDirectory(course_dir);
                }

                if (itm.Name == "2015旧大纲初级电算化-串讲押题班")
                {
                    int i = 1;
                    foreach (var chp in itm.ChapterList)
                    {
                        string chapter_dir = course_dir + "\\" + chp.Name;
                        if (!Directory.Exists(chapter_dir))
                        {
                            Directory.CreateDirectory(chapter_dir);
                        }
                        StringBuilder sb = new StringBuilder();
                        string lesson_path = chapter_dir + "\\" + chp.Name + "_课时.txt";

                        //System.Threading.Thread.Sleep(3000);
                        FileExtension.WriteText(lesson_path, GetLessonAdr(chp.Lessons));
                        i++;
                    }

                }

            }



        }

        static string GetLessonAdr(List<Lesson> lessonList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var itm in lessonList)
            {
                string play_video_html = HttpUtility.SendGet(itm.LessonAdr, null, null, null, cookies);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(play_video_html);
                HtmlNode play_video_node = doc.DocumentNode.SelectSingleNode("//div[@class='play_video']");
                string video_url = play_video_node.ChildNodes[1].Attributes["src"].Value;
                string baseUrl;
                NameValueCollection nvcolle;
                StringHelper.ParseUrl(video_url, out baseUrl, out nvcolle);
                sb.AppendFormat(itm.LessonName+"："+@"http://p.bokecc.com/playvideo.bo?playerid={0}&playertype=1&autoStart=true&vid={1}\r\n", nvcolle["playerid"], nvcolle["vid"]);
            }
            return sb.ToString();
        }

        static string TrimStr(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            return str.Replace(" ", "").Replace("\n", string.Empty);
        }

    }


    /// <summary>
    /// 课程
    /// </summary>
    public class Course
    {
        /// <summary>
        /// 课程名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 课程地址
        /// </summary>
        public string Address { set; get; }
        /// <summary>
        /// 章节
        /// </summary>
        public List<Chapter> ChapterList { set; get; }
    }

    public class Chapter
    {
        /// <summary>
        /// 章节名
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 章节课程
        /// </summary>
        public List<Lesson> Lessons { set; get; }
    }

    /// <summary>
    /// 课
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// 课名
        /// </summary>
        public string LessonName { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string LessonAdr { set; get; }
    }

}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Basement.Framework.Configuration;
using Basement.Framework.IExplorer;
using Basement.Framework.IO;
using Basement.Framework.Net;
using Basement.Framework.Utility;
using HtmlAgilityPack;

namespace wangxiao.kjcity.main
{
    public partial class Main : Form
    {
        const string LOGIN_URL = "http://wangxiao.kjcity.com/lib/loginHandler.ashx";
        const string MYKE_URL = "http://wangxiao.kjcity.com/UserManage/personal.html";
        const string DOMAIN = "http://wangxiao.kjcity.com/";
        const string OUT_LOGIN = "http://wangxiao.kjcity.com/logout.aspx";
        static string BIN_DIR = System.Environment.CurrentDirectory.Replace("Debug", "");
        static string ROOT_DIR = System.Environment.CurrentDirectory.Replace(@"bin\Debug", "");
        private string IE_TEMP_DIR = string.Empty;
        static CookieCollection cookies;
        static List<Course> CourseList = new List<Course>();
        public Main()
        {
            IE_TEMP_DIR = FrameworkConfig.GetAppSetting("ietempdir");
            Control.CheckForIllegalCrossThreadCalls = false;
            this.ShowInTaskbar = true;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(delegate()
            {
                labelMsg.Text = "登录中.....";
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("type", "1");
                dic.Add("name", "343195957@qq.com");
                dic.Add("pass", "15078302815");
                string loginHtml = string.Empty;
                if (cookies == null || cookies.Count == 0)
                {
                    loginHtml = HttpUtility.SendPost(LOGIN_URL, dic, out cookies);
                }
                labelMsg.Text = "";
                if (cookies.Count > 0)
                {
                    MessageBox.Show("登录成功！");
                    GetMyCourse();
                }
                else
                {
                    MessageBox.Show("登录失败！");
                }

            });
            Thread td = new Thread(ts);
            td.Start();


        }

        private void btmOutLogin_Click(object sender, EventArgs e)
        {

            ThreadStart ts = new ThreadStart(delegate()
            {
                labelMsg.Text = "退出中.....";
                string content = HttpUtility.SendGet(OUT_LOGIN, null, null, null, cookies);
                cookies = null;
                listBoxLeft.Items.Clear();
                listBoxRight.Items.Clear();
                labelMsg.Text = "";
                MessageBox.Show("操作成功");
            });
            Thread td = new Thread(ts);
            td.Start();

        }

        private void btnChapter_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(LoadChapter);
            Thread td = new Thread(ts);
            td.Start();
        }



        private void btnPlayer_Click(object sender, EventArgs e)
        {
            IECache.IECacheClear();
            // System.Diagnostics.Process.Start("iexplore.exe", "http://p.bokecc.com/playvideo.bo?uid=FE7A65E6BE2EA539&playerid=CED4B0511C5D4992&playertype=1&autoStart=true&vid=5D906F941F40DFB59C33DC5901307461");
            string name = listBoxRight.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("请选择要播放的课时");
                return;
            }
            string[] arr = name.Split('@');
            if (arr.Length < 3)
            {
                MessageBox.Show("选择的视频不能播放");
                return;
            }
            System.Diagnostics.Process.Start("iexplore.exe", arr[0]);

        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            string cname = listBoxLeft.Text;
            if (string.IsNullOrWhiteSpace(cname))
            {
                MessageBox.Show("无要选择移动的视频");
                return;
            }
            var course = CourseList.Where(m => m.Name == cname).FirstOrDefault();
            if (course != null)
            {
                string text = listBoxRight.Text;
                string[] arr = text.Split('@');
                if (arr.Length < 3)
                {
                    MessageBox.Show("移动失败");
                    return;
                }
                string path = BIN_DIR + cname + "/" + arr[2] + "/";//保存目录
                string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                FileInfo[] files = dir.GetFiles("*.flv", SearchOption.AllDirectories);
                int i = 0;
                foreach (FileInfo file in files)//遍历所有的文件夹 显示.flv的文件
                {
                    try
                    {
                        string fileName = file.FullName;
                        string ext = file.Extension;
                        File.Move(fileName, path + arr[1] + ext);
                    }
                    catch (Exception msg)//异常处理
                    {
                        MessageBox.Show(msg.Message);
                    }
                    i++;
                }

                MessageBox.Show("移动成功！");
                System.Diagnostics.Process.Start("explorer.exe", path);
            }


        }


        #region == 公用方法
        private void LoadChapter()
        {
            labelMsg.Text = "章节获取中.....";
            listBoxRight.Items.Clear();
            if (cookies == null || cookies.Count == 0)
            {
                MessageBox.Show("请先登录");
                return;
            }
            string courseName = listBoxLeft.Text;
            if (string.IsNullOrWhiteSpace(courseName))
            {
                MessageBox.Show("请选择课程");
                return;
            }
            var course = CourseList.Where(m => m.Name == courseName).SingleOrDefault();
            if (course != null)
            {
                GetChapterList(course.Address, course);
            }
            foreach (var itm in course.ChapterList)
            {
                //创建章节目录文件夹
                string chapter_dir = BIN_DIR + "" + courseName + "\\" + itm.Name;
                if (!Directory.Exists(chapter_dir))
                {
                    Directory.CreateDirectory(chapter_dir);
                }
                listBoxRight.Items.Add(itm.Name);
                Dictionary<string, string> dic = GetLessonAdr(itm.Lessons);
                foreach (var nd in dic)
                {
                    listBoxRight.Items.Add(nd.Value + "@" + nd.Key + "@" + itm.Name);
                }

            }
            labelMsg.Text = "";
        }
        private void GetMyCourse()
        {
            listBoxLeft.Items.Clear();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string html = FileExtension.ReadText(ROOT_DIR + @"\personal.html", Encoding.UTF8);
            doc.LoadHtml(html);
            HtmlNode ulNode = doc.DocumentNode.SelectSingleNode("//ul[@class='learn_Bf']");
            HtmlNodeCollection h5s = ulNode.SelectNodes("//h5");
            StringBuilder sb = new StringBuilder();
            foreach (HtmlNode node in h5s)
            {
                string href = DOMAIN + node.ChildNodes[1].Attributes["href"].Value;
                string text = TrimStr(node.ChildNodes[1].InnerText);
                Course model = new Course();
                model.Name = text;
                model.Address = href;
                model.ChapterList = new List<Chapter>();
                sb.AppendLine(model.Name);
                CourseList.Add(model);
                listBoxLeft.Items.Add(model.Name);

                //创建课程文件夹
                string course_dir = BIN_DIR + model.Name;
                if (!Directory.Exists(course_dir))
                {
                    Directory.CreateDirectory(course_dir);
                }

            }
        }

        static void RemoveListBox(ListBox listbox)
        {
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                listbox.Items.Remove(0);

            }
        }

        static void GetChapterList(string url, Course model)
        {
            string chapters_play_html = HttpUtility.SendGet(url, null, null, null, cookies);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //string html = FileExtension.ReadText(@"F:\mygit\zhaopin\wangxiao.kjcity.com\wangxiao.kjcity.com\chapters_play.html", Encoding.UTF8);
            doc.LoadHtml(chapters_play_html);
            HtmlNodeCollection dls = doc.DocumentNode.SelectNodes("//dl[@class='panel_Cf']");
            if (dls == null)
            {
                MessageBox.Show("此课程暂无章节");
                return;
            }

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

        static Dictionary<string, string> GetLessonAdr(List<Lesson> lessonList)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var itm in lessonList)
            {
                string play_video_html = HttpUtility.SendGet(itm.LessonAdr, null, null, null, cookies);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(play_video_html);
                HtmlNode play_video_node = doc.DocumentNode.SelectSingleNode("//div[@class='play_video']");
                string video_url = play_video_node.ChildNodes[1].Attributes["src"].Value;
                string baseUrl;
                NameValueCollection nvcolle;
                StringHelper.ParseUrl(video_url, out baseUrl, out nvcolle);
                dic.Add(itm.LessonName, string.Format(@"http://p.bokecc.com/playvideo.bo?playerid={0}&playertype=1&autoStart=true&vid={1}", nvcolle["playerid"], nvcolle["vid"]));
            }
            return dic;
        }

        static string TrimStr(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            return str.Replace(" ", "").Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        #endregion

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

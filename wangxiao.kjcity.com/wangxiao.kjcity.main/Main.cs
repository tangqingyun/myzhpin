using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using Basement.Framework.Xml;
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
        static Course CurrentCourse = null;
        System.Timers.Timer timers = null;
        static List<Lesson> KeChengList = new List<Lesson>();
        string IsLogin = FrameworkConfig.GetAppSetting("islogin");

        public Main()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;
            this.ShowInTaskbar = true;
            btnPlayer.Enabled = btnMove.Enabled = false;
            if (IsLogin == "0")
            {
                btnLogin.Text = "获取课程";
                btmOutLogin.Enabled = false;
            }
            else
                btnLogin.Text = "登 录";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(delegate()
            {
                if (IsLogin == "0")
                {
                    GetMyCourse();
                    return;
                }

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
                CourseList = new List<Course>();
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
            IECache.IECacheClear();
            System.Diagnostics.Process.Start("iexplore.exe", arr[0]);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            ThreadStart ts = new ThreadStart(MoveVideo);
            Thread td = new Thread(ts);
            td.Start();
        }

        private void btnExcute_Click(object sender, EventArgs e)
        {
            if (CurrentCourse == null)
            {
                MessageBox.Show("请选择课程");
                return;
            }

            if (CurrentCourse.ChapterList == null || CurrentCourse.ChapterList.Count() == 0)
            {
                MessageBox.Show("课程无章节");
                return;
            }
            btnLogin.Enabled = false;
            btnChapter.Enabled = false;
            btnPlayer.Enabled = false;
            btnMove.Enabled = false;
            btnExcute.Enabled = false;
            btmOutLogin.Enabled = false;
            btnExcute.Enabled = false;
            listBoxLeft.Enabled = false;
            listBoxRight.Enabled = false;

            ThreadStart ts = new ThreadStart(ExecuteMoveFlv);
            Thread td = new Thread(ts);
            td.Start();

        }

        #region == 公用方法

        /// <summary>
        /// 执行flv文件移动
        /// </summary>
        private void ExecuteMoveFlv()
        {
            Lesson keshi = KeChengList.Where(m => m.MoveComplete == false).OrderBy(m => m.Id).FirstOrDefault();
            if (keshi == null) {
                return;
            }
            for (int i = 0; i < listBoxRight.Items.Count; i++)
            {
                if (listBoxRight.Items[i].ToString().Contains(keshi.RealAdr))
                {
                    listBoxRight.SelectedItem = listBoxRight.Items[i];
                    break;
                }
            }
            bool isbol = IExplorerExt.HasExistIExplorerByUrl(keshi.RealAdr);
            if (!isbol)
            {
                string text = listBoxRight.Text;
                string flvfile = "";
                if (!string.IsNullOrWhiteSpace(text))
                {
                    flvfile = BIN_DIR + CurrentCourse.Name + "/" + text.Split('@')[2] + "/" + keshi.LessonName + ".flv";
                }

                if (!File.Exists(flvfile))
                {
                    IECache.IECacheClear(); //清空浏览器缓存_打开视频
                    Process prs = System.Diagnostics.Process.Start("iexplore.exe", keshi.RealAdr);
                    //开启计时器移动缓存下载的flv文件
                    timers = new System.Timers.Timer(Convert.ToInt32(FrameworkConfig.GetAppSetting("timer")) * 1000);
                    timers.Elapsed += delegate { MoveVideoFlvFile(keshi); };
                    timers.Enabled = true;
                }
                else
                {
                    keshi.MoveComplete = true;
                    ExecuteMoveFlv();
                }

            }

        }

        private void MoveVideoFlvFile(Lesson lession)
        {
            string text = listBoxRight.Text;
            string[] arr = text.Split('@');
            string path = BIN_DIR + CurrentCourse.Name + "/" + arr[2] + "/";//保存目录
            string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            FileInfo[] files = dir.GetFiles("*.flv", SearchOption.AllDirectories);
            int i = 0;
            string fileToSelect = string.Empty;
            foreach (FileInfo file in files)//遍历所有的文件夹 显示.flv的文件
            {
                try
                {
                    string fileName = file.FullName;
                    string ext = file.Extension;
                    fileToSelect = path + arr[1] + ext;
                    if (!File.Exists(path + arr[1] + ext))
                    {
                        File.Move(fileName, path + arr[1] + ext);
                    }
                    lession.MoveComplete = true;
                    IExplorerExt.QuitIExplorerByLocationURL(arr[0]);
                    FileExtension.WriteText(BIN_DIR + CurrentCourse.Name + "/log.txt", text + "\r\n", Encoding.UTF8, true);
                    ExecuteMoveFlv();//移动完成后继续执行相同操作
                }
                catch (Exception)//异常处理
                {
                    return;
                }
                i++;
            }

        }


        private void MoveVideo()
        {

            string cname = listBoxLeft.Text;
            if (string.IsNullOrWhiteSpace(cname))
            {
                MessageBox.Show("未选择移动的视频");
                return;
            }
            var course = CourseList.Where(m => m.Name == cname).FirstOrDefault();
            if (course == null)
            {
                MessageBox.Show("课程获取失败");
                return;
            }
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
            string fileToSelect = string.Empty;
            foreach (FileInfo file in files)//遍历所有的文件夹 显示.flv的文件
            {
                try
                {
                    string fileName = file.FullName;
                    string ext = file.Extension;
                    fileToSelect = path + arr[1] + ext;
                    File.Move(fileName, path + arr[1] + ext);
                }
                catch (Exception msg)//异常处理
                {
                    MessageBox.Show(msg.Message);
                    return;
                }
                i++;
            }
            if (files.Count() == 0)
            {
                MessageBox.Show("未匹配到视频文件");
                return;
            }

            IExplorerExt.QuitIExplorerByLocationURL(arr[0]);
            System.Diagnostics.Process.Start(path);

        }
        private void LoadChapter()
        {
            string courseName = listBoxLeft.Text;
            if (string.IsNullOrWhiteSpace(courseName))
            {
                ShowMessage("请选择课程");
                return;
            }
            this.ShowMessage("章节获取中.....");
            listBoxRight.Items.Clear();
            string txtfile = ROOT_DIR + "Files/" + courseName + ".xml";
            if (File.Exists(txtfile))//如果课程文件存在则从文件中读取
            {
                ShowLocalFileChapterList(txtfile);
            }
            else
            {
                ShowNewWorkChapterList(txtfile, courseName);
            }
            this.ShowMessage();
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
        /// <summary>
        /// 获取网站上的章节
        /// </summary>
        /// <param name="url"></param>
        /// <param name="course"></param>
        private void ShowNewWorkChapterList(string txtfile, string courseName)
        {
            if (cookies == null || cookies.Count == 0)
            {
                ShowMessage("请先登录");
                return;
            }
            Course course = CourseList.Where(m => m.Name == courseName).SingleOrDefault();
            CurrentCourse = course;
            if (course == null)
            {
                ShowMessage("未获取到课程");
                return;
            }

            string chapters_play_html = HttpUtility.SendGet(course.Address, null, null, null, cookies);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(chapters_play_html);
            HtmlNodeCollection dls = doc.DocumentNode.SelectNodes("//dl[@class='panel_Cf']");
            if (dls == null)
            {
                MessageBox.Show("此课程暂无章节");
                return;
            }

            //抓取章节课时
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
                        chapter.Lessons.Add(new Lesson { Id = i, LessonName = coltext, LessonAdr = href, MoveComplete = false, RealAdr = "" });
                    }
                }
                i++;
                course.ChapterList.Add(chapter);
            }

            foreach (Chapter itm in course.ChapterList)
            {
                KeChengList.AddRange(itm.Lessons);
                //创建章节目录文件夹
                string chapter_dir = BIN_DIR + "" + course.Name + "\\" + itm.Name;
                if (!Directory.Exists(chapter_dir))
                {
                    Directory.CreateDirectory(chapter_dir);
                }
                listBoxRight.Items.Add(itm.Name);
                foreach (var nd in itm.Lessons)
                {
                    string str = nd.GetLessonRealAdr(cookies) + "@" + nd.LessonName + "@" + itm.Name;
                    listBoxRight.Items.Add(str);
                }
            }
            XmlSerializer<Course>.SerializeToFile(txtfile, course);//将课程数据写入文件保存
        }
        /// <summary>
        /// 从本地xml文件中读取课程数据
        /// </summary>
        /// <param name="fileName"></param>
        private void ShowLocalFileChapterList(string fileName)
        {
            this.ShowMessage("文件读取中.....");
            CurrentCourse = XmlSerializer<Course>.DeserializeFromFile(fileName);//从文件中读取课程数据
            foreach (Chapter itm in CurrentCourse.ChapterList)
            {
                KeChengList.AddRange(itm.Lessons);
                listBoxRight.Items.Add(itm.Name);
                foreach (var nd in itm.Lessons)
                {
                    string str = nd.RealAdr + "@" + nd.LessonName + "@" + itm.Name;
                    listBoxRight.Items.Add(str);
                }
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
                string realAdr = string.Format(@"http://p.bokecc.com/playvideo.bo?playerid={0}&playertype=1&autoStart=true&vid={1}", nvcolle["playerid"], nvcolle["vid"]);
                dic.Add(itm.LessonName, realAdr);
            }
            return dic;
        }
        private string TrimStr(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            return str.Replace(" ", "").Replace("\n", string.Empty).Replace("\r", string.Empty);
        }
        private void ShowMessage(string str = "")
        {
            labelMsg.Text = str;
        }

        #endregion

    }



 
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
    public class Lesson
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 课名
        /// </summary>
        public string LessonName { set; get; }
        /// <summary>
        /// 防盗链地址
        /// </summary>
        public string LessonAdr { set; get; }
        /// <summary>
        /// 课程真实地址
        /// </summary>
        public string RealAdr { set; get; }
        /// <summary>
        /// 获取课程真实地址
        /// </summary>
        public string GetLessonRealAdr(CookieCollection cookies)
        {
            string play_video_html = HttpUtility.SendGet(this.LessonAdr, null, null, null, cookies);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(play_video_html);
            HtmlNode play_video_node = doc.DocumentNode.SelectSingleNode("//div[@class='play_video']");
            string video_url = play_video_node.ChildNodes[1].Attributes["src"].Value;
            string baseUrl;
            NameValueCollection nvcolle;
            StringHelper.ParseUrl(video_url, out baseUrl, out nvcolle);
            string realAdr = string.Format(@"http://p.bokecc.com/playvideo.bo?playerid={0}&playertype=1&autoStart=true&vid={1}", nvcolle["playerid"], nvcolle["vid"]);
            RealAdr = realAdr;
            return realAdr;
        }
        /// <summary>
        /// 是否移动完成
        /// </summary>
        public bool MoveComplete { set; get; }
    }
    
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using DESFile;
using System.Xml;
using IniFilrO;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace 音乐播放器
{
    public partial class MainWindow : Window
    {
        #region 全局变量声明
        //软件英文简称
        private static string Appjc = "SCSM";
        //日志
        private static string AppLog = "logs";
        //数据
        private static string AppData = "Data";
        //软件数据存储目录
        private static string AppPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        //语言目录
        private static string AppLangPath = @"lang";
        //主题目录
        private static string AppThemePath = @"theme";
        //语言模板日志目录
        private static string AppLangLogPath = AppPath + @"\" + Appjc + @"\" + AppLog + @"\" + AppLangPath + @"\t";
        //软件运行目录
        string RunPath = System.Environment.CurrentDirectory;
        //窗体位置与大小
        Rect rcnormal;
        //加密密码
        string PassWord = "1HJ5@R9f3DjdN@0kWt!7AjBbSBVQD9!CPVamnzrXThZDlW90IsspvTT^4CXVC0W7HkHNyWA6DFXUWBjzgwFCdr^JHjdQ26EGjMj";
        //语言文件加密后文件后缀名
        string suffix = ".tls";
        //关于软件子窗口对象
        private static About about = new About();
        //软件设置窗口对象
        private static Settings setting = new Settings();
        //软件配置文件
        private static string AppConfigPath = AppPath + @"\" + Appjc + @"\" + AppData + @"\config\config.ini";
        #endregion

        #region 实例化窗体方法
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region 窗体主操作按钮事件
        /// <summary>
        /// 最小化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Min_Window_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 还原窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NorMal_Window_Click(object sender, RoutedEventArgs e)
        {
            this.Left = rcnormal.Left;
            this.Top = rcnormal.Top;
            this.Width = rcnormal.Width;
            this.Height = rcnormal.Height;
            this.Max_Window.Visibility = Visibility.Visible;
            this.NorMal_Window.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 最大化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Max_Window_Click(object sender, RoutedEventArgs e)
        {
            rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);
            Rect rc = SystemParameters.WorkArea;
            this.Left = 0;
            this.Top = 0;
            this.Width = rc.Width;
            this.Height = rc.Height;
            this.Max_Window.Visibility = Visibility.Hidden;
            this.NorMal_Window.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 打开关于软件窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_About_Click(object sender, RoutedEventArgs e)
        {
            if (about == null || about.IsVisible == false)
            {
                about.Left = this.Left + 20;
                about.Top = this.Top + 20;
                about.Show();
            }
            else {
                about.Activate();
                about.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// 打开博客
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_Blog_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "http://tly.unaux.com/");
        }

        /// <summary>
        /// 打开论坛
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void open_BBS_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe","http://bbs.talk4fun.net/");
        }

        /// <summary>
        /// 打开软件设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Click(object sender, RoutedEventArgs e) {
        
            if (setting == null || setting.IsVisible == false)
            {
                setting.Left = this.Left + 20;
                setting.Top = this.Top + 20;
                setting.Show();
            }
            else
            {
                setting.Activate();
                setting.WindowState = WindowState.Normal;
            }
        }
        #endregion

        #region 窗体操作事件

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //禁止拖拽窗体到屏幕顶部窗体自动最大化
            this.ResizeMode = ResizeMode.NoResize;
            //加载默认语言
            Updateinterface("zh-cn");
            //初始化
            init_App();


            //音乐播放器.Language.language.Culture = new CultureInfo("");
            //项目名.文件夹.资源文件名.Culture

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DESFileClass.EncryptFile(RunPath + @"\Language\en-us" + suffix, RunPath + @"\Language\en-usa" + suffix, PassWord);
            //DESFileClass.DecryptFile(RunPath + @"\Language\en-usa" + suffix, RunPath + @"\Language\en-usb" + suffix, PassWord);
            //DESFileClass.EncryptFile(RunPath + @"\Language\zh-cn" + suffix, RunPath + @"\Language\zh-cna" + suffix, PassWord);
            //DESFileClass.DecryptFile(RunPath + @"\Language\zh-cna" + suffix, RunPath + @"\Language\zh-cnb" + suffix, PassWord);
            Updateinterface("zh-cn");
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Updateinterface("en-us");
        }
        #endregion

        #region 加解密操作方法
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="inputFile">被加密文件路径</param>
        /// <param name="outputFile">保存文件路径</param>
        /// <param name="password">密码</param>
        private void EncryptFile(string inputFile, string outputFile, string password)
        {
            try
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);
#pragma warning disable SYSLIB0022 // 类型或成员已过时
                RijndaelManaged RMCrypto = new RijndaelManaged();
#pragma warning restore SYSLIB0022 // 类型或成员已过时
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);
                FileStream fsIn = new FileStream(inputFile, FileMode.Open);
                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);
                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
                //MessageBox.Show("Encrypt Source file succeed!", "Msg :");
            }
            catch
            {
                //MessageBox.Show("Encrypt Source file fail!", "Error :");
            }
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="inputFile">被解密文件路径</param>
        /// <param name="outputFile">保存文件路径</param>
        /// <param name="password">密码</param>
        private void DecryptFile(string inputFile, string outputFile, string password)
        {
            try
            {
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
#pragma warning disable SYSLIB0022 // 类型或成员已过时
                RijndaelManaged RMCrypto = new RijndaelManaged();
#pragma warning restore SYSLIB0022 // 类型或成员已过时
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);
                FileStream fsOut = new FileStream(outputFile, FileMode.Create);
                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);
                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
                //MessageBox.Show("Decrypt Source file succeed!", "Msg :");
            }
            catch
            {
                //MessageBox.Show("Decrypt Source file fail", "Error :");
            }
        }
        #endregion

        #region 软件初始化操作方法
        private void init_App()
        {
            //软件目录
            checkDir(AppPath + @"\" + Appjc);
            //日志目录
            checkDir(AppPath + @"\" + Appjc + @"\" + AppLog);
            //数据目录
            checkDir(AppPath + @"\" + Appjc + @"\" + AppData);
            //配置文件目录
            checkDir(AppPath + @"\" + Appjc + @"\" + AppData + @"\config");
            //语言模板目录
            string LangPath = AppPath + @"\" + Appjc + @"\" + AppLog + @"\" + AppLangPath;
            //主题模板目录
            string ThemePath = AppPath + @"\" + Appjc + @"\" + AppLog + @"\" + AppThemePath;
            if (!Directory.Exists(ThemePath))
            {
                DirectoryInfo di = Directory.CreateDirectory(ThemePath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            if (!Directory.Exists(LangPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(LangPath);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            string LangPatht = AppPath + @"\" + Appjc + @"\" + AppLog + @"\" + AppLangPath + @"\t";
            string ThemePatht = AppPath + @"\" + Appjc + @"\" + AppLog + @"\" + AppThemePath + @"\t";
            if (!Directory.Exists(LangPatht))
            {
                DirectoryInfo di = Directory.CreateDirectory(LangPatht);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            if (!Directory.Exists(ThemePatht))
            {
                DirectoryInfo di = Directory.CreateDirectory(ThemePatht);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            ////效验配置文件
            //写入默认配置
            if (File.Exists(AppConfigPath))
            {
                IniFile inifile = new IniFile(AppConfigPath);
                inifile.IniWriteValue("APP", "LANGUAGE", "ZH-CN");
                inifile.IniWriteValue("APP", "THEME", "WHITE");
                inifile.IniWriteValue("PATH", "LANGUAGE", LangPath);
                inifile.IniWriteValue("PATH", "THEME", ThemePath);
            }
        }
        #endregion

        #region 文件操作方法
        /// <summary>
        /// 检查指定文件是否存在,如不存在则创建
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool checkFile(string url) {
            try {
                if (!File.Exists(url)) File.Create(url);
                return true;
            } catch {
                return false;
            }
        }
        /// <summary>
        /// 检查指定目录是否存在,如不存在则创建
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool checkDir(string url)
        {
            try
            {
                if (!Directory.Exists(url))//如果不存在就创建file文件夹　　             　　              
                    Directory.CreateDirectory(url);//创建该文件夹　　            
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 更新界面
        //更新界面
        private void Updateinterface(string lang)
        {
//            MessageBox.Show(RunPath + @"\Language\" + lang + suffix);
            //获取加密文件并解密文件至日志目录
            DESFileClass.DecryptFile(RunPath + @"\Language\" + lang + suffix, AppLangLogPath + @"\" + lang + suffix, PassWord);
            //读取数据
            var Title = ReturnValue(lang, "Title", "//Content[@key=\"Title\"]");
            //设置界面值
            this.Window_Title.Text = Title[0].value;
        }
        private List<interfaceLang> ReturnValue(string lang, string key, string xpath)
        {
            XmlDocument doc = new XmlDocument();
            //string xmlPath = RunPath + @"\language\" + lang + suffix;
            string xmlPath = AppLangLogPath + @"\" + lang + suffix;
            //MessageBox.Show(xmlPath);
            doc.Load(xmlPath);
            XmlElement root = doc.DocumentElement;
            XmlNodeList listNodes = root.SelectNodes(xpath);
            List<interfaceLang> strArr = new List<interfaceLang>();
            foreach (XmlNode node in listNodes)
            {
                //MessageBox.Show(node.InnerText);
                //strArr.Add(node.InnerText);
                strArr.Add(new interfaceLang
                {
                    key = key,
                    value = node.InnerText
                });
            }
            return strArr;
        }

        #endregion

        #region 窗体操作事件
        private void Search_Input_OnSearch(object sender, SearchEventArgs e)
        {
            //MessageBox.Show("HHH");
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed){
                Clear.Focus();
            }
        }

        private void Search_Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                string url = "http://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric.fcg?nobase64=1&musicid=1387449&callback=json&g_tk=938407465&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0";
                //List<HttpRequestsHead> header = new List<HttpRequestsHead>();
                //header.Add(new HttpRequestsHead { 
                //    key = "referer",
                //    Value = "https://www.kuwo.cn/search/list?key=一路生花"
                //});
                //header.Add(new HttpRequestsHead
                //{
                //    key = "Cookie",
                //    Value = "_ga=GA1.2.1245665577.1633765628; _gid=GA1.2.761249954.1633765628; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1633765628; _gat=1; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1633765801; kw_token=RI71M5SYHGM"
                //});
                //header.Add(new HttpRequestsHead
                //{
                //    key = "csrf",
                //    Value = "RI71M5SYHGM"
                //});
                //string html = getHttp(url, "http://y.qq.com/portal/song/003k64X11As4qX.html");
                //MessageBox.Show(html);
                var option = new ChromeOptions();
                option.AddArgument("--headless");
                option.AddArgument("referer=\"http://y.qq.com/portal/song/003k64X11As4qX.html\"");
                //option.AddArgument("--disable-javascript");
                option.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
                option.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                var CDS = ChromeDriverService.CreateDefaultService();
                CDS.HideCommandPromptWindow = true;
                CDS.Start();
                IWebDriver driver = new ChromeDriver(CDS, option);
                //driver.Url = "http://tyapi.totalh.net/api/yan/api.php?charset=utf-8";
                driver.Url = url;
                var source = driver.PageSource;
                //IWebElement web = driver.FindElement(By.XPath("/html/body"));
                MessageBox.Show(source);
                //MessageBox.Show(web.Text);
            }
        }
        #endregion

        #region EEE
        #endregion

        #region 操作方法
        public string getHttp(string HttpUrl, string RefererUrl)
        {
            string html = "";
            try
            {
                MSXML2.XMLHTTP Http = new MSXML2.XMLHTTPClass();
                //Http.open("GET", HttpUrl, false, null, null);
                Http.open("GET", HttpUrl, false, null, null);
                Http.setRequestHeader("referer", RefererUrl);
                //Http.setRequestHeader("Cookie", "_ga=GA1.2.1245665577.1633765628; _gid=GA1.2.761249954.1633765628; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1633765628; _gat=1; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1633765801; kw_token=RI71M5SYHGM");
                //Http.setRequestHeader("csrf", "RI71M5SYHGM");
                Http.send("");
                html = Encoding.Default.GetString((byte[])Http.responseBody);
                Http = null;
            }
            catch
            {
            }
            return html;
        }
        //private string GetHTTPSendCode(string url, List<HttpRequestsHead> header, string method = "GET", string contenttype = "text/plain; charset=UTF-8", string accept = "application/json, text/javascript, */*; q=0.01")
        //{
        private string GetHTTPSendCode(string url, List<HttpRequestsHead> header, string encoder = "utf-8", string method = "GET", string contenttype = null, string accept = null)
            {
            Uri uri = new Uri(url);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "Get";
            req.KeepAlive = true;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.72 Safari/537.36");
            req.Headers.Add("Referer", "https://www.kuwo.cn/search/list?key=一路生花");
            req.Headers.Add("Cookie", "_ga=GA1.2.1245665577.1633765628; _gid=GA1.2.761249954.1633765628; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1633765628; _gat=1; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1633765801; kw_token=RI71M5SYHGM");
            req.Headers.Add("csrf", "RI71M5SYHGM");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream receviceStream = res.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            res.Close();
            return strHTML;

            //WebClient webClient = new WebClient();
            //webClient.Credentials = CredentialCache.DefaultCredentials;
            //webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.72 Safari/537.36");
            //webClient.Headers.Add("referer", "https://www.kuwo.cn/search/list?key=一路生花");
            //webClient.Headers.Add("Cookie", "_ga=GA1.2.1245665577.1633765628; _gid=GA1.2.761249954.1633765628; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1633765628; _gat=1; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1633765801; kw_token=RI71M5SYHGM");
            //webClient.Headers.Add("csrf", "RI71M5SYHGM");
            //for (int i = 0; i < header.Count; i++)
            //{
            //    webClient.Headers.Add(header[i].key, header[i].Value);
            //}
            //byte[] pagedata = webClient.DownloadData(url);
            //string html = Encoding.GetEncoding(encoder).GetString(pagedata);
            //return html;
            //string HtmlCode = string.Empty;
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //for (int i = 0; i < header.Count; i++)
            //{
            //    request.Headers.Add(header[i].key, header[i].Value);
            //}
            //request.Method = method;
            //request.ContentType = contenttype;
            //request.Accept = accept;
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //if (response.CharacterSet.ToLower() == "gbk")
            //{
            //    using (Stream resstream = response.GetResponseStream())
            //    {
            //        using (StreamReader str = new StreamReader(resstream, Encoding.GetEncoding("gb2312")))
            //        {
            //            HtmlCode = str.ReadToEnd();
            //        }
            //    }
            //}
            //else {
            //    using (Stream resstream = response.GetResponseStream())
            //    {
            //        using (StreamReader str = new StreamReader(resstream, Encoding.UTF8))
            //        {
            //            HtmlCode = str.ReadToEnd();
            //        }
            //    }
            //}
            //return HtmlCode;
        }
        #endregion

        #region 操作事件
        #region qq音乐
        public static void SearchMusic() {

        }
        #endregion
        #endregion
    }
    public class interfaceLang
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class HttpRequestsHead { 
        public string key { get; set; }
        public string Value { get; set; }
    }
}

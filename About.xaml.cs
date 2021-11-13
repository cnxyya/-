using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 音乐播放器
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        #region 窗体主操作方法
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
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
        /// 窗体拖动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        /// <summary>
        /// 窗体加载时方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.NoResize;
            string newVersion = GetWebCode("https://cnxyya.github.io/SMSC-Music-Player/version.md", "utf-8");
            this.About_NewVersion.Text = newVersion;
        }
        #endregion

        #region 操作方法
        private static string GetWebCode(string url, string encoder) {
            WebClient webClient = new WebClient();
            byte[] DataBuffer = webClient.DownloadData(url);
            string SourceCode = Encoding.GetEncoding(encoder).GetString(DataBuffer);
            return SourceCode;
        }
        #endregion

        #region 界面链接点击事件
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/cnxyya/SMSC-Music-Player");
            }
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Diagnostics.Process.Start("explorer.exe", "https://gitee.com/zxcvbnm129/smsc-music-player");
            }
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Diagnostics.Process.Start("explorer.exe", "http://bbs.talk4fun.net/threads/11/");
            }
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                System.Diagnostics.Process.Start("explorer.exe", "http://tly.unaux.com/thread/thread-2021-11-12-83.html");
            }
        }
        #endregion
    }
}

using SMOperateS;
using System.Windows;
using System.Windows.Input;

namespace 音乐播放器
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window
    {
        public delegate void DeleOperate();

        public static event DeleOperate OperateWindowEvent;

        private static MainWindow mainWindow = new MainWindow();

        public Settings()
        {
            InitializeComponent();
        }

        #region 窗体主操作方法
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Min_Window_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Close_Window_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 语言更改方法组
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PublicOperate.RestartApp();
        }
        #endregion

        #region 操作方法
        public void UpdatePanel() {
            mainWindow.open_About.Content = "你好";
        }
        #endregion
    }
}

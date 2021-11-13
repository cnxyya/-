using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 音乐播放器.Control
{
    /// <summary>
    /// SearchTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTextBox : UserControl
    {

        public event EventHandler<SearchEventArgs> OnSearch;
        public SearchTextBox()
        {
            InitializeComponent();
        }

        private void Tl_SearchInput_KeyDown(object sender, KeyEventArgs e)
        {
            ExeRunSearch();
        }

        private void ExeRunSearch() {
            if (OnSearch != null) {
                var args = new SearchEventArgs();
                args.SearchText = Tl_SearchInput.Text;
                OnSearch(this, args);
            }
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            ExeRunSearch();
        }
    }
}
public class SearchEventArgs : EventArgs { 
    public string SearchText { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AccountManagerWindow
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AccountManagerWindowView 
    {
        public AccountManagerWindowView()
        {
            InitializeComponent();
            Loaded += AccountManagerWindowView_Loaded;
        }

        private void AccountManagerWindowView_Loaded(object sender, RoutedEventArgs e)
        {
            //pager.PageShowRows = new System.Collections.ObjectModel.ObservableCollection<int>() { 20, 50, 100, 200 };
            //pager.CurrentShowRows = 50;
        }

        private void Search_OnKeyDown(object sender, KeyEventArgs e)
        {

        }

        private void PagerControl_CurrentPageNumChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            MessageBox.Show($"当前显示页：{e.NewValue}");
        }

        private void PagerControl_CurrentShowRowsChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
        
                MessageBox.Show($"当前页显示行数：{e.NewValue}");
          
        }
    }
}

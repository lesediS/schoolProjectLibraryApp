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

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FindNumber findNumber = new FindNumber();
            this.Hide();
            findNumber.Show();
        }
        private void OpenReplaceBooks(object sender, RoutedEventArgs e)
        {
            Replace ReplacingBooks = new Replace();
            this.Visibility = Visibility.Hidden;
            ReplacingBooks.Show();
        }

        private void IDArea(object sender, RoutedEventArgs e)
        {
            Match ID = new Match();
            this.Visibility = Visibility.Hidden;
            ID.Show();
        }
    }
}

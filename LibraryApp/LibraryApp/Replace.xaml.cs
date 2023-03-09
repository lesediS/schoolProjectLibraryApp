using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryApp.Class;

namespace LibraryApp
{
    public partial class Replace : Window
    {
        readonly List<string> list = new List<string>();
        readonly List<DCode> Dlist = new List<DCode>();
        readonly List<string> userList = new List<string>();
        private readonly Score score = new Score();
        
        public Replace()
        { 
            
            for (int i = 0; i < 10; i++)
            {
                var code = new DCode();
                
                Dlist.Add(code);
                list.Add(code.Code);
            }
            InitializeComponent();
            LBDataBinding.ItemsSource = list;
            Score.Text = Convert.ToString(score.score);
        }

        

        private void move(object sender, RoutedEventArgs e)
        {
            if (LBDataBinding.SelectedIndex != -1)
            {
                userList.Add(list.ElementAt(LBDataBinding.SelectedIndex));
                list.RemoveAt(LBDataBinding.SelectedIndex);
            
                LBDataBinding.ItemsSource = null;
                LBDataBinding.ItemsSource = list;
            
                LBDataSelected.ItemsSource = null;
                LBDataSelected.ItemsSource = userList;

            }
        }
        private void returnItem(object sender, RoutedEventArgs e)
        {
            if (LBDataSelected.SelectedIndex != -1)
            {
                list.Add(userList.ElementAt(LBDataSelected.SelectedIndex));
                userList.RemoveAt(LBDataSelected.SelectedIndex);
                LBDataBinding.ItemsSource = null;
                LBDataBinding.ItemsSource = list;
                LBDataSelected.ItemsSource = null;
                LBDataSelected.ItemsSource = userList;
            }

        }
        
        public void checkList(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int total = Dlist.Count;
            
            score.Check();
            
            var sortedList = Sort.QuickSort(Dlist, 0, Dlist.Count - 1);
            foreach (var x in sortedList)
            {
                if (sortedList.IndexOf(x) == userList.IndexOf(x.Code))
                {
                    i++;
                }
            }
            Score.Text = Convert.ToString(score.score);
            MessageBox.Show("You have " + i + " correct out of " + total);
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            int i = 0;
            int total = Dlist.Count;

            

            var sortedList = Sort.QuickSort(Dlist, 0, Dlist.Count - 1);
            foreach (var x in sortedList)
            {
                if (sortedList.IndexOf(x) == userList.IndexOf(x.Code))
                {
                    i++;
                }
            }
            
            Score.Text = Convert.ToString(score.CheckScore(i, total));
            MessageBox.Show("You have " + i + " correct out of " + total);
        }
    }
}

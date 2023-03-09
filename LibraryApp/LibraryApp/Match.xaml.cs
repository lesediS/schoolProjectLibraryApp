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
using System.Windows.Shapes;
using LibraryApp.Class;

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for Match.xaml
    /// </summary>
    /// 
    
    public partial class Match : Window
    {
        private List<string> list = new List<string>();
        private readonly List<string> CallNumbers = new List<string>();
        private readonly List<string> Descriptions = new List<string>();
        private readonly Score score = new Score();
        Dictionary<string,string> deweyCodes = new Dictionary<string,string>();
        private bool toggleQuestion = true;
        private int totalQuestions = 0;
        private int correctQuestions = 0;
        int callNumberCount;
        int descriptionCount;
        
        public Match()
        {
            toggleQuestion = true;
            InitializeComponent();
            initializeDeweyCodeList();
            populateListBoxes();
            initializeLists();
        }

        private void populateListBoxes()
        {
            
            
            if (toggleQuestion)
            {
                callNumberCount = 7;
                descriptionCount = 4;
            }
            else
            {
                descriptionCount = 7;
                callNumberCount = 4;
            }
            for (var i = 0; i < callNumberCount; i++)
            {
                var code = new DCode();
                CallNumbers.Add(code.bookCode);
            }

            getCodeDescriptions();
            Descriptions.Sort();
        }

        private void initializeLists()
        {
            LeftDataBinding.ItemsSource = null;
            LeftDataBinding.ItemsSource = CallNumbers;
            RightDataBinding.ItemsSource = null;
            RightDataBinding.ItemsSource = Descriptions;
        }

        private void initializeDeweyCodeList()
        {
            deweyCodes.Add("000", "General Works");
            deweyCodes.Add("100", "Philosophy & Psychology");
            deweyCodes.Add("200", "Religion");
            deweyCodes.Add("300", "Social Sciences");
            deweyCodes.Add("400", "Language");
            deweyCodes.Add("500", "Science");
            deweyCodes.Add("600", "Technology");
            deweyCodes.Add("700", "Arts & Recreation");
            deweyCodes.Add("800", "Literature");
            deweyCodes.Add("900", "History & Geography");
        }
        private void getCodeDescriptions()
        {
            int counter;
            if (toggleQuestion)
            {
                counter = Descriptions.Count;
            }
            else
            {
                counter = CallNumbers.Count;
            }
            for (int i = 0; i < counter; i++)
            {
                while (Descriptions.Count < descriptionCount)
                {
                    foreach (KeyValuePair<string, string> deweyCode in deweyCodes)
                    {
                        if (int.Parse(deweyCode.Key) <= int.Parse(CallNumbers[i]) &&
                            (int.Parse(deweyCode.Key) + 99) >= int.Parse(CallNumbers[i]))
                        {
                            Descriptions.Add(deweyCode.Value);
                        }
                    }
                }
            }
        }
        private void LeftDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeftDataBinding.SelectedIndex != -1 && RightDataBinding.SelectedIndex != -1)
            {
                Submit();
                LeftDataBinding.SelectedIndex = -1;
                RightDataBinding.SelectedIndex = -1;
            }
        }
        private void RightDataBinding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LeftDataBinding.SelectedIndex != -1 && RightDataBinding.SelectedIndex != -1)
            {
                Submit();
                LeftDataBinding.SelectedIndex = -1;
                RightDataBinding.SelectedIndex = -1;
            }
        }

        private void NextQuestion(object sender, RoutedEventArgs e)
        {
            toggleQuestion = !toggleQuestion;
            CallNumbers.Clear();
            Descriptions.Clear();
            populateListBoxes();
            initializeLists();
        }

        private void EndGame(object sender, RoutedEventArgs e)
        {
            Score.Text = Convert.ToString(score.CheckScore(correctQuestions,totalQuestions ));
            MessageBox.Show("You have " + correctQuestions + " correct out of " + totalQuestions);
        }
        public bool checkAnswer( string code, string description)
        {
            foreach (KeyValuePair<string, string> deweyCode in deweyCodes)
            {
                if (int.Parse(deweyCode.Key) <= int.Parse(code) && (int.Parse(deweyCode.Key)+99)>= int.Parse(code))
                {
                    if (deweyCode.Value == description)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Submit()
        {
            removeAnswer();
            
            if (!toggleQuestion)
            {
                if (checkAnswer( CallNumbers[RightDataBinding.SelectedIndex],
                        Descriptions[LeftDataBinding.SelectedIndex]))
                {
                    score.score++;
                    Score.Text = score.CheckScore(correctQuestions, totalQuestions).ToString();
                }
            }
            else
            {
                if (checkAnswer( CallNumbers[LeftDataBinding.SelectedIndex], Descriptions[RightDataBinding.SelectedIndex]))
                {
                    score.score++;
                    Score.Text = score.CheckScore(correctQuestions, totalQuestions).ToString();
                }
            }
        }

        private void removeAnswer()
        {
            CallNumbers.RemoveAt(LeftDataBinding.SelectedIndex);
            Descriptions.RemoveAt(RightDataBinding.SelectedIndex);
            initializeLists();
        }
    }
}

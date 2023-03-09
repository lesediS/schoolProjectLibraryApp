using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LibraryApp.Class;

namespace LibraryApp
{
    public partial class FindNumber
    {
        private DeweyBinary db = new DeweyBinary();
        private DeweyNode _number;
        private string topNumber;
        
        Score score = new Score();

        public FindNumber()
        {
            InitializeComponent();
            initGame();
        }

        public void initGame()
        {
            getRandomCallNumber();
            SetUpDeweytreeView();
        }

        private void clearAll()
        {
            DeweyTreeView.Items.Clear();
        }

        private void SetUpDeweytreeView()
        {
            TreeViewItem root = new TreeViewItem();
            //add 3 incorrect root nodes to the treeview
            TreeViewItem DeweyRoot = new TreeViewItem();
            DeweyRoot.Header = "Dewey Decimal";
            DeweyRoot.IsExpanded = true;
            TreeViewItem item1 = new TreeViewItem();
            TreeViewItem item2 = new TreeViewItem();
            TreeViewItem item3 = new TreeViewItem();

            var end = "00";
            Random rand = new Random();
            int num = rand.Next(1, 9);
            item1.Header = num.ToString() + end;
            num = rand.Next(1, 9);
            item2.Header = num.ToString() + end;
            num = rand.Next(1, 9);
            item3.Header = num.ToString() + end;
            item1.Items.Add("Wrong");
            item2.Items.Add("Wrong");
            item3.Items.Add("Wrong");
            DeweyRoot.Items.Add(item1);
            DeweyRoot.Items.Add(item2);
            DeweyRoot.Items.Add(item3);
            var code = Int32.Parse(topNumber+end);
            var correctHead = setupCorrectNode(code);
                
                

            DeweyRoot.Items.Add(correctHead);
            DeweyRoot.Items.SortDescriptions.Add(
                new System.ComponentModel.SortDescription("Header", System.ComponentModel.ListSortDirection.Ascending));
            DeweyTreeView.Items.Add(DeweyRoot);
            //add the correct root node to the treeview
        }

        private object setupCorrectNode(int code)
        {
            TreeViewItem correctHead = new TreeViewItem();
            correctHead.Header = topNumber + "00";
            for (int i = 0; i < 100; i++)
            {
                int Search = code + i;
                //create nodes for each of the 100 children in groups of 10
                if (i % 10 == 0)
                {
                    TreeViewItem subItem = new TreeViewItem();
                    subItem.Header =Search.ToString() + "-" + (Search + 9).ToString();
                    subItem.SetValue(TreeViewItem.TagProperty, Search);
                    correctHead.Items.Add(subItem);
                    for (int j = 0; j < 10; j++)
                    {
                        TreeViewItem subSubItem = new TreeViewItem();
                        var call = db.findDescription(Search + j).Data;
                        subSubItem.Header = call.code + "--"+call.description;
                        subSubItem.SetValue(TreeViewItem.TagProperty, call.code);
                        subItem.Items.Add(subSubItem);
                        subSubItem.AddHandler(TreeViewItem.SelectedEvent, new RoutedEventHandler(subItem_Expanded));
                    }
                }
            }

            return correctHead;
        }

        private void subItem_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;
            var tag = item.Tag;
            if (tag == _number.code)
            {
                score.score++;
                MessageBox.Show("Correct!");
            }
            else
            {
                score.score--;
                MessageBox.Show("Wrong!");
                
            }
            clearAll();
            initGame();
        }
        private void next()
        {
            clearAll();
            initGame();
        }


        private void getRandomCallNumber()
        {
            //get a random call number
            _number = db.GetRandomCallNumber();
            topNumber = _number.code.ToCharArray()[0].ToString();
            //display the call number
            CallNumber.Text = _number.description;
        }

        private void DeweyTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //get the selected node
            TreeViewItem selectedNode = (TreeViewItem)DeweyTreeView.SelectedItem;
            //check to see if its a leaf node
            if (selectedNode.Items.Count == 0)
            {
                //get the call number
                string callNumber = selectedNode.Header.ToString();
                //check to see if the call number is correct
                if (callNumber == _number.description)
                {
                    //correct
                    score.score++;
                    //get a new call number
                    getRandomCallNumber();
                }
                else
                {
                    //incorrect
                    score.score--;
                    initGame();
                }
            }
            else
            {
                //check if the selected node is the same as the call number
                if (selectedNode.Header.ToString().ToCharArray()[0] != _number.code.ToCharArray()[0])
                {
                    //display a message
                    MessageBox.Show("Wrong");
                    //get a new call number
                    score.Check();
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LibraryApp.Class
{
    public class DeweyBinary
    {
        public DeweyBinary()
        {
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\"));
            string codePath = Path.Combine(newPath, "CallNumbers.txt");
            
            string[] lines = File.ReadAllLines(codePath);

            foreach (var item in lines)
            {var split = item.Split(":");
                var a = new DeweyNode()
                {
                    code = split[0],
                    description = split[1]
                };
                this.Add(a);
                
            }
        }
        public class Node
        {
            public DeweyNode Data;
            public Node Left;
            public Node Right;
        }

        public Node Root;

        public void Add(DeweyNode data)
        {
            if (Root == null)
            {
                Root = new Node { Data = data };
            }
            else
            {
                Add(data, Root);
            }
        }

        private void Add(DeweyNode data, Node root)
        {
            if (String.CompareOrdinal(data.code, root.Data.code) < 0)
            {
                if (root.Left == null)
                {
                    root.Left = new Node { Data = data };
                }
                else
                {
                    Add(data, root.Left);
                }
            }
            else
            {
                if (root.Right == null)
                {
                    root.Right = new Node { Data = data };
                }
                else
                {
                    Add(data, root.Right);
                }
            }
        }

        public Node findDescription(int code)
        {
            
            Node temp = Root;
            while (temp != null)
            {
                if (code == int.Parse(temp.Data.code))
                {
                    return temp;
                }
                else if (code < int.Parse(temp.Data.code))
                {
                    temp = temp.Left;
                    
                }
                else
                {
                    temp = temp.Right;
                }
            }
            return findDescription(code, temp);
        }

        public Node findDescription(int code, Node root)
        {
            Node temp = root;
            while (temp != null)
            {
                if (code == int.Parse(temp.Data.code))
                {
                    return temp;
                }
                else if (code < int.Parse(temp.Data.code))
                {
                    temp = temp.Left;

                }
                else
                {
                    temp = temp.Right;
                }
            }

            return temp;
        }

        public int Count()
        {
            return Count(Root);
        }

        private int Count(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.Left == null && root.Right == null)
            {
                return 1;
            }

            return 1 + Count(root.Left) + Count(root.Right);
        }

        public IEnumerable<Node> GetChildren(Node node)
        {
            if (node.Left != null)
            {
                yield return node.Left;
            }

            if (node.Right != null)
            {
                yield return node.Right;
            }
        }

        public IEnumerable<Node> GetTree()
        {
            return GetTree(Root);
        }

        private IEnumerable<Node> GetTree(Node root)
        {
            if (root == null)
            {
                yield break;
            }

            yield return root;

            foreach (var node in GetTree(root.Left))
            {
                yield return node;
            }

            foreach (var node in GetTree(root.Right))
            {
                yield return node;
            }
        }

        public DeweyNode GetRandomCallNumber()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, Count());
            int currentIndex = 0;

            foreach (var node in GetTree())
            {
                if (currentIndex == randomIndex)
                {
                    return node.Data;
                }

                currentIndex++;
            }

            return null;
        }
    };
}




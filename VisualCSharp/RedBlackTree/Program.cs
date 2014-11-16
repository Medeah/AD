using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Dictionary<int, int>();
            //test.Add(1, 1);
            //test.Add(1, 1);
        }
    }

    class RedBlackTree<T, U> : ISortedDictionary<T, U>, IRBproperties where
        T : IComparable<T>
    {
        enum Color
        {
            Red, Black
        }
        class Node
        {
            public Node(T key, U value, Color color)
            {
                Key = key;
                Value = value;
                Color = color;
            }
            public Node P;
            public Node Left;
            public Node Right;
            public Color Color;
            public T Key;
            public U Value;
            public KeyValuePair<T, U> toKeyValue()
            {
                return new KeyValuePair<T, U>(Key, Value);
            }
        }

        readonly Node nil = new Node(default(T), default(U), Color.Black);
        Node root;

        public RedBlackTree()
        {
            root = nil;
            nil.Left = nil;
            nil.Right = nil;
        }

        private void LeftRotate(Node x)
        {
            var y = x.Right;
            x.Right = y.Left;
            if (y.Left != nil)
            {
                y.Left.P = x;
            }
            y.P = x.P;
            if (x.P == nil)
            {
                root = y;
            }
            else if (x == x.P.Left)
            {
                x.P.Left = y;
            }
            else
            {
                x.P.Right = y;
            }
            y.Left = x;
            x.P = y;
        }

        private void RightRotate(Node x)
        {
            var y = x.Left;
            x.Left = y.Right;
            if (y.Right != nil)
            {
                y.Right.P = x;
            }
            y.P = x.P;
            if (x.P == nil)
            {
                root = y;
            }
            else if (x == x.P.Right)
            {
                x.P.Right = y;
            }
            else
            {
                x.P.Left = y;
            }
            y.Right = x;
            x.P = y;
        }

        public void Insert(T key, U value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            Node y = nil;
            Node x = root;
            Node z = new Node(key, value, Color.Red);

            while (x != nil)
            {
                y = x;
                if (z.Key.CompareTo(x.Key) < 0)
                {
                    x = x.Left;
                }
                else if (z.Key.CompareTo(x.Key) > 0)
                {
                    x = x.Right;
                }
                else
                {
                    throw new ArgumentException("An item with the same key has already been added.");
                }
            }
            z.P = y;
            if (y == nil)
            {
                root = z;
            }
            else if (z.Key.CompareTo(y.Key) < 0)
            {
                y.Left = z;
            }
            else
            {
                y.Right = z;
            }
            z.Left = nil;
            z.Right = nil;
            RBInsertFixup(z);
        }

        private void RBInsertFixup(Node z) {
            while (z.P.Color == Color.Red)
            {
                if (z.P == z.P.P.Left)
                {
                    var y = z.P.P.Right;
                    if (y.Color == Color.Red)
                    {
                        z.P.Color = Color.Black;
                        y.Color = Color.Black;
                        z.P.P.Color = Color.Red;
                        z = z.P.P;
                    }
                    else
                    {
                        if (z == z.P.Right)
                        {
                            z = z.P;
                            LeftRotate(z);
                        }
                        z.P.Color = Color.Black;
                        z.P.P.Color = Color.Red;
                        RightRotate(z.P.P);
                    }
                }
                else
                {
                    var y = z.P.P.Left;
                    if (y.Color == Color.Red)
                    {
                        z.P.Color = Color.Black;
                        y.Color = Color.Black;
                        z.P.P.Color = Color.Red;
                        z = z.P.P;
                    }
                    else
                    {
                        if (z == z.P.Left)
                        {
                            z = z.P;
                            RightRotate(z);
                        }
                        z.P.Color = Color.Black;
                        z.P.P.Color = Color.Red;
                        LeftRotate(z.P.P);
                    }
                }
            }
            root.Color = Color.Black;
        }

        private void Transplant(Node u, Node v)
        {
            if (u.P == nil)
            {
                root = v;
            }
            else if (u == u.P.Left)
            {
                u.P.Left = v;
            }
            else
            {
                u.P.Right = v;
            }
            if (v != nil)
            {
                v.P = u.P;
            }
        }

        private void TreeDelete(Node z)
        {
            if (z.Left == nil)
            {
                Transplant(z, z.Right);
            }
            else if (z.Right == nil)
            {
                Transplant(z, z.Left);
            }
            else
            {
                var y = TreeMinimum(z.Right);
                if (y.P != z)
                {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    y.Right.P = y;
                }
                Transplant(z, y);
                y.Left = z.Left;
                y.Left.P = y;
            }
        }

        public void Delete(T key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
            var res = TreeSearch(root, key);
            if (res != nil)
            {
                TreeDelete(res);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private Node TreeSearch(Node root, T key)
        {
            if (root == nil)
            {
                return nil;
            }
            if (root.Key.CompareTo(key) == 0)
            {
                return root;
            }
            if (root.Key.CompareTo(key) > 0)
            {
                return TreeSearch(root.Left, key);
            }
            return TreeSearch(root.Right, key);
        }

        public KeyValuePair<T, U>? Search(T key)
        {
            var res = TreeSearch(root, key);
            if (res != nil)
            {
                return res.toKeyValue();
            }
            return null;
        }

        private Node TreePredecessor(Node x)
        {
            if (x.Left != nil)
            {
                return TreeMaximum(x.Left);
            }
            var y = x.P;
            while (y != nil && x == y.Left)
            {
                x = y;
                y = y.P;
            }
            return y;
        }

        public KeyValuePair<T, U> Predecessor(T key)
        {
            var res = TreeSearch(root, key);
            if (res != nil)
            {
                return TreePredecessor(res).toKeyValue();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private Node TreeSuccessor(Node x)
        {
            if (x.Right != nil)
            {
                return TreeMinimum(x.Right);
            }
            var y = x.P;
            while (y != nil && x == y.Right)
            {
                x = y;
                y = y.P;
            }
            return y;
        }

        public KeyValuePair<T, U> Successor(T key)
        {
            var res = TreeSearch(root, key);
            if (res != nil)
            {
                return TreeSuccessor(res).toKeyValue();
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        private Node TreeMinimum(Node subTreeRoot)
        {
            var x = subTreeRoot;
            while (x.Left != nil)
            {
                x = x.Left;
            }
            return x;
        }

        public KeyValuePair<T, U> Minimum()
        {
            if (root == nil)
            {
                throw new InvalidOperationException();
            }
            return TreeMinimum(root).toKeyValue();
        }

        private Node TreeMaximum(Node subTreeRoot)
        {
            var x = subTreeRoot;
            while (x.Right != nil)
            {
                x = x.Right;
            }
            return x;
        }

        public KeyValuePair<T, U> Maximum()
        {
            if (root == nil)
            {
                throw new InvalidOperationException();
            }
            return TreeMaximum(root).toKeyValue();
        }

        public U this[T key]
        {
            get
            {
                var res = Search(key);
                if (res.HasValue)
                {
                    return res.Value.Value;
                }
                throw new KeyNotFoundException();
            }
            set
            {
                var res = TreeSearch(root, key);
                if (res == nil)
                {
                    Insert(key, value);
                }
                else
                {
                    res.Value = value;
                }
            }
        }


        public bool BlackRoot()
        {
            return root.Color == Color.Black;
        }

        public bool RedHasBlackChildren()
        {
            
            for (var curr = TreeMinimum(root);curr != nil;curr = TreeSuccessor(curr))
            {
                if (curr.Color == Color.Red && curr.Left.Color == Color.Red || curr.Right.Color == Color.Red)
                {
                    return false;
                }
            }
            return true;
        }

        // Black depth of a node
        private int bd(Node x)
        {
            
            var count = 0;
            for (var curr = x; curr != nil; curr = curr.P)
            {
                if (curr.Color == Color.Black)
                {
                    count++;
                }
            }
            return count;
        }
             
        public bool BlackHeight()
        {            
            var rootHeight = -1;
            for (var curr = TreeMinimum(root); curr != nil; curr = TreeSuccessor(curr))
            {
                if (curr.Left == nil && curr.Right == nil)
                {
                    if (rootHeight != -1)
                    {
                        if (bd(curr) != rootHeight)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        rootHeight = bd(curr);
                    }
                }
            }
            return true;
        }
    }


    interface ISortedDictionary<T, U> where
        T : IComparable<T>
    {
        void Insert(T key, U value);
        void Delete(T key);
        Nullable<KeyValuePair<T, U>> Search(T key);
        KeyValuePair<T, U> Predecessor(T key);
        KeyValuePair<T, U> Successor(T key);
        KeyValuePair<T, U> Minimum();
        KeyValuePair<T, U> Maximum();
        U this[T key] { get; set; }
    }

    interface IRBproperties
    {
        bool BlackRoot();
        bool RedHasBlackChildren();
        bool BlackHeight();
    }
}

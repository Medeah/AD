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

    class RedBlackTree<T, U> : ISortedDictionary<T, U> where
        T : IComparable<T>
    {
        class Node
        {
            public Node(T key, U value)
            {
                Key = key;
                Value = value;
            }
            public Node P;
            public Node Left;
            public Node Right;
            public T Key;
            public U Value;
            public KeyValuePair<T, U> toKeyValue()
            {
                return new KeyValuePair<T, U>(Key, Value);
            }
        }

        Node root;

        public void Insert(T key, U value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            Node y = null;
            Node x = root;
            Node z = new Node(key, value);

            while (x != null)
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
            if (y == null)
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
        }
       

        private void Transplant(Node u, Node v)
        {
            if (u.P == null)
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
            if (v != null)
            {
                v.P = u.P;
            }
        }

        private void TreeDelete(Node z)
        {
            if (z.Left == null)
            {
                Transplant(z, z.Right);
            }
            else if (z.Right == null)
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
            if (res != null)
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
            if (root == null)
            {
                return null;
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
            if (res != null)
            {
                return res.toKeyValue();
            }
            return null;
        }

        private Node TreePredecessor(Node x)
        {
            if (x.Left != null)
            {
                return TreeMaximum(x.Left);
            }
            var y = x.P;
            while (y != null && x == y.Left)
            {
                x = y;
                y = y.P;
            }
            return y;
        }

        public KeyValuePair<T, U> Predecessor(T key)
        {
            var res = TreeSearch(root, key);
            if (res != null)
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
            if (x.Right != null)
            {
                return TreeMinimum(x.Right);
            }
            var y = x.P;
            while (y != null && x == y.Right)
            {
                x = y;
                y = y.P;
            }
            return y;
        }

        public KeyValuePair<T, U> Successor(T key)
        {
            var res = TreeSearch(root, key);
            if (res != null)
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
            while (x.Left != null)
            {
                x = x.Left;
            }
            return x;
        }

        public KeyValuePair<T, U> Minimum()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return TreeMinimum(root).toKeyValue();
        }

        private Node TreeMaximum(Node subTreeRoot)
        {
            var x = subTreeRoot;
            while (x.Right != null)
            {
                x = x.Right;
            }
            return x;
        }

        public KeyValuePair<T, U> Maximum()
        {
            if (root == null)
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
                if (res == null)
                {
                    Insert(key, value);
                }
                else
                {
                    res.Value = value;
                }
            }
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
}

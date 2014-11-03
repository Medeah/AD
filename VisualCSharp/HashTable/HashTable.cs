using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    [Serializable]
    public class HashTable<T, U> : IDictionary<T, U>
    {

        private LinkedList<T, U>[] data;
        private int cap;
        private int count = 0;

        public HashTable() : this(10) { } 
        public HashTable(int size)
        {
            data = new LinkedList<T, U>[10];
            cap = 10;
        } 
        public void Add(T key, U value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            var hash = key.GetHashCode() % cap;
            var cell = data[(uint)key.GetHashCode() % cap];
            if (cell == null)
            {
                cell = new LinkedList<T, U>();
                data[(uint)key.GetHashCode() % cap] = cell;
            }
            try
            {
                var test = cell.Search(key);
                throw new ArgumentException();
            }
            catch (KeyNotFoundException)
            {
                
            }

            cell.Insert(key, value);
            count++;
        }

        public bool Remove(T key)
        {
            var cell = data[(uint)key.GetHashCode() % cap];
            if (cell == null)
            {
                return false;
            }

            if (data[(uint)key.GetHashCode() % cap].Delete(key))
            {
                count--;
                return true;
            }
            return false;
        }

        private U Search (T key) {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            var hash = key.GetHashCode() % cap;
            var cell = data[(uint)key.GetHashCode() % cap];
            if (cell == null)
            {
                throw new KeyNotFoundException();
            }
            return data[(uint)key.GetHashCode() % cap].Search(key);
        }

        public U this[T key]
        {
            get
            {
                return Search(key);
            }
            set
            {
                if (ContainsKey(key)) {
                    var cell = data[(uint)key.GetHashCode() % cap];
                    cell.Set(key, value);
                } else {
                    Add(key, value);
                }
            }
        }

        public bool ContainsKey(T key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            var cell = data[(uint)key.GetHashCode() % cap];
            if (cell == null)
            {
                return false;
            }

            try
            {
                var test = cell.Search(key);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        public ICollection<T> Keys
        {
            get { return this.Select(x => x.Key).ToList(); }
        }

        public bool TryGetValue(T key, out U value)
        {
            if (ContainsKey(key))
            {
                value = this[key];
                return true;
            }
            value = default(U);
            return false;
        }

        public ICollection<U> Values
        {
            get { return this.Select(x => x.Value).ToList(); }
        }

        

        public void Add(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            count = 0;
            data = new LinkedList<T, U>[10];
            cap = 10;
        }

        public bool Contains(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
        {
            var size = count;
            for (int i = 0; i < cap; i++)
            {
                if (data[i] != null)
	            {
                    foreach (var item in data[i])
                    {
                        if (count != size)
                        {
                            throw new InvalidOperationException();
                        }
                        yield return item;
                    }
	            }
                
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    [Serializable]
    public class LinkedList<T, U>
    {
        private Link<T, U> head;

        public LinkedList()
        {
            head = null;
        }

        public U Search(T k)
        {
            var x = head;
            while (x != null)
            {
                if (x.Pair.Key.Equals(k))
                {
                    return x.Pair.Value;
                }
                x = x.Next;
            }

            throw new KeyNotFoundException();
        }

        public void Insert(T key, U value)
        {
            var x = new Link<T, U>(key, value);
            x.Next = head;
            head = x;
        }

        public bool Delete(T k)
        {
            if (head != null && head.Pair.Key.Equals(k))
            {
                head = head.Next;
                return true;
            }
            var curr = head;
            while(curr.Next != null) {
                if (curr.Next.Pair.Key.Equals(k))
	            {
                    curr.Next = curr.Next.Next;
                    return true;
	            }
                curr = curr.Next;
            }

            return false;
        }

        public IEnumerator<KeyValuePair<T, U>> GetEnumerator()
        {
            var x = head;
            while (x != null)
            {
                yield return x.Pair;
                x = x.Next;
            }
        }

        public bool Set(T key, U value)
        {
            var x = head;
            while (x != null)
            {
                if (x.Pair.Key.Equals(key))
                {
                    x.Pair = new KeyValuePair<T, U>(key, value);
                    return true;
                }
            }
            return false;
        }
    }
    [Serializable]
    public class Link<T, U>
    {
        public Link(T key, U value)
        {
            Pair = new KeyValuePair<T, U>(key, value);
        }
        public Link<T, U> Next;
        public KeyValuePair<T, U> Pair;
    }
}

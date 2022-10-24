using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class HashTable<TKey, TValue> : IEnumerable<TKey>
    {
        LinkedList<Item>[] hashArray;
        int itemCount;
        public int ItemCount { get => itemCount; set => itemCount = value; }

        public HashTable(int capacity = 1024)
        {
            hashArray = new LinkedList<Item>[capacity];
            itemCount = 0;
        }
        //public double CalcAavarageLoad()
        //{
        //    return hashArray.Where(item => item != null );
        //}
        public TValue GetValue(TKey key)
        {
            int ind = KeyToIndex(key);
            if (hashArray[ind] != null)
            {
                if (TryGetKey(key, out TValue value))
                    return value;
            }
            throw new ArgumentException("key does not exist");
        }
        private bool TryGetKey(TKey key, out TValue value)
        {
            int ind = KeyToIndex(key);
            if (hashArray[ind] == null)
            {
                value = default;
                return false;
            }
            value = hashArray[ind].FirstOrDefault
                (item => item.key.Equals(key)).value;
            if (value != null)
                return true;
            return false;
        }
        public bool ContainKey(TKey key)
        {
            int ind = KeyToIndex(key);
            if (hashArray[ind] == null)
                return false;
            return hashArray[ind].Any(item => item.key.Equals(key));
        }
        public void Add(TKey key, TValue value)
        {
            int ind = KeyToIndex(key);
            if (hashArray[ind] == null)
                hashArray[ind] = new LinkedList<Item>();
            else
            {
                if (ContainKey(key))
                    throw new ArgumentException($"this key is allready exist {key}");
            }

            hashArray[ind].AddFirst(new Item(key, value));
            itemCount++;

            if (itemCount > hashArray.Length)
            {
                ReHash();
            }
        }
        private void ReHash()
        {
            int newCapacity = hashArray.Length * 2;
            itemCount = 0;
            LinkedList<Item>[] temp = hashArray;
            hashArray = new LinkedList<Item>[newCapacity];
            foreach (LinkedList<Item> list in temp)
            {
                if (list != null)
                {
                    foreach (Item item in list)
                    {
                        Add(item.key, item.value);
                    }
                }
            }
        }
        private int KeyToIndex(TKey key)
        {
            int calcRes = key.GetHashCode();
            return Math.Abs(calcRes % hashArray.Length);
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < hashArray.Length; i++)
            {
                if (hashArray[i] != null)
                {
                    foreach (var item in hashArray[i]) yield return item.key;
                }
            }
        }

        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        class Item
        {
            public TKey key;
            public TValue value;
            public Item(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }
}

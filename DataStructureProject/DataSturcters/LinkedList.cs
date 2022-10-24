using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
   public class LinkedList<T> : IEnumerable<T>
    {
        Node start;
        Node end;
        public int Count { get; private set; }

        public void AddFirst(T value) // O(1) - O(c)
        {
            //add managment to end
            Node n = new Node(value);
            n.next = start;
            start = n;
            if (end == null) end = n;
            Count++;
        }

        public void AddLast(T value)  // O(1)
        {
            if (start == null)
            {
                AddFirst(value);
                return;
            }

            Node n = new Node(value);
            end.next = n;
            end = n;
            Count++;
        }

        public bool RemoveFirst() // O(1)
        {
            if (start == null) return false;
            else
            {
                start = start.next;
                if (start == null) end = start;
                Count--;
                return true;
            }
        }

        //arr[index]

        public bool GetAt(int index, out T item) // O(n)
        {
            item = default; // 0, false, null
            if (index < 0 || index >= Count) return false;

            Node tmp = start;
            for (int i = 0; i < index; i++)
            {
                tmp = tmp.next;
            }
            item = tmp.value;
            return true;
        }

        public override string ToString()
        {
            StringBuilder allValues = new StringBuilder();

            Node tmp = start;
            while (tmp != null)
            {
                allValues.Append(tmp.value + " ");
                tmp = tmp.next;
            }
            return allValues.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = start;
            //if (start == null) yield break;

            while (current != null)
            {
                yield return current.value;
                current = current.next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() // for old, non-generic environments
        {
            return GetEnumerator();
        }

        //public IEnumerator<T> GetEnumerator()
        //{
        //    ListEnumerator lstEnum = new ListEnumerator(start);
        //    return lstEnum;
        //}

        //class ListEnumerator : IEnumerator<T>
        //{
        //    Node current;
        //    Node start;
        //    bool isFirstTime;

        //    public ListEnumerator(Node start)
        //    {
        //        this.start = start;
        //        Reset();
        //    }

        //    public T Current => current.value;

        //    object IEnumerator.Current => throw new NotImplementedException();

        //    public void Dispose()
        //    {

        //    }           

        //    public bool MoveNext()
        //    {
        //        if (isFirstTime)
        //        {
        //            isFirstTime = false;
        //            return start != null;
        //        }

        //        if (current.next != null)
        //        {
        //            current = current.next;
        //            return true;
        //        }
        //        else return false;
        //    }

        //    public void Reset()
        //    {
        //        current = start;
        //        isFirstTime = true;
        //    }
        //}

        public class Node
        {
            internal T value;
            internal Node next; //reference to the next Node

            internal Node(T value)
            {
                this.value = value;
                next = null;
            }

            internal Node(T value, Node nextVal)
            {
                this.value = value;
                next = nextVal;
            }
        }
    }
}

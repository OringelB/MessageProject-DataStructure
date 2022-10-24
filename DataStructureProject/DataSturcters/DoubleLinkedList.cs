using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        public DoubleLinkedListNode Start { get; private set; }
        public DoubleLinkedListNode End { get; private set; }

        public int Count { get; private set; }

        public void AddFirst(T value)
        {
            DoubleLinkedListNode n = new DoubleLinkedListNode(value);
            n.next = Start;
            if (Start != null)
            {
                Start.previous = n;
            }
            Start = n;
            if (End == null) End = n;
            Count++;
        }

        public void AddLast(T value)
        {
            if (Start == null)
            {
                AddFirst(value);
                return;
            }

            DoubleLinkedListNode n = new DoubleLinkedListNode(value);
            n.previous = End;
            End.next = n;
            End = n;
            Count++;
        }

        public bool RemoveFirst()
        {
            if (Start == null) return false;
            else
            {
                if (Start.next == null)
                {
                    Start = null;
                    End = null;
                    Count--;
                    return true;
                }
                else
                {
                    Start = Start.next;
                    Start.previous = null;
                    if (Start == null) End = Start;
                    Count--;
                    return true;
                }
            }
        }
        public bool RemoveLast()
        {
            if (End == null) return false;
            else
            {
                End = End.previous;
                End.next = null;
                if (End == null) Start = End;
                Count--;
                return true;
            }
        }



        public bool GetAt(int position, out T item)
        {
            item = default;
            if (position < 0 || position >= Count) return false;

            DoubleLinkedListNode tmp = Start;
            for (int i = 0; i < position; i++)
            {
                tmp = tmp.next;
            }
            item = tmp.value;
            return true;
        }

        public void RemoveByNode(DoubleLinkedListNode currentNode)
        {
            DoubleLinkedListNode previousNode = currentNode.previous;
            DoubleLinkedListNode nextNode = currentNode.next;
            if (previousNode == null)
            {
                RemoveFirst();

            }
            else
            {
                previousNode.next = nextNode;
                nextNode.previous = previousNode;
            }
        }

        public T GetDataByNode(DoubleLinkedListNode currentNode)
        {
            return currentNode.value;
        }

        public bool AddAt(int position, T item)
        {
            if (position < 0 || position > Count) return false;
            if (position == Count)
            {
                AddLast(item);
                return true;
            }
            if (position == 0)
            {
                AddFirst(item);
                return true;
            }
            DoubleLinkedListNode tmp = Start;
            DoubleLinkedListNode newNode = new DoubleLinkedListNode(item);
            DoubleLinkedListNode Previous;
            for (int i = 0; i < position; i++)
            {
                tmp = tmp.next;
            }
            Previous = tmp.previous;
            Previous.next = newNode;
            newNode.previous = Previous;
            newNode.next = tmp;
            tmp.previous = newNode;
            Count++;
            return true;

        }
        public override string ToString()
        {
            StringBuilder allValues = new StringBuilder();

            DoubleLinkedListNode tmp = Start;
            while (tmp != null)
            {
                allValues.Append(tmp.value + " ");
                tmp = tmp.next;
            }
            return allValues.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            DoubleLinkedListNode current = Start;
            while (current != null)
            {
                yield return current.value;
                current = current.next;
            }
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }


        public class DoubleLinkedListNode
        {
            internal T value { get; set; }
            internal DoubleLinkedListNode next { get; set; }
            internal DoubleLinkedListNode previous { get; set; }

            internal DoubleLinkedListNode(T value)
            {
                this.value = value;
                next = null;
                previous = null;
            }

            internal DoubleLinkedListNode(T value, DoubleLinkedListNode nextVal, DoubleLinkedListNode previousVal)
            {
                this.value = value;
                next = nextVal;
                previous = previousVal;
            }
        }
    }
}

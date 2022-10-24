using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Queue<T> : IEnumerable<T>    //EnQueu = AddLast       (addfirst) pop = DeQueq (addlast)
    {
        LinkedList<T> QueueList = new LinkedList<T>();


        public void EnQueue(T item) => QueueList.AddLast(item);


        public bool DeQueu() => QueueList.RemoveFirst();


        public bool Peek(out T item) => QueueList.GetAt(0, out item);

        public override string ToString() // A,B,C => C-B-A
        {
            return QueueList.ToString();
        } 

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in QueueList)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Logic
{
    public class Manager
    {
        DoubleLinkedList<Message> messageDatas = new DoubleLinkedList<Message>();
        HashTable<string, DataStructures.Queue<DoubleLinkedList<Message>.DoubleLinkedListNode>> groupHashTable = new HashTable<string, DataStructures.Queue<DoubleLinkedList<Message>.DoubleLinkedListNode>>();

        public void SendMessageToGroup(string groupName, string message)
        {
            Message msg = new Message(message, groupName);
            messageDatas.AddLast(msg);
            DoubleLinkedList<Message>.DoubleLinkedListNode CurrentNode = messageDatas.End;


            if (groupHashTable.ContainKey(groupName))
            {
                groupHashTable.GetValue(groupName).EnQueue(CurrentNode);
            }
            else
            {
                DataStructures.Queue<DoubleLinkedList<Message>.DoubleLinkedListNode> NewQueue = new DataStructures.Queue<DoubleLinkedList<Message>.DoubleLinkedListNode>();
                NewQueue.EnQueue(CurrentNode);
                groupHashTable.Add(groupName, NewQueue);
            }
        }
        public bool GetAllGroupNames(out string[] groupNames)
        {
            groupNames = default;
            if (groupHashTable.ItemCount == 0) return false;
            groupNames = new string[groupHashTable.ItemCount];
            int index = 0;
            foreach (var item in groupHashTable)
            {
                groupNames[index] = item.ToString();
                index++;
            }
            return true;
        }

        public bool GetAndRemoveMessageFromGroup(string groupName, out Message message) // Reads and takes out the oldest message in the key group entered.
        {
            message = default;
            if (groupHashTable.ContainKey(groupName))
            {

                var messageQueueByGroupName = groupHashTable.GetValue(groupName);

                if (messageQueueByGroupName.Peek(out DoubleLinkedList<Message>.DoubleLinkedListNode doubleLinkedListNode))
                {
                    message = messageDatas.GetDataByNode(doubleLinkedListNode);
                    messageQueueByGroupName.DeQueu();
                    messageDatas.RemoveByNode(doubleLinkedListNode);
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public bool CutOldestMessage(out Message message)
        { 
            if (messageDatas.Count == 0)
            {
                message = default;
                return false;
            }
            messageDatas.GetAt(0, out Message OldestMessage);   //Gets the oldest message
            message = OldestMessage; //returns the oldest message
            string Key = OldestMessage.Name; //gets the key
            DataStructures.Queue<DoubleLinkedList<Message>.DoubleLinkedListNode> CurrentQueue = groupHashTable.GetValue(Key); //Gets the queq
            CurrentQueue.DeQueu(); //remove the last message
            messageDatas.RemoveFirst(); //removes last message from all messages
            return true; //
        }
      
        public bool GetOldestMessages(int messagesAmount, out Message[] messages)
        {
            messages = default;
            if (messageDatas.Count == 0)
            {
                return false;
            }
            int counter = 0;
            if (messageDatas.Count < messagesAmount)
            {
                messagesAmount = messageDatas.Count;
            }
            messages = new Message[messagesAmount];
            foreach (Message msg in messageDatas)
            {
                if (counter == messagesAmount)
                {
                    return true;
                }
                messages[counter] = msg;
                counter++;
            }
            return true;
        }
        public bool GetMessagesFromDate(DateTime date, out DataStructures.LinkedList<Message> messages )
        {
            messages = new DataStructures.LinkedList<Message>();
            foreach(Message msg in messageDatas)
            {
                if(msg.DateSent.CompareTo(date) > 0)
                {
                    messages.AddFirst(msg);
                }
            }
            return true;
        }
        public bool ReadMessagesByGroupAndContent(string groupName, string content, out DataStructures.LinkedList<Message> messages) // Does not delete the message
        {
            messages = new DataStructures.LinkedList<Message>();
            if (messageDatas.Count == 0) return false;

            //string lowerCaseContent = content.ToLower();
            //string lowerGroupName = groupName.ToLower();

            //Regex regex = new Regex($@".{lowerCaseContent}");
            int messagesCount = 0;

            foreach (Message message in messageDatas)
            {
                if(message.Name == groupName)
                {
                    string[] words = Regex.Split(message.MessageData, @"\W+");
                    foreach(string word in words)
                    {
                        if (word == content)
                        {
                            messagesCount++;
                            messages.AddFirst(message);
                        }
                    }

                }
            }

            //foreach (Message message in messageDatas)
            //{
            //    if (message.Name.ToLower() == lowerGroupName)
            //    {
            //        if (Regex.IsMatch(message.MessageData.ToLower(), @"\b" + content + @"\b"))
            //        {
            //            messages.AddFirst(message);
            //            messagesCount++;
            //        }
            //    }
            //}
            if (messagesCount == 0) return false;
            return true;
        }
    }


}
